using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
