using CrossStitching.Models;

namespace CrossStitching.ViewModels
{
    public class PaletteDrawable : IDrawable
    {
        public List<ThreadColor> Colors { get; set; }
        public int Columns { get; set; } = 10;
        public float CellSize { get; set; } = 20f;
        public float Spacing { get; set; } = 1f;

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            if (Colors == null || Colors.Count == 0) return;

            for (int i = 0; i < Colors.Count; i++)
            {
                int col = i % Columns;
                int row = i / Columns;

                float x = col * (CellSize + Spacing);
                float y = row * (CellSize + Spacing);

                canvas.FillColor = Color.FromArgb(Colors[i].Hex);
                canvas.FillRectangle(x, y, CellSize, CellSize);

                canvas.StrokeColor = Microsoft.Maui.Graphics.Colors.LightGray;
                canvas.StrokeSize = 1;
                canvas.DrawRectangle(x, y, CellSize, CellSize);
            }
        }
    }
}
