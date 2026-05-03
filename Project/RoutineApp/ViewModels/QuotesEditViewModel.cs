using CommunityToolkit.Mvvm.ComponentModel;
using RoutineApp.Models;
using System.Collections.ObjectModel;

namespace RoutineApp.ViewModels
{
    public partial class QuotesEditViewModel : ObservableObject
    {
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
    }
}
