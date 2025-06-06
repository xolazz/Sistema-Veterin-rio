using SQLite;
using VetZone.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VetZone.Services
{
    public class UsuarioService
    {
        private SQLiteAsyncConnection _database;

        public UsuarioService()
        {
            InitializeDatabase();
        }

        private async void InitializeDatabase()
        {
            if (_database == null)
            {
                var dbPath = Path.Combine(FileSystem.AppDataDirectory, "VetZone.db3");
                _database = new SQLiteAsyncConnection(dbPath);
                await _database.CreateTableAsync<Usuario>();
            }
        }

        public async Task<int> SaveUsuarioAsync(Usuario usuario)
        {
            await EnsureDatabaseInitialized();
            if (usuario.Id != 0)
            {
                return await _database.UpdateAsync(usuario);
            }
            else
            {
                return await _database.InsertAsync(usuario);
            }
        }

        public async Task<Usuario> GetUsuarioByIdAsync(int id)
        {
            await EnsureDatabaseInitialized();
            return await _database.Table<Usuario>().Where(u => u.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Usuario> GetUsuarioByNomeAsync(string nome)
        {
            await EnsureDatabaseInitialized();
            return await _database.Table<Usuario>().Where(u => u.Nome.ToLower() == nome.ToLower()).FirstOrDefaultAsync();
        }

        // Obter todos os usuários (útil para testes ou lista de gerenciamento)
        public async Task<List<Usuario>> GetUsuariosAsync()
        {
            await EnsureDatabaseInitialized();
            return await _database.Table<Usuario>().ToListAsync();
        }

        public async Task<int> DeleteUsuarioAsync(Usuario usuario)
        {
            await EnsureDatabaseInitialized();
            return await _database.DeleteAsync(usuario);
        }

        private async Task EnsureDatabaseInitialized()
        {
            if (_database == null)
            {
                
            }
        }
    }
}