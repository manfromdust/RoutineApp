using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CrossStitching.Models;

namespace CrossStitching.ViewModels
{
    public partial class SetupViewModel : ObservableObject
    {
        private readonly INavigation _navigation;
        private readonly TaskCompletionSource<bool> _taskCompletionSource;
        private bool _isTaskCompleted = false;
        private CanvasData _data;

        [ObservableProperty]
        private string _inputRows = "50";

        [ObservableProperty]
        private string _inputCols = "50";

        [ObservableProperty]
        private string _inputCellSize = "12";

        public SetupViewModel(INavigation navigation,
                              CanvasData data,
                              TaskCompletionSource<bool> taskCompletionSource)
        {
            _navigation = navigation;
            _data = data;
            _taskCompletionSource = taskCompletionSource;
        }

        public void NotifyDisappeared()
        {
            if (!_isTaskCompleted)
            {
                _isTaskCompleted = true;
                _taskCompletionSource?.SetResult(false);
            }
        }

        [RelayCommand]
        public async Task GenerateCanvasAsync()
        {
            int setCols = 50;
            int setRows = 50;
            float setCellSize = 12f;

            if (float.TryParse(InputCellSize.Trim(), out setCellSize) &&
                int.TryParse(InputCols.Trim(), out setCols) &&
                int.TryParse(InputRows.Trim(), out setRows))
            {
                if (setCols < 0 || setRows < 0 ||
                    setCellSize < 0)
                {
                    return;
                }
                _data.CellSize = setCellSize;
                _data.Cols = setCols;
                _data.Rows = setRows;

                _data.GenerateCanvas();

                _taskCompletionSource.SetResult(true);
                _isTaskCompleted = true;

                await _navigation.PopAsync();
            }
        }
    }
}
