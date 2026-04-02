

using CrossStitching.Models;

namespace CrossStitching.ViewModels
{
    public class CrossStitchingDrawable : IDrawable
    {
        public CanvasData Data { get; set; }
        public float CellSize { get; set; }

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            if (Data == null || Data.Pixels == null)
            {
                return;
            }

            for (int row = 0; row < Data.Rows; row++)
            {
                for (int col = 0; col < Data.Cols; col++)
                {
                    int index = (row * Data.Cols) + col;

                    canvas.FillColor = Color.FromArgb(Data.Pixels[index]);
                    canvas.FillRectangle(col * CellSize, row * CellSize, CellSize, CellSize);

                    canvas.StrokeColor = Colors.LightGray;
                    canvas.StrokeSize = 1;
                    canvas.DrawRectangle(col * CellSize, row * CellSize, CellSize, CellSize);
                }
            }
        }
    }
}
