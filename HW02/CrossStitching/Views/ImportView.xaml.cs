using CrossStitching.Models;
using CrossStitching.ViewModels;

namespace CrossStitching.Views;

public partial class ImportView : ContentPage
{
	public ImportView(TaskCompletionSource<bool> tcs, CanvasData data)
	{
		InitializeComponent();
		BindingContext = new ImportViewModel(this.Navigation, tcs, data);
	}
}