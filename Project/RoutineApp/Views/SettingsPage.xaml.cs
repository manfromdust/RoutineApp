using RoutineApp.ViewModels;

namespace RoutineApp.Views;

public partial class SettingsPage : ContentPage
{
	public SettingsPage()
	{
		InitializeComponent();
	}

    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        if (BindingContext is RoutineAddViewModel viewModel)
        {
            viewModel.NotifyDisappered();
        }
    }
}