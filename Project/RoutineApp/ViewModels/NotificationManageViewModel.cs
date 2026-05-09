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
    public partial class NotificationManageViewModel : ObservableObject
    {
        private INotificationRepository _notificationRepo;
        private IQuoteItemRepository _quoteRepo;

        [ObservableProperty]
        ObservableCollection<NotificationRecordViewModel> notifications = new();

        [ObservableProperty]
        int routineId;

        [ObservableProperty]
        TimeSpan notificationToAdd;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(ActiveButtonText))]
        NotificationRecordViewModel selectedNotification;

        public NotificationManageViewModel(INotificationRepository notificationRepository,
                                           IQuoteItemRepository quoteItemRepository)
        {
            _notificationRepo = notificationRepository;
            _quoteRepo = quoteItemRepository;

            _notificationRepo.OnItemAdded += async (s, e) => Notifications.Add(CreateNotificationRecordViewModel(e));
            _notificationRepo.OnItemAdded += async (s, e) => NotificationService.ScheduleDailyQuotesAsync(e.Id,
                                                                                                          "Routine",
                                                                                                          e.TimeOfDay,
                                                                                                          _quoteRepo.GetRandomQuotes(RoutineId,
                                                                                                          NotificationService.DAYS_TO_SCHEDULE).Result);
            _notificationRepo.OnItemUpdated += async (s, e) => MainThread.BeginInvokeOnMainThread(async () => await LoadNotificationsAsync());
            _notificationRepo.OnItemRemoved += async (s, e) => Notifications.Remove(Notifications.FirstOrDefault(i => i.Notification.Id == e.Id));
            _notificationRepo.OnItemRemoved += async (s, e) => NotificationService.CancelNotifications(e.Id);

            var now = DateTime.Now.TimeOfDay;
            NotificationToAdd = new TimeSpan(now.Hours, now.Minutes, 0);
        }

        partial void OnRoutineIdChanged(int value)
        {
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
                Task.Run(() => NotificationService.CancelNotifications(SelectedNotification.Notification.Id));
            }
            else
            {
                var randomQuotes = await _quoteRepo.GetRandomQuotes(RoutineId, NotificationService.DAYS_TO_SCHEDULE);
                Task.Run(async () => await NotificationService.ScheduleDailyQuotesAsync(SelectedNotification.Notification.Id,
                                                                                        "Routine",
                                                                                        SelectedNotification.Notification.TimeOfDay,
                                                                                        randomQuotes));
            }
            SelectedNotification.Notification.Active = !SelectedNotification.Notification.Active;
            await _notificationRepo.UpdateItemAsync(SelectedNotification.Notification);
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
            await _notificationRepo.AddItemAsync(newNotification);
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
            await _notificationRepo.RemoveItemAsync(SelectedNotification.Notification);
            SelectedNotification = null;
            var toastSuccess = Toast.Make("Notification removed", ToastDuration.Short, 14);
            await toastSuccess.Show();
        }
    }
}
