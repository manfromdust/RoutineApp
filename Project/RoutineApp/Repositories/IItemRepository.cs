using RoutineApp.Models;

namespace RoutineApp.Repositories
{
    public interface IItemRepository
    {
        event EventHandler<RoutineItem> OnItemAdded;
        event EventHandler<RoutineItem> OnItemRemoved;
        event EventHandler<RoutineItem> OnItemUpdated;

        Task<List<RoutineItem>> GetItemsAsync();
        Task AddItemAsync(RoutineItem item);
        Task RemoveItemAsync(RoutineItem item);
        Task UpdateItemAsync(RoutineItem item);
    }
}
