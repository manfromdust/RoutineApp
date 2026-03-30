using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CrossStitching.Models;

namespace CrossStitching.ViewModels
{
    public partial class SetupViewModel : ViewModel
    {
        [ObservableProperty]
        CanvasDimensions _dimensions;

        public SetupViewModel()
        {
            _dimensions = new CanvasDimensions();
        }

        [RelayCommand]
        public async Task GenerateCanvasAsync()
        {
            WeakReferenceMessenger.Default.Send(Dimensions);
            await Navigation.PopAsync();
        }
    }
}
