using CrossStitching.Models;
using CrossStitching.ViewModels;

namespace CrossStitching.Views;

public partial class MainView : ContentPage
{
	private readonly CanvasData _masterData = new CanvasData();
	private readonly CrossStitchingDrawable _drawable;
    public MainView()
	{
		InitializeComponent();

		_drawable = new CrossStitchingDrawable();
		CanvasGV.Drawable = _drawable;
		var mvm = new MainViewModel(this.Navigation, _masterData);
        BindingContext = mvm;

		_drawable.Data = _masterData;
		_drawable.CellSize = mvm.CellSize;

		mvm.RequestRedraw = () => CanvasGV.Invalidate();
    }

	private void OnCanvasInteracted(object sender, TouchEventArgs e)
	{
		if (BindingContext is MainViewModel mvm && e.Touches.Length > 0)
		{
			mvm.PaintCellCommand.Execute(e.Touches[0]);
		}
    }
}