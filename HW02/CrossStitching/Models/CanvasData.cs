
namespace CrossStitching.Models
{
    public class CanvasData
    {
        public int Rows { get; set; } = 100;
        public int Cols { get; set; } = 100;

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
