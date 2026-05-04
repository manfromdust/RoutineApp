using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RoutineApp.Models;
using RoutineApp.Repositories;
using System.Collections.ObjectModel;

namespace RoutineApp.ViewModels
{
    [QueryProperty(nameof(RoutineId), "RoutineId")]
    [QueryProperty(nameof(NotificationRepo), "NotificationRepo")]
    public partial class NotificationManageViewModel : ObservableObject
    {
        private int _routineId;
        private INotificationRepository _notificationRepo;

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

        [ObservableProperty]
        ObservableCollection<NotificationRecordViewModel> notifications;

        [ObservableProperty]
        TimeSpan notificationToAdd;

        [ObservableProperty]
        NotificationRecordViewModel selectedNotification;

        public NotificationManageViewModel()
        {
            _notificationRepo.OnItemAdded += async (s, e) => Notifications.Add(CreateNotificationRecordViewModel(e));
            _notificationRepo.OnItemUpdated += async (s, e) => Task.Run(async () => await LoadNotificationsAsync());
            _notificationRepo.OnItemRemoved += async (s, e) => Notifications.Remove(Notifications.FirstOrDefault(i => i.Notification.Id == e.Id));

            var now = DateTime.Now.TimeOfDay;
            NotificationToAdd = new TimeSpan(now.Hours, now.Minutes, 0);

            Task.Run(async () => await LoadNotificationsAsync());
        }

        private async Task LoadNotificationsAsync()
        {
            var notifications = await _notificationRepo.GetItemsAsync(RoutineId);
            var notificationViewModels = notifications.Select(n => CreateNotificationRecordViewModel(n)).ToList();
            Notifications = new ObservableCollection<NotificationRecordViewModel>(notificationViewModels);
        }

        private NotificationRecordViewModel CreateNotificationRecordViewModel(NotificationRecord record)
        {
            return new NotificationRecordViewModel(record);
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
            //NotificationRecordViewModel newNotificationVM = CreateNotificationRecordViewModel(newNotification);
            //Notifications.Add(newNotificationVM);
            //SelectedNotification = newNotificationVM;
            var toast = Toast.Make("Notification added", ToastDuration.Short, 14);
            await toast.Show();
        }
    }
}
