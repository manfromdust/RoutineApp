using RoutineApp.ViewModels;

namespace RoutineApp.Views;

public partial class RoutineEditPage : ContentPage
{
	public RoutineEditPage()
	{
		InitializeComponent();
	}

	protected override void OnDisappearing()
	{
		base.OnDisappearing();
		if (BindingContext is RoutineEditViewModel vm)
		{
			vm.NotifyDisappered();
		}
    }
}