using Npgsql;

namespace SimpleNotificationSystem.DataAccessLayer
{
    public class DbConnection
    {
        private static string _connectionString = "Host=localhost;Port=5432;Database=notificationdb;Username=postgres;Password=vbnm";
        public NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection(_connectionString);
        }
    }
}