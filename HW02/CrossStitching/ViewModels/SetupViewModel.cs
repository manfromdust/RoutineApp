using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CrossStitching.Models;

namespace CrossStitching.ViewModels
{
    public partial class SetupViewModel : ViewModel
    {
        [ObservableProperty]
        CanvasData _dimensions;

        public SetupViewModel()
        {
            _dimensions = CanvasData;
        }

        [RelayCommand]
        public async Task GenerateCanvasAsync()
        {
            //WeakReferenceMessenger.Default.Send(Dimensions);

            await Navigation.PopAsync();
        }
    }
}
