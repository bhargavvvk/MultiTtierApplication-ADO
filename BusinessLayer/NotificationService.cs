using SimpleNotificationSystem.DataAccessLayer;
using SimpleNotificationSystem.Interfaces;
using SimpleNotificationSystem.Models;
using SimpleNotificationSystem.NotificationSenders;

namespace SimpleNotificationSystem.BusinessLayer
{
    public class NotificationService
    {
        IRepository<int, Notification> notificationRepository;
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
            sender.Send(user, notification);
            return notificationRepository.Create(notification);
        }
        public List<Notification>? GetAllNotifications()
        {
            return notificationRepository.GetAll();
        }
        public Notification? DeleteNotification(int id)
        {
            return notificationRepository.Delete(id);
        }
        public Notification? UpdateNotificationMessage(int id, string updatedMessage)
        {
             if (string.IsNullOrWhiteSpace(updatedMessage))
            {
                 throw new Exception("Message cannot be empty.");
            }
            if (updatedMessage.Length < 5)
            {
                throw new Exception("Message should contain at least 5 characters.");
            }
            Notification? existingNotification = notificationRepository.Get(id);
            if (existingNotification == null)
            {
                throw new Exception("Notification not found.");
            }
            if (existingNotification.Type == NotificationType.SMS && updatedMessage.Length > 160)
            {
                throw new Exception("SMS message should not exceed 160 characters.");
            }
            existingNotification.Message = updatedMessage;
            return notificationRepository.Update(id, existingNotification);
        }
        public Notification? GetNotificationById(int id)
        {
            return notificationRepository.Get(id);
        }
    }
}