using CommunityToolkit.Mvvm.ComponentModel;
using CrossStitching.Models;
using System.Windows.Input;

namespace CrossStitching.ViewModels
{
    public partial class PaletteViewModel : ViewModel
    {
        [ObservableProperty]
        private ThreadColor _threadColor;

        public ICommand DifferentThreadColorCommand { get; set; }
    }
}
