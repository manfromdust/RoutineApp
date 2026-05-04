using RoutineApp.Models;

namespace RoutineApp.Repositories
{
    public interface INotificationRepository
    {
        event EventHandler<NotificationRecord> OnItemAdded;
        event EventHandler<NotificationRecord> OnItemRemoved;
        event EventHandler<NotificationRecord> OnItemUpdated;

        Task<List<NotificationRecord>> GetItemsAsync(int routineId);
        Task AddItemAsync(NotificationRecord item);
        Task RemoveItemAsync(NotificationRecord item);
        Task UpdateItemAsync(NotificationRecord item);
    }
}
