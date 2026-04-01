using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CrossStitching.Services;

namespace CrossStitching.ViewModels
{
    public partial class ExportViewModel : ViewModel
    {
        [ObservableProperty]
        private string _fileName;

        [RelayCommand]
        public async Task ExportToJsonAsync()
        {
            if (FileName == null)
            {
                FileName = DateTime.Now.ToString() + ".json";
            }
            else
            {
                FileName = FileName.EndsWith(".json") ? FileName : FileName + ".json";
            }

            ImportExportCanvas.ExportToJson(FileName, CanvasData);
            await Navigation.PopAsync();
        }
    }
}
