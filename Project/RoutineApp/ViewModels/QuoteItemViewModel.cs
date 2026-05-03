using CommunityToolkit.Mvvm.ComponentModel;
using RoutineApp.Models;

namespace RoutineApp.ViewModels
{
    public partial class QuoteItemViewModel : ObservableObject
    {
        [ObservableProperty]
        public RoutineQuote quote;

        public QuoteItemViewModel(RoutineQuote quote)
        {
            Quote = quote;
        }
    }
}
