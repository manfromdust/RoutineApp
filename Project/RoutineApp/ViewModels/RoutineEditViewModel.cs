using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RoutineApp.Models;
using RoutineApp.Repositories;
using RoutineApp.Views;

namespace RoutineApp.ViewModels
{
    [QueryProperty(nameof(RoutineRepo), "RoutineRepo")]
    [QueryProperty(nameof(QuoteRepo), "QuoteRepo")]
    [QueryProperty(nameof(RoutineItem), "RoutineItem")]
    [QueryProperty(nameof(NotificationRepo), "NotificationRepo")]
    [QueryProperty(nameof(CompletionSource), "CompletionSource")]
    public partial class RoutineEditViewModel : ObservableObject
    {
        private IRoutineItemRepository _routineRepo;
        private IQuoteItemRepository _quoteRepo;
        private INotificationRepository _notificationRepo;
        private RoutineItem _routineItem;
        private TaskCompletionSource<bool> _completionSource;
        private bool _isTaskCompleted = false;

        public IRoutineItemRepository RoutineRepo
        {
            get => _routineRepo;
            set => SetProperty(ref _routineRepo, value);
        }

        public IQuoteItemRepository QuoteRepo
        {
            get => _quoteRepo;
            set => SetProperty(ref _quoteRepo, value);
        }

        public RoutineItem RoutineItem
        {
            get => _routineItem;
            set => SetProperty(ref _routineItem, value);
        }

        public INotificationRepository NotificationRepo
        {
            get => _notificationRepo;
            set => SetProperty(ref _notificationRepo, value);
        }

        public TaskCompletionSource<bool> CompletionSource
        {
            get => _completionSource;
            set => SetProperty(ref _completionSource, value);
        }

        [ObservableProperty]
        public RoutineItem item;

        public RoutineEditViewModel()
        {
            item = RoutineItem;
        }

        public void NotifyDisappered()
        {
            if (!_isTaskCompleted)
            {
                CompletionSource.SetResult(false);
            }
        }

        [RelayCommand]
        public async Task SaveAsync()
        {
            if (string.IsNullOrWhiteSpace(Item.Name))
            {
                var toast = Toast.Make("Routine name cannot be empty.", ToastDuration.Long, 14);
                await toast.Show();
                return;
            }
            await RoutineRepo.UpdateItemAsync(Item);
            CompletionSource.SetResult(true);
            _isTaskCompleted = true;
            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        public async Task ViewQuotesAsync()
        {
            await Shell.Current.GoToAsync(nameof(QuotesEditPage), new Dictionary<string, object>
            {
                { "QuoteRepo", QuoteRepo },
                { "RoutineId", Item.Id }
            });
        }

        public async Task ManageNotificationsAsync()
        {
            await Shell.Current.GoToAsync(nameof(NotificationsPage), new Dictionary<string, object>
            {
                { "NotificationRepo", NotificationRepo },
                { "RoutineId", Item.Id }
            });
        }
}
