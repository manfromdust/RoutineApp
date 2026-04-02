using CrossStitching.Models;

namespace CrossStitching.Views;

public partial class ExportView : ContentPage
{
	public ExportView(CanvasData data)
	{
		InitializeComponent();
		BindingContext = new ViewModels.ExportViewModel(this.Navigation, data);
    }
}