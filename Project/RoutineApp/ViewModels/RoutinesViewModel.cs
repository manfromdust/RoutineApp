using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace RoutineApp.ViewModels
{
    public partial class RoutinesViewModel : ObservableObject
    {
        private readonly INavigation? _navigation;

        ObservableCollection<RoutineItemViewModel> items;

        public RoutinesViewModel(INavigation navigation)
        {
            _navigation = navigation;
            items = new ObservableCollection<RoutineItemViewModel>();
        }
    }
}
