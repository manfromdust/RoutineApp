using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CrossStitching.Models;
using CrossStitching.Services;
using System.Collections.ObjectModel;

namespace CrossStitching.ViewModels
{
    public partial class PaletteViewModel : ViewModel
    {
        private readonly Action<string> _callback;
        private readonly Window _window;

        [ObservableProperty]
        private List<ThreadColor> _threadColors = new();

        public int Columns { get; } = 10;
        public float CellSize { get; } = 20f;
        public float Spacing { get; } = 1f;

        public PaletteViewModel(Action<string> callback, Window window)
        {
            _callback = callback;
            _window = window;
            _ = FillPaletteAsync();
        }

        public float PaletteWidth => Columns * (CellSize + Spacing);
        public float PaletteHeight => (float)Math.Ceiling((double)ThreadColors.Count / Columns) * (CellSize + Spacing);

        private async Task FillPaletteAsync()
        {
            var threadColors = await LoadCustomColors.LoadColorsAsync();
            Array.Sort([.. threadColors], new ColorsComparer());

            MainThread.BeginInvokeOnMainThread(() =>
            {
                ThreadColors = threadColors;
                OnPropertyChanged(nameof(PaletteHeight));
            });
        }

        [RelayCommand]
        public void SelectColor(int index)
        {
            if (index >= 0 && index < ThreadColors.Count)
            {
                string hex = ThreadColors[index].Hex;
                _callback?.Invoke(hex);
            }
        }
    }
}
