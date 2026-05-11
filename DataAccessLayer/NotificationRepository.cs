
using SimpleNotificationSystem.Models;

namespace SimpleNotificationSystem.DataAccessLayer
{
   public class NotificationRepository: AbstractRepository<int, Notification>
    {
        private int _currentId= 0;
        public override Notification Create(Notification item)
        {
            item.Id = ++_currentId;
            _storage[item.Id] = item;
            return item;
        }
    }
}