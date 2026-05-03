using RoutineApp.Models;

namespace RoutineApp.Repositories
{
    public interface IQuoteItemRepository
    {
        event EventHandler<RoutineQuote> OnItemAdded;
        event EventHandler<RoutineQuote> OnItemRemoved;
        event EventHandler<RoutineQuote> OnItemUpdated;

        Task<List<RoutineQuote>> GetItemsAsync(int routineId);
        Task AddItemAsync(RoutineQuote item);
        Task RemoveItemAsync(RoutineQuote item);
        Task UpdateItemAsync(RoutineQuote item);
    }
}
