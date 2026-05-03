using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RoutineApp.Models;
using RoutineApp.Repositories;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;

namespace RoutineApp.ViewModels
{
    [QueryProperty(nameof(RoutineRepo), "RoutineRepo")]
    [QueryProperty(nameof(CompletionSource), "CompletionSource")]
    public partial class RoutineAddViewModel : ObservableObject
    {
        private IRoutineItemRepository _routineRepo;
        private TaskCompletionSource<bool> _completionSource;
        private bool _isTaskCompleted = false;

        public IRoutineItemRepository RoutineRepo
        {
            get => _routineRepo;
            set => SetProperty(ref _routineRepo, value);
        }

        public TaskCompletionSource<bool> CompletionSource
        {
            get => _completionSource;
            set => SetProperty(ref _completionSource, value);
        }

        [ObservableProperty]
        public RoutineItem item = new();

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
            await RoutineRepo.AddItemAsync(Item);
            CompletionSource.SetResult(true);
            _isTaskCompleted = true;
            await Shell.Current.GoToAsync("..");
        }
    }
}
