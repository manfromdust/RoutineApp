using RoutineApp.ViewModels;

namespace RoutineApp.Views;

public partial class QuotesEditPage : ContentPage
{
	public QuotesEditPage(QuotesEditViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}