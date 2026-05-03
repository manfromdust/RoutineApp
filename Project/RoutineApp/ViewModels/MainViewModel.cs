using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RoutineApp.Models;
using RoutineApp.Views;
using RoutineApp.Repositories;
using System.Collections.ObjectModel;

namespace RoutineApp.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly INavigation _navigation;
        private readonly IRoutineItemRepository _routineRepo;
        private readonly IQuoteItemRepository _quoteRepo;

        [ObservableProperty]
        ObservableCollection<RoutineItemViewModel> items;

        [ObservableProperty]
        RoutineItemViewModel selectedItem;

        public MainViewModel(INavigation navigation,
                                 IRoutineItemRepository routineRepo,
                                 IQuoteItemRepository quoteRepo)
        {
            _navigation = navigation;
            _routineRepo = routineRepo;
            _quoteRepo = quoteRepo;

            _routineRepo.OnItemAdded += async (s, e) => Items.Add(CreateRoutineItemViewModel(e));
            _routineRepo.OnItemUpdated += async (s, e) => Task.Run(async () => await LoadDataAsync());
            _routineRepo.OnItemRemoved += async (s, e) => Items.Remove(Items.FirstOrDefault(i => i.Item.Id == e.Id));

            Task.Run(async () => await LoadDataAsync());
        }

        private async Task LoadDataAsync()
        {
            var routines = await _routineRepo.GetItemsAsync();
            var routineViewModels = routines.Select(r => CreateRoutineItemViewModel(r)).ToList();
            Items = new ObservableCollection<RoutineItemViewModel>(routineViewModels);
        }

        private RoutineItemViewModel CreateRoutineItemViewModel(RoutineItem item)
        {
            return new RoutineItemViewModel(item);
        }

        [RelayCommand]
        public async Task AddRoutineAsync()
        {
            var tcs = new TaskCompletionSource<bool>();

            await _navigation.PushAsync(new RoutineAddPage(_routineRepo, tcs));

            bool result = await tcs.Task;

            if (result)
            {
                await _navigation.PushAsync(new RoutineEditPage(_quoteRepo));
                Task.Run(async () => await LoadDataAsync());
            }
        }
    }
}
