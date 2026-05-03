using RoutineApp.Models;

namespace RoutineApp.Repositories
{
    public class QuoteItemRepository : ItemRepository<RoutineQuote>, IQuoteItemRepository
    {
        public async Task<List<RoutineQuote>> GetItemsAsync(int routineId)
        {
            await CreateConnectionAsync();
            return await _connection.Table<RoutineQuote>()
                                    .Where(q => q.RoutineId == routineId)
                                    .ToListAsync();
        }
    }
}
