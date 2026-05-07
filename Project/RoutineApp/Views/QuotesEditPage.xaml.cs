using RoutineApp.ViewModels;

namespace RoutineApp.Views;

public partial class QuotesEditPage : ContentPage
{
	public QuotesEditPage(QuotesEditViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        if (BindingContext is QuotesEditViewModel vm)
        {
            vm.NotifyDisappered();
        }
    }
}