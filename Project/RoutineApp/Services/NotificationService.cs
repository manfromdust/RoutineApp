using Plugin.LocalNotification;
using Plugin.LocalNotification.Core.Models;

namespace RoutineApp.Services
{
    public static class NotificationService
    {
        public static async Task<bool> CheckAndRequestPermissionAsync()
        {
            var isEnabled = await LocalNotificationCenter.Current.AreNotificationsEnabled();
            if (!isEnabled)
            {
                isEnabled = await LocalNotificationCenter.Current.RequestNotificationPermission();
            }
            return isEnabled;
        }

        public static async Task ScheduleDailyQuotesAsync(int notificationId, string routineName,
                                                   TimeSpan timeOfDay, List<string> randomQuotes)
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

        public static void CancelNotifications(int notificationId)
        {
            int baseId = notificationId * 100;

            for (int i = 1; i <= 30; i++)
            {
                int androidNotificationId = baseId + i;
                LocalNotificationCenter.Current.Cancel(androidNotificationId);
            }
        }

        public static async Task RefreshDailyQuotesAsync(int notificationId, string routineName,
                                                  TimeSpan timeOfDay, List<string> randomQuotes)
        {
            CancelNotifications(notificationId);
            await ScheduleDailyQuotesAsync(notificationId, routineName, timeOfDay, randomQuotes);
        }
    }
}
