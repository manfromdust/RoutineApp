using SQLite;

namespace RoutineApp.Repositories
{
    public class ItemRepository<T> where T : new()
    {
        protected SQLiteAsyncConnection _connection;

        public event EventHandler<T> OnItemAdded;
        public event EventHandler<T> OnItemRemoved;
        public event EventHandler<T> OnItemUpdated;

        public async Task<List<T>> GetItemsAsync()
        {
            await CreateConnectionAsync();
            return await _connection.Table<T>().ToListAsync();
        }

        public async Task AddItemAsync(T item)
        {
            await CreateConnectionAsync();
            await _connection.InsertAsync(item);
            OnItemAdded?.Invoke(this, item);
        }

        public async Task RemoveItemAsync(T item)
        {
            await CreateConnectionAsync();
            await _connection.DeleteAsync(item);
            OnItemRemoved?.Invoke(this, item);
        }

        public async Task UpdateItemAsync(T item)
        {
            await CreateConnectionAsync();
            await _connection.UpdateAsync(item);
            OnItemUpdated?.Invoke(this, item);
        }

        protected async Task CreateConnectionAsync()
        {
            if (_connection == null)
            {
                var databasePath = Path.Combine(FileSystem.AppDataDirectory, "routineapp.db");
                _connection = new SQLiteAsyncConnection(databasePath);
                await _connection.CreateTableAsync<T>();
            }
        }
    }
}
