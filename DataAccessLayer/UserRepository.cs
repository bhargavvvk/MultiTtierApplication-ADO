using Npgsql;
using SimpleNotificationSystem.Models;

namespace SimpleNotificationSystem.DataAccessLayer
{
    public class UserRepository
    {
        private  DbConnection dbConnection;
        public UserRepository()
        {
            dbConnection = new DbConnection();
        }
        public  User Create(User user)
        {
            NpgsqlConnection connection = dbConnection.GetConnection();
            string insertCmd = $"Insert into Users(name,email,phonenumber) values('{user.Name}','{user.Email}','{user.PhoneNumber}') RETURNING userid";

            NpgsqlCommand command = new NpgsqlCommand(insertCmd, connection);
            try
            {
                connection.Open();
                int generatedId = Convert.ToInt32(command.ExecuteScalar());
                user.Id = generatedId;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                connection?.Close();
            }
            return user;
        }
        public User? Get(int id)
        {
            NpgsqlConnection connection = dbConnection.GetConnection();
            User? user= null;
            string SelectQuery= $"SELECT * FROM users where userid={id}";
            NpgsqlCommand command = new NpgsqlCommand(SelectQuery, connection);
            try
            {
                connection.Open();
                NpgsqlDataReader reader=command.ExecuteReader();
                if(reader.Read())
                {
                    user=new User
                    {
                        Id=Convert.ToInt32(reader[0]),
                        Name=reader[1].ToString(),
                        Email=reader[2].ToString(),
                        PhoneNumber=reader[3].ToString()
                    };
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
            return user;
        }
    }
}