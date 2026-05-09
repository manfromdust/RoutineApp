using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;


namespace RoutineApp.ViewModels
{
    public partial class SettingsViewModel : ObservableObject
    {
        [RelayCommand]
        public async Task ClearDataAsync()
        {
            bool isConfirmed = await Shell.Current.DisplayAlertAsync("Confirm Delete", "Are you sure you want to delete all data?", "Yes", "No");
            if (!isConfirmed)
                return;

            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "routineapp.db");
            if (File.Exists(dbPath))
            {
                try
                {
                    File.Delete(dbPath);
                    await Toast.Make("Data cleared successfully. App will close.").Show();
                    Application.Current.Quit();
                }
                catch (Exception ex)
                {
                    await Toast.Make($"Error clearing data: {ex.Message}").Show();
                }
            }
        }
    }
}
