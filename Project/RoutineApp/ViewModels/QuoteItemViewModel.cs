using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RoutineApp.Models;

namespace RoutineApp.ViewModels
{
    public partial class QuoteItemViewModel : ObservableObject
    {
        [ObservableProperty]
        public RoutineQuote quote;

        public event EventHandler RoutineActiveStatusChanged;

        public QuoteItemViewModel(RoutineQuote quote)
        {
            Quote = quote;
        }

        [RelayCommand]
        public void ToggleActiveStatus()
        {
            Quote.Active = !Quote.Active;
            RoutineActiveStatusChanged?.Invoke(this, new EventArgs());
        }
    }
}
