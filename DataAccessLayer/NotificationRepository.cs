
using Npgsql;
using SimpleNotificationSystem.Models;

namespace SimpleNotificationSystem.DataAccessLayer
{
   public class NotificationRepository
    {
        private  DbConnection dbConnection;
        public NotificationRepository()
        {
            dbConnection = new DbConnection();
        }
        public  Notification Create(Notification item)
        {
            NpgsqlConnection connection= dbConnection.GetConnection();
            string insertCmd=$"Insert into notifications(message,notificationtype,sentdate,userid) values('{item.Message}','{item.Type}', '{item.SentDate:yyyy-MM-dd HH:mm:ss}', {item.UserId}) RETURNING id";
            NpgsqlCommand command = new NpgsqlCommand(insertCmd, connection);
            try
            {
                connection.Open();
                int generatedId = Convert.ToInt32(command.ExecuteScalar());
                item.Id = generatedId;
                Console.WriteLine("Notification created successfully");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                connection?.Close();
            }
            return item;
        }
        public List<Notification>? GetAll()
        {
             NpgsqlConnection connection= dbConnection.GetConnection();
             List<Notification> notifications= new List<Notification>();
             string SelectQuery= $"SELECT * FROM notifications";
             NpgsqlCommand command = new NpgsqlCommand(SelectQuery, connection);
            try
            {
                connection.Open();
                NpgsqlDataReader reader=command.ExecuteReader();
                while(reader.Read())
                {
                    Notification notification=new Notification
                    {
                        Id=Convert.ToInt32(reader[0]),
                        Message=reader[1].ToString(),
                        Type=(NotificationType)Enum.Parse(typeof(NotificationType), reader[2].ToString()),
                        SentDate=Convert.ToDateTime(reader[3]),
                        UserId=Convert.ToInt32(reader[4])
                    };
                    notifications.Add(notification);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                connection?.Close();
            }
            return notifications;
        }
    }
}