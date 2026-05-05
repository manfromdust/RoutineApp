using Plugin.LocalNotification;
using Plugin.LocalNotification.Core.Models;

namespace RoutineApp.Services
{
    public class NotificationService
    {
        public async Task<bool> CheckAndRequestPermissionAsync()
        {
            var isEnabled = await LocalNotificationCenter.Current.AreNotificationsEnabled();
            if (!isEnabled)
            {
                isEnabled = await LocalNotificationCenter.Current.RequestNotificationPermission();
            }
            return isEnabled;
        }

        public async Task ScheduleDailyQuotesAsync(int notificationId, string routineName, TimeSpan timeOfDay, List<string> randomQuotes)
        {
            if (randomQuotes == null || randomQuotes.Count == 0) return;

            int baseId = notificationId * 100;

            for (int i = 0; i < 30; i++)
            {
                DateTime scheduleDate = DateTime.Today.AddDays(i).Add(timeOfDay);

                if (scheduleDate < DateTime.Now) continue;

                string quoteForToday = randomQuotes[i % randomQuotes.Count];

                var notification = new NotificationRequest
                {
                    NotificationId = baseId + i + 1,
                    Title = routineName,
                    Description = quoteForToday,
                    Schedule = new NotificationRequestSchedule
                    {
                        NotifyTime = scheduleDate,
                    }
                };

                await LocalNotificationCenter.Current.Show(notification);
            }
        }

        public void CancelNotifications(int routineTimeId)
        {
            int baseId = routineTimeId * 100;

            for (int i = 1; i <= 30; i++)
            {
                int androidNotificationId = baseId + i;
                LocalNotificationCenter.Current.Cancel(androidNotificationId);
            }
        }
    }
}
