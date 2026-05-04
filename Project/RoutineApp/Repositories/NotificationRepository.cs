
using RoutineApp.Models;

namespace RoutineApp.Repositories
{
    class NotificationRepository : ItemRepository<NotificationRecord>, INotificationRepository
    {
        public async Task<List<NotificationRecord>> GetItemsAsync(int routineId)
        {
            await CreateConnectionAsync();
            return await _connection.Table<NotificationRecord>()
                                    .Where(n => n.RoutineId == routineId)
                                    .ToListAsync();
        }
    }
}
