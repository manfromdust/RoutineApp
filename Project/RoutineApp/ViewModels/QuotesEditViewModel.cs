using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RoutineApp.Models;
using RoutineApp.Repositories;
using System.Collections.ObjectModel;

namespace RoutineApp.ViewModels
{
    [QueryProperty(nameof(RoutineId), "RoutineId")]
    public partial class QuotesEditViewModel : ObservableObject
    {
        private IQuoteItemRepository _quoteRepo;

        [ObservableProperty]
        ObservableCollection<QuoteItemViewModel> quotes = new();

        [ObservableProperty]
        int routineId;

        [ObservableProperty]
        string newQuote;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(ActiveButtonText))]
        QuoteItemViewModel selectedQuote;

        public QuotesEditViewModel(IQuoteItemRepository quoteRepo)
        {
            _quoteRepo = quoteRepo;
            _quoteRepo.OnItemAdded += async (s, e) => Quotes.Add(CreateQuoteItemViewModel(e));
            _quoteRepo.OnItemUpdated += async (s, e) => MainThread.BeginInvokeOnMainThread(async () => await LoadQuotesAsync());
            _quoteRepo.OnItemRemoved += async (s, e) => Quotes.Remove(Quotes.FirstOrDefault(i => i.Quote.Id == e.Id));
        }

        partial void OnRoutineIdChanged(int value)
        {
            MainThread.BeginInvokeOnMainThread(async () => await LoadQuotesAsync());
        }

        public string ActiveButtonText => SelectedQuote != null && SelectedQuote.Quote.Active ? "Deactivate" : "Activate";

        private QuoteItemViewModel CreateQuoteItemViewModel(RoutineQuote quote)
        {
            return new QuoteItemViewModel(quote);
        }

        private async Task LoadQuotesAsync()
        {
            var quotes = await _quoteRepo.GetItemsAsync(RoutineId);
            Quotes.Clear();
            foreach (var quote in quotes)
            {
                Quotes.Add(CreateQuoteItemViewModel(quote));
            }
        }

        [RelayCommand]
        public async Task ChangeActiveAsync()
        {
            if (SelectedQuote == null)
            {
                var toast = Toast.Make("Please select a quote to change active status.", ToastDuration.Short, 14);
                await toast.Show();
                return;
            }
            SelectedQuote.Quote.Active = !SelectedQuote.Quote.Active;
            await _quoteRepo.UpdateItemAsync(SelectedQuote.Quote);
            OnPropertyChanged(nameof(ActiveButtonText));
        }

        [RelayCommand]
        public async Task AddNewQuoteAsync()
        {
            if (string.IsNullOrWhiteSpace(NewQuote))
            {
                var toast = Toast.Make("Quote cannot be empty.", ToastDuration.Long, 14);
                await toast.Show();
                return;
            }

            var quoteItem = new RoutineQuote
            {
                Quote = NewQuote,
                RoutineId = RoutineId,
            };

            await _quoteRepo.AddItemAsync(quoteItem);
            var toastSuccess = Toast.Make("Quote added successfully.", ToastDuration.Short, 14);
            await toastSuccess.Show();
            NewQuote = string.Empty;
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

            bool isConfirmed = await Shell.Current.DisplayAlertAsync("Confirm Delete", "Are you sure you want to delete this quote?", "Yes", "No");
            
            if (!isConfirmed)
            {
                return;
            }

            await _quoteRepo.RemoveItemAsync(SelectedQuote.Quote);
            SelectedQuote = null;
            var toastSuccess = Toast.Make("Quote removed successfully.", ToastDuration.Short, 14);
            await toastSuccess.Show();
        }
    }
}
