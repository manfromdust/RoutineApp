using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CrossStitching.Models;
using CrossStitching.Services;

namespace CrossStitching.ViewModels
{
    public partial class ExportViewModel : ObservableObject
    {
        private readonly INavigation _navigation;
        private readonly CanvasData _canvasData;

        [ObservableProperty]
        private string _fileName;

        public ExportViewModel(INavigation navigation, CanvasData data)
        {
            _navigation = navigation;
            _canvasData = data;
        }

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

            ImportExportCanvas.ExportToJson(FileName, _canvasData);
            await _navigation.PopAsync();
        }
    }
}
