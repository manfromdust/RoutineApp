using SQLite;
using RoutineApp.Models;

namespace RoutineApp.Services
{
    public static class DatabaseInitializer
    {
        private static readonly Task _initTask = Task.Run(InitializeAsync);

        public static Task WaitForReadyAsync() => _initTask;

        private static async Task InitializeAsync()
        {
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "routineapp.db");

            var connection = new SQLiteAsyncConnection(dbPath);

            await connection.CreateTableAsync<RoutineItem>();
            await connection.CreateTableAsync<RoutineQuote>();
            await connection.CreateTableAsync<NotificationRecord>();
        }
    }
}
