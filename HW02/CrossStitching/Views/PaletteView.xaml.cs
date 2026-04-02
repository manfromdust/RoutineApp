using CrossStitching.ViewModels;

namespace CrossStitching.Views;

public partial class PaletteView : ContentPage
{
	private readonly Action<string> _callback;
    private readonly PaletteDrawable _drawable;

	public PaletteView(Action<string> callback)
	{
		InitializeComponent();
		_callback = callback;
        _drawable = new PaletteDrawable();
        PaletteGV.Drawable = _drawable;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext == null)
        {
            var vm = new PaletteViewModel(_callback, this.Window);
            BindingContext = vm;

            _drawable.Columns = vm.Columns;
            _drawable.CellSize = vm.CellSize;
            _drawable.Spacing = vm.Spacing;

            vm.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(PaletteViewModel.ThreadColors))
                {
                    _drawable.Colors = vm.ThreadColors;
                    PaletteGV.HeightRequest = vm.PaletteHeight;
                    PaletteGV.WidthRequest = vm.PaletteWidth;
                    PaletteGV.Invalidate();
                }
            };
        }
    }

    private void OnPaletteTapped(object sender, TouchEventArgs e)
    {
        if (BindingContext is PaletteViewModel vm && e.Touches.Length > 0)
        {
            var touch = e.Touches[0];

            int col = (int)(touch.X / (vm.CellSize + vm.Spacing));
            int row = (int)(touch.Y / (vm.CellSize + vm.Spacing));

            if (col >= vm.Columns) return;

            int index = (row * vm.Columns) + col;

            vm.SelectColorCommand.Execute(index);
        }
    }
}