
namespace CrossStitching.Models
{
    public class CanvasData
    {
        public int Rows { get; set; } = 50;
        public int Cols { get; set; } = 50;
        public float CellSize { get; set; } = 12f;

        public List<string> Pixels { get; set; }

        public void GenerateCanvas()
        {
                Pixels = new List<string>(Rows * Cols);
                for (int i = 0; i < Rows * Cols; i++)
                {
                    Pixels.Add("#FFFFFF");
            }
        }
    }
}
