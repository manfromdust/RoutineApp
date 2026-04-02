using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CrossStitching.Models;
using System.Windows.Input;

namespace CrossStitching.ViewModels
{
    public partial class PixelViewModel : ViewModel
    {
        [ObservableProperty]
        private Pixel _pixel;

        public ICommand ChangeColorCommand { get; set; }
    }
}
