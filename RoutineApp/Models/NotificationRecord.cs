using SQLite;

namespace RoutineApp.Models
{
    public class NotificationRecord
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Indexed]
        public int RoutineId { get; set; }

        public TimeSpan TimeOfDay { get; set; }
        public bool Active { get; set; } = true;
    }
}
