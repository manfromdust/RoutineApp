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

        public async Task<List<string>> GetRandomQuotes(int routineId, int max)
        {
            await CreateConnectionAsync();
            string query = "SELECT * FROM RoutineQuote WHERE RoutineId = ? ORDER BY RANDOM() LIMIT ?";
            var randomQuotes = await _connection.QueryAsync<RoutineQuote>(query, routineId, max);
            return randomQuotes.Select(q => q.Quote).ToList();
        }
    }
}
