using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;


namespace RoutineApp.ViewModels
{
    public partial class SettingsViewModel : ObservableObject
    {
        [ObservableProperty]
        ObservableCollection<string> appearanceOptions = new() { "System Default", "Light", "Dark" };
        
        [ObservableProperty]
        string selectedAppearance;

        public SettingsViewModel()
        {
            string savedTheme = Preferences.Get("AppTheme", "System Default");
            SelectedAppearance = savedTheme;
        }

        partial void OnSelectedAppearanceChanged(string value)
        {
            if (string.IsNullOrEmpty(value)) return;

            Preferences.Set("AppTheme", value);

            ChangeAppThemeColor(value);
        }

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

        public static void ChangeAppThemeColor(string theme)
        {
            switch (theme)
            {
                case "Light":
                    Application.Current.UserAppTheme = AppTheme.Light;
                    break;
                case "Dark":
                    Application.Current.UserAppTheme = AppTheme.Dark;
                    break;
                default:
                    Application.Current.UserAppTheme = AppTheme.Unspecified;
                    break;
            }
        }
    }
}
