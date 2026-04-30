using RoutineApp.Models;
using SQLite;

namespace RoutineApp.Repositories
{
    public class RoutineItemRepository : IRoutineItemRepository
    {
        private SQLiteAsyncConnection _connection;
        
        public event EventHandler<RoutineItem> OnItemAdded;
        public event EventHandler<RoutineItem> OnItemRemoved;
        public event EventHandler<RoutineItem> OnItemUpdated;

        public async Task<List<RoutineItem>> GetItemsAsync()
        {
            await CreateConnectionAsync();
            return await _connection.Table<RoutineItem>().ToListAsync();
        }

        public async Task AddItemAsync(RoutineItem item)
        {
            await CreateConnectionAsync();
            await _connection.InsertAsync(item);
            OnItemAdded?.Invoke(this, item);
        }

        public async Task RemoveItemAsync(RoutineItem item)
        {
            await CreateConnectionAsync();
            await _connection.DeleteAsync(item);
            OnItemRemoved?.Invoke(this, item);
        }

        public async Task UpdateItemAsync(RoutineItem item)
        {
            await CreateConnectionAsync();
            await _connection.UpdateAsync(item);
            OnItemUpdated?.Invoke(this, item);
        }

        private async Task CreateConnectionAsync()
        {
            if (_connection == null)
            {
                var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "routineapp.db");
                _connection = new SQLiteAsyncConnection(databasePath);
                await _connection.CreateTableAsync<RoutineItem>();
            }
        }
    }
}
