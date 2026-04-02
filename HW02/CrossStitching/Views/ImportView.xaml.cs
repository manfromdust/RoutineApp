using CrossStitching.Models;
using CrossStitching.ViewModels;

namespace CrossStitching.Views;

public partial class ImportView : ContentPage
{
	public ImportView(CanvasData data)
	{
		InitializeComponent();
		BindingContext = new ImportViewModel(this.Navigation, data);
	}
}