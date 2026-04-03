using CrossStitching.Models;
using CrossStitching.ViewModels;

namespace CrossStitching.Views;

public partial class ImportView : ContentPage
{
	public ImportView(CanvasData data, TaskCompletionSource<bool> tcs)
	{
		InitializeComponent();
		BindingContext = new ImportViewModel(this.Navigation, data, tcs);
	}

    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        if (BindingContext is ImportViewModel vm)
        {
            vm.NotifyDisappeared();
        }
    }
}