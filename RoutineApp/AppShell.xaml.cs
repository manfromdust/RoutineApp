namespace RoutineApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(Views.RoutineAddPage), typeof(Views.RoutineAddPage));
            Routing.RegisterRoute(nameof(Views.RoutineEditPage), typeof(Views.RoutineEditPage));
            Routing.RegisterRoute(nameof(Views.QuotesEditPage), typeof(Views.QuotesEditPage));
            Routing.RegisterRoute(nameof(Views.NotificationsPage), typeof(Views.NotificationsPage));
        }
    }
}
