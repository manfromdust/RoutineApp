
using CrossStitching.Models;

namespace CrossStitching.Services
{
    public static class LoadCustomColors
    {
        public static async Task<List<ThreadColor>> LoadColorsAsync()
        {
            var colors = new List<ThreadColor>();
            
            using var stream = await FileSystem.OpenAppPackageFileAsync("threadcolors_dmc_rgb.csv");
            using var reader = new StreamReader(stream);

            string line;

            line = await reader.ReadLineAsync();

            while ((line = await reader.ReadLineAsync()) != null)
            {
                var parts = line.Split(',');

                if (parts.Length >= 6)
                {
                    if (int.TryParse(parts[0], out int id) &&
                        int.TryParse(parts[2], out int r) &&
                        int.TryParse(parts[3], out int g) &&
                        int.TryParse(parts[4], out int b))
                    {
                        var color = new ThreadColor
                        {
                            Id = id,
                            Name = parts[1],
                            R = r,
                            G = g,
                            B = b,
                            Hex = parts[5]
                        };
                        colors.Add(color);
                    }
                }
            }

            return colors;
        }
    }
}
