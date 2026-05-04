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
        NotificationRecord notificationToAdd;

        [ObservableProperty]
        NotificationRecordViewModel selectedNotification;

        public NotificationManageViewModel()
        {
            _notificationRepo.OnItemAdded += async (s, e) => Notifications.Add(CreateNotificationRecordViewModel(e));
            _notificationRepo.OnItemUpdated += async (s, e) => Task.Run(async () => await LoadNotificationsAsync());
            _notificationRepo.OnItemRemoved += async (s, e) => Notifications.Remove(Notifications.FirstOrDefault(i => i.Notification.Id == e.Id));
            NotificationToAdd = new NotificationRecord();

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
            NotificationToAdd.RoutineId = RoutineId;
            await NotificationRepo.AddItemAsync(NotificationToAdd);
            //NotificationRecordViewModel newNotificationVM = CreateNotificationRecordViewModel(NotificationToAdd);
            //Notifications.Add(newNotificationVM);
            //SelectedNotification = newNotificationVM;
            NotificationToAdd = new NotificationRecord();
        }
    }
}
