using SQLite;

namespace RoutineApp.Models
{
    public class RoutineItem
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; } = true;
    }
}
