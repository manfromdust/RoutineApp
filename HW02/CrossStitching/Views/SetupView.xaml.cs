using CrossStitching.Models;
using CrossStitching.ViewModels;

namespace CrossStitching.Views;

public partial class SetupView : ContentPage
{
	public SetupView(CanvasData data, TaskCompletionSource<bool> tcs)
	{
		InitializeComponent();
		BindingContext = new ViewModels.SetupViewModel(this.Navigation, data, tcs);
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        if (BindingContext is SetupViewModel vm)
        {
            vm.NotifyDisappeared();
        }
    }
}