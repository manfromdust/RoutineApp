using CommunityToolkit.Mvvm.ComponentModel;

namespace CrossStitching.ViewModels
{
    public class ViewModel : ObservableObject
    {
        public INavigation Nagivation { get; set; }
    }
}
