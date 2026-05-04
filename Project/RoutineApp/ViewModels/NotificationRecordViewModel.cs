using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RoutineApp.Models;

namespace RoutineApp.ViewModels
{
    public partial class NotificationRecordViewModel : ObservableObject
    {
        [ObservableProperty]
        private NotificationRecord notification;

        public event EventHandler ActiveStatusChanged;

        public NotificationRecordViewModel(NotificationRecord notification)
        {
            Notification = notification;
        }

        [RelayCommand]
        public void ToggleActiveStatus()
        {
            Notification.IsActive = !Notification.IsActive;
            ActiveStatusChanged?.Invoke(this, new EventArgs());
        }
    }
}
