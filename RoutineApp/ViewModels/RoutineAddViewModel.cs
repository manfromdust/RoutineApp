using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RoutineApp.Models;
using RoutineApp.Repositories;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;

namespace RoutineApp.ViewModels
{
    public partial class RoutineAddViewModel : ObservableObject
    {
        private IRoutineItemRepository _routineRepo;

        [ObservableProperty]
        public string item;

        public RoutineAddViewModel(IRoutineItemRepository routineRepo)
        {
            _routineRepo = routineRepo;
        }

        [RelayCommand]
        public async Task SaveAsync()
        {
            if (string.IsNullOrWhiteSpace(Item))
            {
                var toast = Toast.Make("Routine name cannot be empty.", ToastDuration.Long, 14);
                await toast.Show();

                return;
            }
            await _routineRepo.AddItemAsync(new RoutineItem { Name = Item });
            await Shell.Current.GoToAsync("..");
        }
    }
}
