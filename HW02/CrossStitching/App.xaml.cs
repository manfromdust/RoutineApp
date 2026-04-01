using Microsoft.Extensions.DependencyInjection;

namespace CrossStitching
{
    public partial class App : Application
    {
        private readonly Page _initialPage;
        public App(Views.MainView view)
        {
            InitializeComponent();

            _initialPage = new NavigationPage(view);

        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(_initialPage);
        }
    }
}