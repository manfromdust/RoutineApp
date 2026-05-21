using RoutineApp.ViewModels;

namespace RoutineApp.Views;

public partial class NotificationsPage : ContentPage
{
	public NotificationsPage(NotificationManageViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
    }
}