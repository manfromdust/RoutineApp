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
    }
}
