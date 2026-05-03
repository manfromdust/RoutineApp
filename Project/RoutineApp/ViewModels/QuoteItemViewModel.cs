using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RoutineApp.Models;

namespace RoutineApp.ViewModels
{
    public partial class QuoteItemViewModel : ObservableObject
    {
        [ObservableProperty]
        public RoutineQuote quote;

        public event EventHandler routineActiveStatusChanged;

        public QuoteItemViewModel(RoutineQuote quote)
        {
            Quote = quote;
        }

        [RelayCommand]
        public void ToggleActiveStatus()
        {
            Quote.Active = !Quote.Active;
            routineActiveStatusChanged?.Invoke(this, new EventArgs());
        }
    }
}
