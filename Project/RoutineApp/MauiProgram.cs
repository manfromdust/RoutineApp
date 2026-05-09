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
            builder.Services.AddSingleton<ViewModels.MainViewModel>();
            builder.Services.AddSingleton<Views.MainPage>();
            builder.Services.AddTransient<ViewModels.RoutineEditViewModel>();
            builder.Services.AddTransient<Views.RoutineEditPage>();
            builder.Services.AddTransient<ViewModels.NotificationManageViewModel>();
            builder.Services.AddTransient<Views.NotificationsPage>();
            builder.Services.AddTransient<ViewModels.QuotesEditViewModel>();
            builder.Services.AddTransient<Views.QuotesEditPage>();
            builder.Services.AddTransient<ViewModels.RoutineAddViewModel>();
            builder.Services.AddTransient<Views.RoutineAddPage>();
            builder.Services.AddSingleton<ViewModels.SettingsViewModel>();
            builder.Services.AddSingleton<Views.SettingsPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
