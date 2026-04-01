using CrossStitching.ViewModels;

namespace CrossStitching.Views;

public partial class ImportView : ContentPage
{
	public ImportView(TaskCompletionSource<bool> tcs)
	{
		InitializeComponent();
		BindingContext = new ImportViewModel(tcs);
	}
}