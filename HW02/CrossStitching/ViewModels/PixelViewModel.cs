using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CrossStitching.Models;

namespace CrossStitching.ViewModels
{
    public partial class PixelViewModel : ViewModel
    {
        [ObservableProperty]
        private Pixel _pixel;

        public void SetColor(Color color)
        {
            Pixel.Color = color;
        }
    }
}
