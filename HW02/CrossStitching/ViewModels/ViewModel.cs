using CommunityToolkit.Mvvm.ComponentModel;
using CrossStitching.Models;

namespace CrossStitching.ViewModels
{
    public abstract partial class ViewModel : ObservableObject
    {
        public CanvasData CanvasData { get; set; } = new CanvasData();
    }
}
