using RoutineApp.ViewModels;

namespace RoutineApp.Views;

public partial class RoutineAddPage : ContentPage
{
	public RoutineAddPage()
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