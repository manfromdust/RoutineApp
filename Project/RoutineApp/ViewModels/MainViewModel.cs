using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RoutineApp.Models;
using RoutineApp.Views;
using RoutineApp.Repositories;
using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using RoutineApp.Services;

namespace RoutineApp.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly IRoutineItemRepository _routineRepo;
        private readonly IQuoteItemRepository _quoteRepo;
        private readonly INotificationRepository _notificationRepo;

        [ObservableProperty]
        ObservableCollection<RoutineItemViewModel> items = new();

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(ActiveButtonText))]
        RoutineItemViewModel selectedItem;

        public MainViewModel(IRoutineItemRepository routineRepo,
                             IQuoteItemRepository quoteRepo,
                             INotificationRepository notificationRepo)
        {
            _routineRepo = routineRepo;
            _quoteRepo = quoteRepo;
            _notificationRepo = notificationRepo;

            _routineRepo.OnItemAdded += async (s, e) => Items.Add(CreateRoutineItemViewModel(e));
            _routineRepo.OnItemUpdated += (s, e) => MainThread.BeginInvokeOnMainThread(async () => await LoadDataAsync());
            _routineRepo.OnItemRemoved += async (s, e) => Items.Remove(Items.FirstOrDefault(i => i.Item.Id == e.Id));

            MainThread.BeginInvokeOnMainThread(async () => await LoadDataAsync());
        }

        public string ActiveButtonText => SelectedItem != null && SelectedItem.Item.Active ? "Deactivate" : "Activate";

        private async Task LoadDataAsync()
        {
            await DatabaseInitializer.WaitForReadyAsync();
            var routines = await _routineRepo.GetItemsAsync();
            MainThread.BeginInvokeOnMainThread(() =>
            {
                Items.Clear();
                foreach (var routine in routines)
                {
                    Items.Add(CreateRoutineItemViewModel(routine));
                }
            });
        }

        private RoutineItemViewModel CreateRoutineItemViewModel(RoutineItem item)
        {
            return new RoutineItemViewModel(item);
        }

        [RelayCommand]
        public async Task AddRoutineAsync()
        {
            await Shell.Current.GoToAsync(nameof(RoutineAddPage));
        }

        [RelayCommand]
        public async Task ChangeActiveAsync()
        {
            if (SelectedItem == null)
            {
                var toast = Toast.Make("Please select a routine to change active status.", ToastDuration.Short, 14);
                await toast.Show();
                return;
            }
            var notifications = await _notificationRepo.GetItemsAsync(SelectedItem.Item.Id);
            if (SelectedItem.Item.Active)
            {
                foreach (var notification in notifications)
                {
                    NotificationService.CancelNotifications(notification.Id);
                }
            } else
            {
                foreach (var notification in notifications)
                {
                    var randomQuotes = await _quoteRepo.GetRandomQuotes(SelectedItem.Item.Id, 30);
                    await NotificationService.ScheduleDailyQuotesAsync(notification.Id,
                                                                       SelectedItem.Item.Name,
                                                                       notification.TimeOfDay,
                                                                       randomQuotes);
                }
            }
            SelectedItem.Item.Active = !SelectedItem.Item.Active;
            await _routineRepo.UpdateItemAsync(SelectedItem.Item);
            OnPropertyChanged(nameof(ActiveButtonText));
        }

        [RelayCommand]
        public async Task EditRoutineAsync()
        {
            if (SelectedItem == null)
            {
                var toast = Toast.Make("Please select a routine to manage.", ToastDuration.Short, 14);
                await toast.Show();
                return;
            }

            await Shell.Current.GoToAsync(nameof(RoutineEditPage), new Dictionary<string, object>
            {
                { "RoutineItem", SelectedItem.Item }
            });

            SelectedItem = null;
        }
    }
}
