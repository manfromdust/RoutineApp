using CommunityToolkit.Mvvm.ComponentModel;
using CrossStitching.Models;

namespace CrossStitching.ViewModels
{
    public partial class PaletteViewModel : ViewModel
    {
        [ObservableProperty]
        private ThreadColor _threadColor;
    }
}
