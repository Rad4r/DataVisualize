using Unity.Notifications.Android;
using UnityEngine;

public class MobileNotifications : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Remove already displayed notifications when app is opened
        AndroidNotificationCenter.CancelAllDisplayedNotifications();
        
        //Create channel
        AndroidNotificationChannel channel = new AndroidNotificationChannel()
        {
            Id = "channel_id",
            Name = "Remind Channel",
            Importance = Importance.Default,
            Description = "Reminder notifications",
        };
        AndroidNotificationCenter.RegisterNotificationChannel(channel);
        
        //send message
        AndroidNotification notification = new AndroidNotification();
        notification.Title = "The time has come";
        notification.Text = "You need to practice your chimp test";
        notification.FireTime = System.DateTime.Now.AddMinutes(1);

        //Send Notification
        int id = AndroidNotificationCenter.SendNotification(notification, "channel_id");
        
        //Check if script already run to not repeat the notifications
        if (AndroidNotificationCenter.CheckScheduledNotificationStatus(id) == NotificationStatus.Scheduled)
        {
            AndroidNotificationCenter.CancelAllNotifications();
            AndroidNotificationCenter.SendNotification(notification, "channel_id");
        }
    }
}
