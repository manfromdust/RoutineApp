
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
            string query = @"
                            SELECT n.* 
                            FROM NotificationRecord n
                            INNER JOIN RoutineItem r ON n.RoutineId = r.Id
                            WHERE n.Active = 1 AND r.Active = 1";
            return await _connection.QueryAsync<NotificationRecord>(query);
            //return await _connection.Table<NotificationRecord>()
            //    .Where(item => item.Active)
            //    .ToListAsync();
        }
    }
}
