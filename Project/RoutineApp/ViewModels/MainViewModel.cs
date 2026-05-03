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
        private readonly IRoutineItemRepository _routineRepo;
        private readonly IQuoteItemRepository _quoteRepo;

        [ObservableProperty]
        ObservableCollection<RoutineItemViewModel> items;

        [ObservableProperty]
        RoutineItemViewModel selectedItem;

        public MainViewModel(IRoutineItemRepository routineRepo,
                             IQuoteItemRepository quoteRepo)
        {
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

            await Shell.Current.GoToAsync(nameof(RoutineAddPage), new Dictionary<string, object>
            {
                { "RoutineRepo", _routineRepo },
                { "CompletionSource", tcs }
            });

            bool result = await tcs.Task;

            if (result)
            {
                Task.Run(async () => await LoadDataAsync());
            }
        }

        [RelayCommand]
        public async Task EditRoutineAsync(RoutineItemViewModel item)
        {
            if (item == null) return;
            var tcs = new TaskCompletionSource<bool>();

            await Shell.Current.GoToAsync(nameof(RoutineEditPage), new Dictionary<string, object>
            {
                { "RoutineRepo", _routineRepo },
                { "QuoteRepo", _quoteRepo },
                { "RoutineItem", item.Item },
                { "CompletionSource", tcs }
            });

            bool result = await tcs.Task;

            if (result)
            {
                Task.Run(async () => await LoadDataAsync());
            }

            SelectedItem = null;
        }
    }
}
