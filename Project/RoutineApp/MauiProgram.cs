using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using Plugin.LocalNotification;
using RoutineApp.Repositories;

namespace RoutineApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .UseLocalNotification()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton<IRoutineItemRepository, RoutineItemRepository>();
            builder.Services.AddSingleton<IQuoteItemRepository, QuoteItemRepository>();
            builder.Services.AddSingleton<INotificationRepository, NotificationRepository>();
            builder.Services.AddTransient<ViewModels.MainViewModel>();
            builder.Services.AddTransient<Views.MainPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
