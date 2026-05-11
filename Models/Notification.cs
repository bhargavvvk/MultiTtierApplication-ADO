namespace SimpleNotificationSystem.Models
{
    public enum NotificationType
    {
    Email = 1,
    SMS
    }
    public class Notification
    {
        public int Id { get; set; }=0;
        public string Message { get; set; }= string.Empty;
        public NotificationType Type { get; set; }
        public DateTime SentDate { get; set; }
        public int UserId { get; set; }= 0;
        //if User type is used
        public Notification()        {
            //cause we may create notification object without passing parameters, so we need to have a parameterless constructor
        }
        public Notification(int id, string message,NotificationType type, DateTime sentDate, int userId)
        {   Id = id;
            Message = message;
            Type = type;
            SentDate = sentDate;
            UserId = userId;
        }
        public override string ToString()
        {
            return $"Notification Id : {Id}\n" +
                   $"Message : {Message}\n" +
                   $"Notification Type : {Type}\n" +
                   $"Sent Date : {SentDate}\n" +
                   $"User Id : {UserId}";
        }
    }
}