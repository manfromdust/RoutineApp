using RoutineApp.Models;

namespace RoutineApp.Repositories
{
    public interface IRoutineItemRepository
    {
        event EventHandler<RoutineItem> OnItemAdded;
        event EventHandler<RoutineItem> OnItemRemoved;
        event EventHandler<RoutineItem> OnItemUpdated;

        Task<List<RoutineItem>> GetItemsAsync();
        Task<RoutineItem> GetItemByIdAsync(int id);
        Task AddItemAsync(RoutineItem item);
        Task RemoveItemAsync(RoutineItem item);
        Task UpdateItemAsync(RoutineItem item);
    }
}
