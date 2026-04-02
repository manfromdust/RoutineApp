using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CrossStitching.Models;
using CrossStitching.Views;
using System.Collections.ObjectModel;


namespace CrossStitching.ViewModels
{
    public partial class MainViewModel : ViewModel
    {
        private readonly INavigation? _navigation;
        private CanvasData _data;

        public Action RequestRedraw { get; set; } 

        [ObservableProperty]
        private float _cellSize;

        [ObservableProperty]
        private string _selectedColor;


        public MainViewModel(INavigation navigation, CanvasData data)
        {
            SelectedColor = "#000000";
            CellSize = 12f;
            _navigation = navigation;
            _data = data;
            _data.GenerateCanvas();
        }

        public float CanvasWidth => _data.Cols * CellSize;
        public float CanvasHeight => _data.Rows * CellSize;

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

            await _navigation.PushAsync(new SetupView(tcs, _data));

            if (await tcs.Task)
            {
                await Task.Delay(100); // Ensure the UI has time to update before creating the canvas

                //await CreateCanvasAsync();
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

            //_data.Pixels = Pixels.Select(p => p.Pixel.Color.ToHex()).ToList();
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
            await _navigation.PushAsync(new ImportView(tcs, _data));

            if (await tcs.Task)
            {
                //CreateCanvasFromImport();
            }
        }
    }
}
