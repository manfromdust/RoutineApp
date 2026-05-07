using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RoutineApp.Models;
using RoutineApp.Repositories;
using RoutineApp.Services;
using System.Collections.ObjectModel;

namespace RoutineApp.ViewModels
{
    [QueryProperty(nameof(RoutineId), "RoutineId")]
    [QueryProperty(nameof(NotificationRepo), "NotificationRepo")]
    [QueryProperty(nameof(QuoteRepo), "QuoteRepo")]
    public partial class NotificationManageViewModel : ObservableObject
    {
        private int _routineId;
        private INotificationRepository _notificationRepo;
        private IQuoteItemRepository _quoteItemRepository;

        public int RoutineId
        {
            get => _routineId;
            set => SetProperty(ref _routineId, value);
        }

        public INotificationRepository NotificationRepo
        {
            get => _notificationRepo;
            set => SetProperty(ref _notificationRepo, value);
        }

        public IQuoteItemRepository QuoteRepo
        {
            get => _quoteItemRepository;
            set => SetProperty(ref _quoteItemRepository, value);
        }

        [ObservableProperty]
        ObservableCollection<NotificationRecordViewModel> notifications = new();

        [ObservableProperty]
        TimeSpan notificationToAdd;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(ActiveButtonText))]
        NotificationRecordViewModel selectedNotification;

        public NotificationManageViewModel()
        {
            _notificationRepo.OnItemAdded += async (s, e) => Notifications.Add(CreateNotificationRecordViewModel(e));
            _notificationRepo.OnItemUpdated += async (s, e) => Task.Run(async () => await LoadNotificationsAsync());
            _notificationRepo.OnItemRemoved += async (s, e) => Notifications.Remove(Notifications.FirstOrDefault(i => i.Notification.Id == e.Id));

            var now = DateTime.Now.TimeOfDay;
            NotificationToAdd = new TimeSpan(now.Hours, now.Minutes, 0);

            MainThread.BeginInvokeOnMainThread(async () => await LoadNotificationsAsync());
        }

        public string ActiveButtonText => SelectedNotification != null && SelectedNotification.Notification.Active ? "Deactivate" : "Activate";

        private async Task LoadNotificationsAsync()
        {
            var notifications = await _notificationRepo.GetItemsAsync(RoutineId);
            Notifications.Clear();
            foreach (var notification in notifications)
            {
                Notifications.Add(CreateNotificationRecordViewModel(notification));
            }
        }

        private NotificationRecordViewModel CreateNotificationRecordViewModel(NotificationRecord record)
        {
            return new NotificationRecordViewModel(record);
        }

        [RelayCommand]
        public async Task ChangeActiveAsync()
        {
            if (SelectedNotification == null)
            {
                var toast = Toast.Make("Please select a notification to change.", ToastDuration.Short, 14);
                await toast.Show();
                return;
            }
            if (SelectedNotification.Notification.Active)
            {
                NotificationService.CancelNotifications(SelectedNotification.Notification.Id);
            }
            else
            {
                var randomQuotes = await QuoteRepo.GetRandomQuotes(RoutineId, 30);
                await NotificationService.ScheduleDailyQuotesAsync(SelectedNotification.Notification.Id,
                                                                   "Your Routine",
                                                                   SelectedNotification.Notification.TimeOfDay,
                                                                   randomQuotes);
            }
            SelectedNotification.Notification.Active = !SelectedNotification.Notification.Active;
            await NotificationRepo.UpdateItemAsync(SelectedNotification.Notification);
            var toastSuccess = Toast.Make("Notification updated", ToastDuration.Short, 14);
            await toastSuccess.Show();
        }

        [RelayCommand]
        public async Task AddNotificationAsync()
        {
            var newNotification = new NotificationRecord
            {
                RoutineId = RoutineId,
                TimeOfDay = NotificationToAdd,
            };
            await NotificationRepo.AddItemAsync(newNotification);
            var toast = Toast.Make("Notification added", ToastDuration.Short, 14);
            await toast.Show();
        }

        [RelayCommand]
        public async Task RemoveNotificationAsync()
        {
            if (SelectedNotification == null)
            {
                var toast = Toast.Make("Please select a notification to remove.", ToastDuration.Short, 14);
                await toast.Show();
                return;
            }
            await NotificationRepo.RemoveItemAsync(SelectedNotification.Notification);
            SelectedNotification = null;
            var toastSuccess = Toast.Make("Notification removed", ToastDuration.Short, 14);
            await toastSuccess.Show();
        }
    }
}
