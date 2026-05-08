using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RoutineApp.Models;
using RoutineApp.Repositories;
using RoutineApp.Services;
using RoutineApp.Views;

namespace RoutineApp.ViewModels
{
    [QueryProperty(nameof(RoutineItem), "RoutineItem")]
    public partial class RoutineEditViewModel : ObservableObject
    {
        private IRoutineItemRepository _routineRepo;
        private IQuoteItemRepository _quoteRepo;
        private INotificationRepository _notificationRepo;
        private RoutineItem _routineItem;

        public RoutineItem RoutineItem
        {
            get => _routineItem;
            set => SetProperty(ref _routineItem, value);
        }

        [ObservableProperty]
        string newName = string.Empty;

        public RoutineEditViewModel(IRoutineItemRepository routineRepo,
                                    IQuoteItemRepository quoteRepo,
                                    INotificationRepository notificationRepo)
        {
            NewName = RoutineItem?.Name ?? string.Empty;
            _routineRepo = routineRepo;
            _quoteRepo = quoteRepo;
            _notificationRepo = notificationRepo;
        }

        [RelayCommand]
        public async Task SaveAsync()
        {
            if (string.IsNullOrWhiteSpace(NewName))
            {
                var toast = Toast.Make("Routine name cannot be empty.", ToastDuration.Long, 14);
                await toast.Show();
                return;
            }
            RoutineItem.Name = NewName;
            await _routineRepo.UpdateItemAsync(RoutineItem);
            var toast_s = Toast.Make("Routine renamed successfully.", ToastDuration.Long, 14);
            await toast_s.Show();
        }

        [RelayCommand]
        public async Task ViewQuotesAsync()
        {
            await Shell.Current.GoToAsync(nameof(QuotesEditPage), new Dictionary<string, object>
            {
                { "RoutineId", RoutineItem.Id }
            });
        }

        [RelayCommand]
        public async Task RegenerateQuotesNotificationAsync()
        {
            await Task.Run(async () =>
            {
                var notifications = await _notificationRepo.GetItemsAsync(RoutineItem.Id);
                foreach (var notification in notifications)
                {
                    var randomQuotes = await _quoteRepo.GetRandomQuotes(RoutineItem.Id, 30); ;
                    await NotificationService.RefreshDailyQuotesAsync(notification.Id,
                                                                      RoutineItem.Name,
                                                                      notification.TimeOfDay,
                                                                      randomQuotes);
                }
            });
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                var toast = Toast.Make("Notifications successfully set up.", ToastDuration.Long, 14);
                await toast.Show();
            });
        }

        [RelayCommand]
        public async Task ManageNotificationsAsync()
        {
            await Shell.Current.GoToAsync(nameof(NotificationsPage), new Dictionary<string, object>
            {
                { "RoutineId", RoutineItem.Id }
            });
        }

        [RelayCommand]
        public async Task DeleteRoutineAsync()
        {
            bool isConfirmed = await Shell.Current.DisplayAlertAsync("Confirm Delete", "Are you sure you want to delete this routine?", "Yes", "No");

            if (isConfirmed)
            {
                var notifications = await _notificationRepo.GetItemsAsync(RoutineItem.Id);
                foreach (var notification in notifications)
                {
                    NotificationService.CancelNotifications(notification.Id);
                    await _notificationRepo.RemoveItemAsync(notification);
                }
                await _routineRepo.RemoveItemAsync(RoutineItem);
                var toast = Toast.Make("Routine and its notifications removed successfully.", ToastDuration.Long, 14);
                await toast.Show();
                await Shell.Current.GoToAsync("..");
            }
        }
    }
}
