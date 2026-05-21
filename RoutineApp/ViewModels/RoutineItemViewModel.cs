
using CommunityToolkit.Mvvm.ComponentModel;
using RoutineApp.Models;

namespace RoutineApp.ViewModels
{
    public partial class RoutineItemViewModel : ObservableObject
    {
        [ObservableProperty]
        public RoutineItem item;

        public RoutineItemViewModel(RoutineItem item)
        {
            Item = item;
        }
    }
}
