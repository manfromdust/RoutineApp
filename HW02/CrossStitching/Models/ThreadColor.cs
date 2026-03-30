
namespace CrossStitching.Models
{
    public class ThreadColor
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int R { get; set; }
        public int G { get; set; }
        public int B { get; set; }

        public string Hex { get; set; }

        public Color Color { get; set; }
    }
}
