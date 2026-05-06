
using RoutineApp.Models;

namespace RoutineApp.Repositories
{
    public class NotificationRepository : ItemRepository<NotificationRecord>, INotificationRepository
    {
        public async Task<List<NotificationRecord>> GetItemsAsync(int routineId)
        {
            await CreateConnectionAsync();
            return await _connection.Table<NotificationRecord>()
                                    .Where(n => n.RoutineId == routineId)
                                    .ToListAsync();
        }

        public async Task<List<NotificationRecord>> GetActiveItemsAsync()
        {
            await CreateConnectionAsync();
            return await _connection.Table<NotificationRecord>()
                .Where(item => item.Active)
                .ToListAsync();
        }
    }
}
