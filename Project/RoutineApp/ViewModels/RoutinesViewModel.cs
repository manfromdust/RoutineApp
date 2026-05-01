using CommunityToolkit.Mvvm.ComponentModel;
using RoutineApp.Models;
using RoutineApp.Repositories;
using System.Collections.ObjectModel;

namespace RoutineApp.ViewModels
{
    public partial class RoutinesViewModel : ObservableObject
    {
        private readonly INavigation _navigation;
        private readonly IRoutineItemRepository _routineRepo;
        private readonly IQuoteItemRepository _quoteRepo;

        [ObservableProperty]
        ObservableCollection<RoutineItemViewModel> items;

        public RoutinesViewModel(INavigation navigation,
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
    }
}
