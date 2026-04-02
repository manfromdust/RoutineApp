using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CrossStitching.Models;

namespace CrossStitching.ViewModels
{
    public partial class SetupViewModel : ViewModel
    {
        private readonly INavigation _navigation;
        private CanvasData _data;

        [ObservableProperty]
        private string _inputRows = "100";

        [ObservableProperty]
        private string _inputCols = "100";

        public SetupViewModel(INavigation navigation,
                              CanvasData data)
        {
            _navigation = navigation;
            _data = data;
        }

        [RelayCommand]
        public async Task GenerateCanvasAsync()
        {
            int setCols = 100;
            int setRows = 100;
            int.TryParse(InputCols, out setCols);
            int.TryParse(InputRows, out setRows);

            _data.Cols = setCols;
            _data.Rows = setRows;

            _data.GenerateCanvas();

            await _navigation.PopAsync();
        }
    }
}
