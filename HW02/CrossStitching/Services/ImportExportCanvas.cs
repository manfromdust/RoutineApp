
using CrossStitching.Models;

namespace CrossStitching.Services
{
    public static class ImportExportCanvas
    {
        public static void ExportToJson(string fileName, CanvasData data)
        {
            if (!fileName.EndsWith(".json"))
            {
                fileName += ".json";
            }

            var filePath = Path.Combine(FileSystem.AppDataDirectory, fileName);

            File.WriteAllText(filePath, System.Text.Json.JsonSerializer.Serialize(data));
        }

        public static CanvasData? ImportFromJson(string filePath)
        {
            if (filePath.EndsWith("no file chosen"))
            {
                return null;
            }

            if (File.Exists(filePath))
            {
                var json = File.ReadAllText(filePath);
                var data = System.Text.Json.JsonSerializer.Deserialize<CanvasData>(json);
                return data;
            }

            return null;
        }
    }
}
