using RoutineApp.ViewModels;

namespace RoutineApp.Views;

public partial class RoutineAddPage : ContentPage
{
	public RoutineAddPage(RoutineAddViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
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