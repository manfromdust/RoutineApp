using RoutineApp.Models;

namespace RoutineApp.Repositories
{
    public class RoutineItemRepository : ItemRepository<RoutineItem>, IRoutineItemRepository
    {
        public async Task<List<RoutineItem>> GetItemsAsync()
        {
            await CreateConnectionAsync();
            return await _connection.Table<RoutineItem>().ToListAsync();
        }
        public async Task<RoutineItem> GetItemByIdAsync(int id)
        {
            await CreateConnectionAsync();
            return await _connection.Table<RoutineItem>().Where(item => item.Id == id).FirstOrDefaultAsync();
        }
    }
}
