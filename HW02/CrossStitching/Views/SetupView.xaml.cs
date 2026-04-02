using CrossStitching.Models;

namespace CrossStitching.Views;

public partial class SetupView : ContentPage
{
	public SetupView(TaskCompletionSource<bool> tcs, CanvasData data)
	{
		InitializeComponent();
		BindingContext = new ViewModels.SetupViewModel(this.Navigation, tcs, data);
    }

	protected override void OnDisappearing()
	{
		base.OnDisappearing();
		if (BindingContext is ViewModels.SetupViewModel viewModel)
		{
			viewModel.NotifyDisappeared();
		}
    }
}