using SimpleNotificationSystem.Models;
namespace SimpleNotificationSystem.BusinessLayer
{
    public class UserService
    {
        List<User> users;

        public UserService()
        {
            users = new List<User>();
        }

        // Add User
        public User AddUser(User user)
        {
            if (string.IsNullOrWhiteSpace(user.Name))
            {
                throw new Exception("User name cannot be empty.");
            }

            if (string.IsNullOrWhiteSpace(user.Email) ||
                !user.Email.Contains("@") ||
                !user.Email.Contains("."))
            {
                throw new Exception("Invalid email format.");
            }

            if (string.IsNullOrWhiteSpace(user.PhoneNumber) ||
                user.PhoneNumber.Length != 10 ||
                !user.PhoneNumber.All(char.IsDigit))
            {
                throw new Exception(
                    "Phone number must contain exactly 10 digits.");
            }

            bool userExists = users.Any(u => u.Id == user.Id);

            if (userExists)
            {
                throw new Exception("User Id already exists.");
            }

            users.Add(user);

            return user;
        }

        // Get User By Id
        public User? GetUserById(int id)
        {
            return users.FirstOrDefault(u => u.Id == id);
        }

        // Get All Users
        public List<User> GetAllUsers()
        {
            return users;
        }
    }
}