
namespace CrossStitching.Models
{
    public class CanvasData
    {
        public int Rows { get; set; } = 100;
        public int Cols { get; set; } = 100;

        public List<string> Pixels { get; set; }
    }
}
