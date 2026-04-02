using CrossStitching.Models;

namespace CrossStitching.Services
{
    public class ColorsComparer : IComparer<ThreadColor>
    {
        public int Compare(ThreadColor? x, ThreadColor? y)
        {
            if (x == null && y == null) return 0;
            if (x == null) return -1;
            if (y == null) return 1;
            return string.Compare(x.Hex, y.Hex, StringComparison.Ordinal);
        }
    }
}
