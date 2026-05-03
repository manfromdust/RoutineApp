using CommunityToolkit.Mvvm.ComponentModel;
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
            Quotes = new ObservableCollection<QuoteItemViewModel>();
            NewQuote = new RoutineQuote();
        }

        private QuoteItemViewModel CreateQuoteItemViewModel(RoutineQuote quote)
        {
            var quoteVM = new QuoteItemViewModel(quote);
            quoteVM.routineActiveStatusChanged += QuoteVM_routineActiveStatusChanged;
            return quoteVM;
        }

        private void QuoteVM_routineActiveStatusChanged(object sender, EventArgs e)
        {
            if (sender is QuoteItemViewModel quoteVM)
            {
                Task.Run(async () => await QuoteRepo.UpdateItemAsync(quoteVM.Quote));
            }
        }

        public async Task LoadQuotesAsync()
        {
            var quotes = await QuoteRepo.GetItemsAsync(RoutineId);
            var quoteVMs = quotes.Select(q => CreateQuoteItemViewModel(q)).ToList();
            Quotes = new ObservableCollection<QuoteItemViewModel>(quoteVMs);
        }
    }
}
