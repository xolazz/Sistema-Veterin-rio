using SQLite;
using VetZone.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VetZone.Services
{
    public class EspecieService
    {
        private SQLiteAsyncConnection _database;

        public EspecieService()
        {
            InitializeDatabase();
        }

        private async void InitializeDatabase()
        {
            if (_database == null)
            {
                var dbPath = Path.Combine(FileSystem.AppDataDirectory, "VetZone.db3");
                _database = new SQLiteAsyncConnection(dbPath);
                await _database.CreateTableAsync<Especie>();
            }
        }

        public async Task<List<Especie>> GetEspeciesAsync()
        {
            await EnsureDatabaseInitialized();
            return await _database.Table<Especie>().ToListAsync();
        }

        public async Task<Especie> GetEspecieAsync(int id)
        {
            await EnsureDatabaseInitialized();
            return await _database.Table<Especie>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public async Task<int> SaveEspecieAsync(Especie especie)
        {
            await EnsureDatabaseInitialized();
            if (especie.Id != 0)
            {
                // Atualizar espécie existente
                return await _database.UpdateAsync(especie);
            }
            else
            {
                // Adicionar nova espécie
                return await _database.InsertAsync(especie);
            }
        }

        public async Task<int> DeleteEspecieAsync(Especie especie)
        {
            await EnsureDatabaseInitialized();
            return await _database.DeleteAsync(especie);
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