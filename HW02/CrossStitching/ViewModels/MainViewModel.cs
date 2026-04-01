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

        [ObservableProperty]
        private ObservableCollection<PixelViewModel> _pixels;

        [ObservableProperty]
        private int _columns;

        [ObservableProperty]
        private Color _selectedColor;

        [ObservableProperty]
        private ObservableCollection<PaletteViewModel> _palette;

        public MainViewModel()
        {
            Pixels = new ObservableCollection<PixelViewModel>();
            Palette = new ObservableCollection<PaletteViewModel>();
            SelectedColor = Colors.White;
            _ = FillPaletteAsync();
            //WeakReferenceMessenger.Default.Register<CanvasData>(this, (r, m) =>
            //                                                { CreateCanvas(m.Rows, m.Cols); });
        }

        private async Task FillPaletteAsync()
        {
            Palette.Clear();
            var threadColors = await LoadCustomColors.LoadColorsAsync();
            foreach (var threadColor in threadColors)
            {
                Palette.Add(new PaletteViewModel { ThreadColor = threadColor });
            }
        }

        public void CreateCanvas()
        {
            Pixels.Clear();
            _cols = CanvasData.Cols;
            _rows = CanvasData.Rows;
            Columns = _cols;
            
            for (int i = 0; i < _rows * _cols; i++)
            {
                Pixels.Add(new PixelViewModel { Pixel = new Pixel() });
            }
        }

        private void CreateCanvasFromImport()
        {
            Pixels.Clear();
            _cols = CanvasData.Cols;
            _rows = CanvasData.Rows;
            Columns = _cols;

            foreach (var colorHex in CanvasData.Pixels)
            {
                var pixel = new Pixel { Color = Color.Parse(colorHex) };
                Pixels.Add(new PixelViewModel { Pixel = pixel });
            }
        }

        [RelayCommand]
        public async Task NewCanvasAsync()
        {
            await Navigation.PushAsync(new SetupView());
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
            CanvasData.Pixels = Pixels.Select(p => p.Pixel.Color.ToHex()).ToList();
            await Navigation.PushAsync(new ExportView());
        }

        [RelayCommand]
        public async Task ImportCanvasAsync()
        {
            var tcs = new TaskCompletionSource<bool>();
            await Navigation.PushAsync(new ImportView(tcs));

            if (await tcs.Task)
            {
                CreateCanvasFromImport();
            }
        }
    }
}
