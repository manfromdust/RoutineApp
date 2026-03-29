using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

using CrossStitching.Models;

namespace CrossStitching.ViewModels
{
    public partial class MainViewModel : ViewModel
    {
        private readonly IServiceProvider _serviceProvider;

        [ObservableProperty]
        ObservableCollection<ThreadColor> threadColors;



        public MainViewModel(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            ThreadColors = new ObservableCollection<ThreadColor>();
        }
    }
}
