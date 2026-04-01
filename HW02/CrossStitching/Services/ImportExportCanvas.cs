
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
    }
}
