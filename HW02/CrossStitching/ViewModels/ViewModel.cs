using CommunityToolkit.Mvvm.ComponentModel;

namespace CrossStitching.ViewModels
{
    public abstract partial class ViewModel : ObservableObject
    {
        public INavigation Navigation { get; set; }
    }
}
