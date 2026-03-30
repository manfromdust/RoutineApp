using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

using CrossStitching.Models;
using CommunityToolkit.Mvvm.Messaging;
using CrossStitching.Views;
using CrossStitching.Services;

namespace CrossStitching.ViewModels
{
    public partial class MainViewModel : ViewModel
    {
        private readonly IServiceProvider _serviceProvider;

        [ObservableProperty]
        private ObservableCollection<PixelViewModel> _pixels;

        [ObservableProperty]
        private int _columns;

        [ObservableProperty]
        private Color _selectedColor;

        [ObservableProperty]
        private ObservableCollection<PaletteViewModel> _palette;

        public MainViewModel(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            Pixels = new ObservableCollection<PixelViewModel>();
            Palette = new ObservableCollection<PaletteViewModel>();
            _ = FillPaletteAsync();
            WeakReferenceMessenger.Default.Register<CanvasDimensions>(this, (r, m) =>
                                                            { CreateCanvas(m.Rows, m.Cols); });
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

        public void CreateCanvas(int rows, int cols)
        {
            Columns = cols;
        }

        [RelayCommand]
        public async Task NewCanvasAsync()
        {
            await Navigation.PushAsync(_serviceProvider.GetRequiredService<SetupView>());
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
