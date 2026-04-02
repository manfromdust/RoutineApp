using CrossStitching.Models;

namespace CrossStitching.Views;

public partial class SetupView : ContentPage
{
	public SetupView(CanvasData data)
	{
		InitializeComponent();
		BindingContext = new ViewModels.SetupViewModel(this.Navigation, data);
    }
}