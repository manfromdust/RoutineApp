using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CrossStitching.Models;
using CrossStitching.Views;


namespace CrossStitching.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly INavigation? _navigation;
        private CanvasData _data;

        public Action RequestRedraw { get; set; }
        public Action<float> UpdateDrawableSize { get; set; }

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(CanvasWidth))]
        [NotifyPropertyChangedFor(nameof(CanvasHeight))]
        private float _cellSize;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(SelectedColorUI))]
        private string _selectedColor;


        public MainViewModel(INavigation navigation, CanvasData data)
        {
            SelectedColor = "#000000";
            CellSize = data.CellSize;
            _navigation = navigation;
            _data = data;
            _data.GenerateCanvas();
        }

        public float CanvasWidth => _data.Cols * CellSize;
        public float CanvasHeight => _data.Rows * CellSize;

        public Color SelectedColorUI => Color.FromArgb(SelectedColor);

        [RelayCommand]
        public void PaintCell(PointF touchPoint)
        {
            if (CanvasWidth == 0 || CanvasHeight == 0)
            {
                return;
            }

            int col = (int)(touchPoint.X / CellSize);
            int row = (int)(touchPoint.Y / CellSize);

            if (col >= 0 && col < _data.Cols && row >= 0 && row < _data.Rows)
            {
                int index = row * _data.Cols + col;
                if (! _data.Pixels[index].Equals(SelectedColor))
                {
                    _data.Pixels[index] = SelectedColor;
                    
                    RequestRedraw?.Invoke();
                }
            }
        }

        [RelayCommand]
        public async Task NewCanvasAsync()
        {
            if (_navigation == null)
            {
                return;
            }

            var tcs = new TaskCompletionSource<bool>();

            await _navigation.PushAsync(new SetupView(_data, tcs));

            bool result = await tcs.Task;

            if (result)
            {
                CellSize = _data.CellSize;
                UpdateDrawableSize?.Invoke(CellSize);
                RequestRedraw?.Invoke();
            }
        }

        [RelayCommand]
        public async Task OpenPaletteAsync()
        {
            var palettePage = new PaletteView(OnColorPicked);
            var paletteWindow = new Window(palettePage)
            {
                Title = "Palette",
                Width = 300,
                Height = 600
            };

            Application.Current.OpenWindow(paletteWindow);
        }

        private void OnColorPicked(string color)
        {
            SelectedColor = color;
        }

        [RelayCommand]
        public async Task ExportCanvasAsync()
        {
            if (_navigation == null)
            {
                return;
            }

            await _navigation.PushAsync(new ExportView(_data));
        }

        [RelayCommand]
        public async Task ImportCanvasAsync()
        {
            if (_navigation == null)
            {
                return;
            }

            var tcs = new TaskCompletionSource<bool>();

            await _navigation.PushAsync(new ImportView(_data, tcs));

            bool result = await tcs.Task;

            if (result)
            {
                CellSize = _data.CellSize;
                UpdateDrawableSize?.Invoke(CellSize);
                RequestRedraw?.Invoke();
            }
        }
    }
}
