using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CrossStitching.Models;
using CrossStitching.Services;
using CrossStitching.Views;
using System.Collections.ObjectModel;


namespace CrossStitching.ViewModels
{
    public partial class MainViewModel : ViewModel
    {
        private int _rows;
        private int _cols;
        private readonly INavigation? _navigation;
        private CanvasData _data;

        [ObservableProperty]
        private ObservableCollection<PixelViewModel> _pixels;

        [ObservableProperty]
        private int _columns;

        [ObservableProperty]
        private Color _selectedColor;

        [ObservableProperty]
        private ObservableCollection<PaletteViewModel> _palette;

        public MainViewModel(INavigation navigation, CanvasData data)
        {
            Pixels = new ObservableCollection<PixelViewModel>();
            Palette = new ObservableCollection<PaletteViewModel>();
            SelectedColor = Colors.Red;
            _navigation = navigation;
            _data = data;
            _ = FillPaletteAsync();
        }

        private async Task FillPaletteAsync()
        {
            Palette.Clear();
            var threadColors = await LoadCustomColors.LoadColorsAsync();
            MainThread.BeginInvokeOnMainThread(() =>
            {
                foreach (var threadColor in threadColors)
                {
                    Palette.Add(new PaletteViewModel { ThreadColor = threadColor,
                                                       DifferentThreadColorCommand = this.DifferentThreadColorCommand});
                }
            });
        }

        public async Task CreateCanvasAsync()
        {
            _cols = _data.Cols;
            _rows = _data.Rows;

            var temp = await Task.Run(() =>
            {
                var list = new List<PixelViewModel>(_rows * _cols);
                for (int i = 0; i < _rows * _cols; i++)
                {
                    list.Add(new PixelViewModel
                    {
                        Pixel = new Pixel(),
                        ChangeColorCommand = this.ChangeColorCommand
                    });
                }
                return list;
            });

            MainThread.BeginInvokeOnMainThread(() =>
            {
                Columns = _cols;

                Pixels = new ObservableCollection<PixelViewModel>(temp);
            });
        }

        private void CreateCanvasFromImport()
        {
            Pixels.Clear();
            _cols = _data.Cols;
            _rows = _data.Rows;
            Columns = _cols;

            foreach (var colorHex in _data.Pixels)
            {
                var pixel = new Pixel { Color = Color.Parse(colorHex) };
                Pixels.Add(new PixelViewModel { Pixel = pixel });
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

                await CreateCanvasAsync();
            }
        }

        [RelayCommand]
        public async Task ChangeColorAsync(PixelViewModel pixel)
        {
            pixel.Pixel.Color = SelectedColor;
        }

        [RelayCommand]
        public async Task DifferentThreadColorAsync(PaletteViewModel palette)
        {
            SelectedColor = palette.ThreadColor.Color;
        }

        [RelayCommand]
        public async Task ExportCanvasAsync()
        {
            if (_navigation == null)
            {
                return;
            }

            _data.Pixels = Pixels.Select(p => p.Pixel.Color.ToHex()).ToList();
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
                CreateCanvasFromImport();
            }
        }
    }
}
