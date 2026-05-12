using SimpleNotificationSystem.DataAccessLayer;
using SimpleNotificationSystem.Interfaces;
using SimpleNotificationSystem.Models;
using SimpleNotificationSystem.NotificationSenders;

namespace SimpleNotificationSystem.BusinessLayer
{
    public class NotificationService
    {
        NotificationRepository notificationRepository;
        public NotificationService()
        {
            notificationRepository = new NotificationRepository();
        }
        public Notification SendNotification(User user,NotificationType type, string message)
        {
             if (user == null)
            {
                throw new Exception("User cannot be null.");
            }
            if (string.IsNullOrWhiteSpace(message))
            {
                throw new Exception("Message cannot be null or empty.");
            }
            if (message.Length < 5)
            {
                throw new Exception("Message should contain at least 5 characters.");
            }
            INotificationSender sender;
            switch(type)
            {
                case NotificationType.Email:
                    if (string.IsNullOrWhiteSpace(user.Email))
                    {
                        throw new Exception("User email cannot be null or empty.");
                    }
                    sender= new EmailNotificationSender();
                    break;
                case NotificationType.SMS:
                    if (string.IsNullOrWhiteSpace(user.PhoneNumber))
                    {
                        throw new Exception("User phone number cannot be null or empty.");
                    }
                    if (message.Length > 160)
                    {
                        throw new Exception("SMS message should not exceed 160 characters.");
                    }
                    sender= new SmsNotificationSender();
                    break;
                default:
                    throw new Exception("Invalid notification type.");
            }
            Notification notification=new Notification
            {
                Message=message,
                Type=type,
                SentDate=DateTime.Now,
                UserId=user.Id
            };
            Notification createdNotification = notificationRepository.Create(notification);
            sender.Send(user, createdNotification);
            return createdNotification;
        }
        public List<Notification>? GetAllNotifications()
        {
            return notificationRepository.GetAll();
        }
    }
}