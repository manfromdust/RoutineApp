using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

using CrossStitching.Models;
using CommunityToolkit.Mvvm.Messaging;

namespace CrossStitching.ViewModels
{
    public partial class MainViewModel : ViewModel
    {
        private readonly IServiceProvider _serviceProvider;

        [ObservableProperty]
        private ObservableCollection<ThreadColor> _threadColors;

        [ObservableProperty]
        private int _columns;

        public MainViewModel(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            ThreadColors = new ObservableCollection<ThreadColor>();
            WeakReferenceMessenger.Default.Register<CanvasDimensions>(this, (r, m) =>
                                                            { CreateCanvas(m.Rows, m.Cols); });
        }

        public void CreateCanvas(int rows, int cols)
        {
            Columns = cols;
        }
    }
}
