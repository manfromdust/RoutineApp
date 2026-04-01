using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CrossStitching.Models;
using CrossStitching.Services;
using CrossStitching.Views;
using System.Collections.ObjectModel;
using static Android.InputMethodServices.Keyboard;

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
    }
}
