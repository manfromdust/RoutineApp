using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CrossStitching.Models;
using CrossStitching.Services;
using System.Collections.ObjectModel;

namespace CrossStitching.ViewModels
{
    public partial class ImportViewModel : ViewModel
    {
        private readonly INavigation _navigation;
        private readonly CanvasData _canvasData;

        [ObservableProperty]
        private ObservableCollection<string> _importedFiles;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(GetFileName))]
        private string _chosenFile = "No file chosen";

        public ImportViewModel(INavigation navigation,
                               CanvasData data)
        {
            _navigation = navigation;
            _canvasData = data;
            ImportedFiles = new ObservableCollection<string>();
            LoadSavedFiles();
        }

        public string GetFileName => Path.GetFileNameWithoutExtension(ChosenFile);

        private void LoadSavedFiles()
        {
            ImportedFiles.Clear();
            var files = Directory.GetFiles(FileSystem.AppDataDirectory, "*.json");
            foreach (var file in files)
            {
                ImportedFiles.Add(file);
            }
        }

        [RelayCommand]
        public async Task ImportFromJsonAsync()
        {
            //var filePath = Path.Combine(FileSystem.AppDataDirectory, ChosenFile);

            try
            {
                //CanvasData? importedData = ImportExportCanvas.ImportFromJson(filePath);
                CanvasData? importedData = ImportExportCanvas.ImportFromJson(ChosenFile);
                if (importedData == null)
                {
                    throw new Exception("Failed to import canvas data.");
                }

                _canvasData.Rows = importedData.Rows;
                _canvasData.Cols = importedData.Cols;
                _canvasData.Pixels = importedData.Pixels;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                await _navigation.PopAsync();
            }
        }
    }
}
