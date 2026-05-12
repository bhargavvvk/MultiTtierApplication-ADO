using SimpleNotificationSystem.Models;
using SimpleNotificationSystem.Interfaces;
using SimpleNotificationSystem.DataAccessLayer;
using System.Dynamic;
namespace SimpleNotificationSystem.BusinessLayer

{
    public class UserService
    {
        UserRepository userRepository;
        public UserService()
        {
            userRepository= new UserRepository();
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
            return userRepository.Create(user);
        }

        // Get User By Id
        public User? GetUserById(int id)
        {
            return userRepository.Get(id);
        }
    }
}