using SQLite;
using VetZone.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VetZone.Services
{
    public class ClienteService
    {
        private SQLiteAsyncConnection _database;

        public ClienteService()
        {
            InitializeDatabase();
        }

        private async void InitializeDatabase()
        {
            if (_database == null)
            {
                var dbPath = Path.Combine(FileSystem.AppDataDirectory, "VetZone.db3");
                _database = new SQLiteAsyncConnection(dbPath);
                await _database.CreateTableAsync<Cliente>();
            }
        }

        public async Task<List<Cliente>> GetClientesAsync()
        {
            await EnsureDatabaseInitialized();
            return await _database.Table<Cliente>().ToListAsync();
        }

        public async Task<Cliente> GetClienteAsync(int id)
        {
            await EnsureDatabaseInitialized();
            return await _database.Table<Cliente>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public async Task<int> SaveClienteAsync(Cliente cliente)
        {
            await EnsureDatabaseInitialized();
            if (cliente.Id != 0)
            {
                // Atualizar cliente existente
                return await _database.UpdateAsync(cliente);
            }
            else
            {
                // Adicionar novo cliente
                return await _database.InsertAsync(cliente);
            }
        }

        public async Task<int> DeleteClienteAsync(Cliente cliente)
        {
            await EnsureDatabaseInitialized();
            return await _database.DeleteAsync(cliente);
        }

        private async Task EnsureDatabaseInitialized()
        {
            // Garante que o banco de dados foi inicializado antes de qualquer operação
            if (_database == null)
            {
                await Task.Run(() => InitializeDatabase());
            }
        }
    }
}