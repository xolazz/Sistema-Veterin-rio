using SQLite;
using VetZone.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VetZone.Services
{
    public class AnimalService
    {
        private SQLiteAsyncConnection _database;
        private EspecieService _especieService;
        private ClienteService _clienteService; 

        public AnimalService()
        {
            InitializeDatabase();
            _especieService = new EspecieService();
            _clienteService = new ClienteService(); 
        }

        private async void InitializeDatabase()
        {
            if (_database == null)
            {
                var dbPath = Path.Combine(FileSystem.AppDataDirectory, "VetZone.db3");
                _database = new SQLiteAsyncConnection(dbPath);
                await _database.CreateTableAsync<Animal>();
            }
        }

        public async Task<List<Animal>> GetAnimaisAsync()
        {
            await EnsureDatabaseInitialized();
            var animais = await _database.Table<Animal>().ToListAsync();

            foreach (var animal in animais)
            {
                animal.Especie = await _especieService.GetEspecieAsync(animal.EspecieId);
                // Carrega o cliente
                animal.Cliente = await _clienteService.GetClienteAsync(animal.ClienteId);
            }
            return animais;
        }

        public async Task<Animal> GetAnimalAsync(int id)
        {
            await EnsureDatabaseInitialized();
            var animal = await _database.Table<Animal>().Where(i => i.Id == id).FirstOrDefaultAsync();
            if (animal != null)
            {
                animal.Especie = await _especieService.GetEspecieAsync(animal.EspecieId);
                // Carrega o cliente
                animal.Cliente = await _clienteService.GetClienteAsync(animal.ClienteId);
            }
            return animal;
        }

        public async Task<int> SaveAnimalAsync(Animal animal)
        {
            await EnsureDatabaseInitialized();
            if (animal.Id != 0)
            {
                return await _database.UpdateAsync(animal);
            }
            else
            {
                return await _database.InsertAsync(animal);
            }
        }

        public async Task<int> DeleteAnimalAsync(Animal animal)
        {
            await EnsureDatabaseInitialized();
            return await _database.DeleteAsync(animal);
        }

        private async Task EnsureDatabaseInitialized()
        {
            if (_database == null)
            {
                await Task.Run(() => InitializeDatabase());
            }
        }
    }
}