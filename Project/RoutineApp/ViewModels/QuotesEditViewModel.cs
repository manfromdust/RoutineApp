using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RoutineApp.Models;
using RoutineApp.Repositories;
using System.Collections.ObjectModel;

namespace RoutineApp.ViewModels
{
    [QueryProperty(nameof(QuoteRepo), "QuoteRepo")]
    [QueryProperty(nameof(RoutineId), "RoutineId")]
    public partial class QuotesEditViewModel : ObservableObject
    {
        private IQuoteItemRepository _quoteRepo;
        private int _routineId;

        public IQuoteItemRepository QuoteRepo
        {
            get => _quoteRepo;
            set => SetProperty(ref _quoteRepo, value);
        }

        public int RoutineId
        {
            get => _routineId;
            set => SetProperty(ref _routineId, value);
        }

        [ObservableProperty]
        public ObservableCollection<QuoteItemViewModel> quotes;

        [ObservableProperty]
        public RoutineQuote newQuote;

        [ObservableProperty]
        public QuoteItemViewModel selectedQuote;

        public QuotesEditViewModel()
        {
            Task.Run(async () => await LoadQuotesAsync());
            NewQuote = new RoutineQuote();
        }

        private QuoteItemViewModel CreateQuoteItemViewModel(RoutineQuote quote)
        {
            var quoteVM = new QuoteItemViewModel(quote);
            quoteVM.RoutineActiveStatusChanged += QuoteVM_routineActiveStatusChanged;
            return quoteVM;
        }

        private void QuoteVM_routineActiveStatusChanged(object sender, EventArgs e)
        {
            if (sender is QuoteItemViewModel quoteVM)
            {
                Task.Run(async () => await QuoteRepo.UpdateItemAsync(quoteVM.Quote));
            }
        }

        private async Task LoadQuotesAsync()
        {
            var quotes = await QuoteRepo.GetItemsAsync(RoutineId);
            var quoteVMs = quotes.Select(q => CreateQuoteItemViewModel(q)).ToList();
            Quotes = new ObservableCollection<QuoteItemViewModel>(quoteVMs);
        }

        [RelayCommand]
        public async Task AddNewQuoteAsync()
        {
            if (string.IsNullOrWhiteSpace(NewQuote.Quote))
            {
                var toast = Toast.Make("Quote cannot be empty.", ToastDuration.Long, 14);
                await toast.Show();
                return;
            }
            NewQuote.RoutineId = RoutineId;
            await QuoteRepo.AddItemAsync(NewQuote);
            QuoteItemViewModel newQuoteVM = CreateQuoteItemViewModel(NewQuote);
            Quotes.Add(newQuoteVM);
            SelectedQuote = newQuoteVM;
            var toastSuccess = Toast.Make("Quote added successfully.", ToastDuration.Short, 14);
            await toastSuccess.Show();
            NewQuote = new RoutineQuote();
        }

        [RelayCommand]
        public async Task RemoveQuoteAsync()
        {
            if (SelectedQuote == null)
            {
                var toast = Toast.Make("Please select a quote to remove.", ToastDuration.Short, 14);
                await toast.Show();
                return;
            }

            await QuoteRepo.RemoveItemAsync(SelectedQuote.Quote);
            Quotes.Remove(SelectedQuote);
            SelectedQuote = null;
            var toastSuccess = Toast.Make("Quote removed successfully.", ToastDuration.Short, 14);
            await toastSuccess.Show();
        }
    }
}
