using Microsoft.Extensions.Logging;



namespace CrossStitching
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })
                .RegisterViewModels()
                .RegisterViews();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }

        public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder app)
        {
            app.Services.AddTransient<ViewModels.MainViewModel>();
            app.Services.AddTransient<ViewModels.SetupViewModel>();

            return app;
        }

        public static MauiAppBuilder RegisterViews(this MauiAppBuilder app)
        {
            app.Services.AddTransient<Views.MainView>();
            app.Services.AddTransient<Views.SetupView>();
            return app;
        }
    }
}
