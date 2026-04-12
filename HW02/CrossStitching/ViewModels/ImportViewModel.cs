using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CrossStitching.Models;
using CrossStitching.Services;
using System.Collections.ObjectModel;

namespace CrossStitching.ViewModels
{
    public partial class ImportViewModel : ObservableObject
    {
        private readonly INavigation _navigation;
        private readonly CanvasData _canvasData;
        private readonly TaskCompletionSource<bool> _tcs;
        private bool _isTaskCompleted = false;

        [ObservableProperty]
        private ObservableCollection<string> _importedFiles;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(GetFileName))]
        private string _chosenFile = "No file chosen";

        public ImportViewModel(INavigation navigation,
                               CanvasData data,
                               TaskCompletionSource<bool> tcs)
        {
            _navigation = navigation;
            _canvasData = data;
            _tcs = tcs;
            ImportedFiles = [];
            LoadSavedFiles();
        }

        public void NotifyDisappeared()
        {
            if (!_isTaskCompleted)
            {
                _isTaskCompleted = true;
                _tcs?.SetResult(false);
            }
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
            try
            {
                CanvasData? importedData = ImportExportCanvas.ImportFromJson(ChosenFile) ?? 
                    throw new Exception("Failed to import canvas data.");

                _canvasData.Rows = importedData.Rows;
                _canvasData.Cols = importedData.Cols;
                _canvasData.CellSize = importedData.CellSize;
                _canvasData.Pixels = importedData.Pixels;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                _isTaskCompleted = true;
                _tcs?.SetResult(true);
                await _navigation.PopAsync();
            }
        }
    }
}
