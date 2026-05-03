using SQLite;

namespace RoutineApp.Models
{
    public class RoutineQuote
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Indexed]
        public int RoutineId { get; set; }
        public string Quote { get; set; } = string.Empty;
        public bool Active { get; set; }
    }
}
