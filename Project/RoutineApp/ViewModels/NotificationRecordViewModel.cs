using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RoutineApp.Models;

namespace RoutineApp.ViewModels
{
    public partial class NotificationRecordViewModel : ObservableObject
    {
        [ObservableProperty]
        NotificationRecord notification;

        [ObservableProperty]
        string title;

        public NotificationRecordViewModel(NotificationRecord notification)
        {
            Notification = notification;
            Title = Notification.TimeOfDay.ToString();
        }
    }
}
