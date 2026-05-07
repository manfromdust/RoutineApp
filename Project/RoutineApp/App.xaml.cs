using RoutineApp.Repositories;
using RoutineApp.Services;

namespace RoutineApp
{
    public partial class App : Application
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IRoutineItemRepository _routineItemRepository;
        private readonly IQuoteItemRepository _quoteItemRepository;
        private readonly DatabaseInitializer _dbInitializer;

        public App(INotificationRepository notificationRepo,
                   IRoutineItemRepository routineRepo,
                   IQuoteItemRepository quoteRepo,
                   DatabaseInitializer dbInitializer)
        {
            _notificationRepository = notificationRepo;
            _routineItemRepository = routineRepo;
            _quoteItemRepository = quoteRepo;
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }

        protected override async void OnStart()
        {
            base.OnStart();

            await NotificationService.CheckAndRequestPermissionAsync();

            Task.Run(async () =>
            {
                await _dbInitializer.InitializeAsync();

                var activeNotifications = await _notificationRepository.GetActiveItemsAsync();

                foreach (var notification in activeNotifications)
                {
                    var randomQuotes = await _quoteItemRepository.GetRandomQuotes(notification.RoutineId, 30);
                    await NotificationService.ScheduleDailyQuotesAsync(
                        notification.Id,
                        (await _routineItemRepository.GetItemByIdAsync(notification.RoutineId)).Name,
                        notification.TimeOfDay,
                        randomQuotes);
                }
            });
        }
    }
}