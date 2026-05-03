using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace RoutineApp.ViewModels
{
    public partial class QuotesEditViewModel : ObservableObject
    {
        [ObservableProperty]
        public ObservableCollection<QuoteItemViewModel> quotes;

        [ObservableProperty]
        public String newQuote;

        public QuotesEditViewModel()
        {
            Quotes = new ObservableCollection<QuoteItemViewModel>();
        }


    }
}
