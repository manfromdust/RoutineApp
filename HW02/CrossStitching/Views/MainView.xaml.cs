using CrossStitching.Models;
using CrossStitching.ViewModels;

namespace CrossStitching.Views;

public partial class MainView : ContentPage
{
	private readonly CanvasData _masterData = new CanvasData();
	public MainView()
	{
		InitializeComponent();
		BindingContext = new MainViewModel(this.Navigation, _masterData);
    }
}