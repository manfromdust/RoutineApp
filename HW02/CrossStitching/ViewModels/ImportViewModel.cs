using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CrossStitching.Models;
using CrossStitching.Services;
using System.Collections.ObjectModel;

namespace CrossStitching.ViewModels
{
    public partial class ImportViewModel : ViewModel
    {
        TaskCompletionSource<bool> _completion;

        [ObservableProperty]
        private ObservableCollection<string> _importedFiles;

        [ObservableProperty]
        private string _chosenFile = "No file chosen";

        public ImportViewModel(TaskCompletionSource<bool> completion)
        {
            _completion = completion;
            LoadSavedFiles();
        }

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
            var filePath = Path.Combine(FileSystem.AppDataDirectory, ChosenFile);

            try
            {
                CanvasData? importedData = ImportExportCanvas.ImportFromJson(filePath);
                if (importedData == null)
                {
                    throw new Exception("Failed to import canvas data.");
                }
                CanvasData = importedData;
            }
            catch (Exception ex)
            {
                // error pop up message
            }
            finally
            {
                _completion.SetResult(true);
                await Application.Current.MainPage.Navigation.PopAsync();
            }
        }
    }
}
