using System;
using System.Collections.Generic;
using System.Linq;
using SimpleNotificationSystem.BusinessLayer;
using SimpleNotificationSystem.Models;

namespace SimpleNotificationSystem.Presentation
{
    internal class Program
    {
        static void Main(string[] args)
        {
            NotificationService notificationService =new NotificationService();
            UserService userService = new UserService();
            bool isRunning = true;
            while (isRunning)
            {
                Console.WriteLine("\n===== SIMPLE NOTIFICATION SYSTEM =====");

                Console.WriteLine("1. Add User Details");
                Console.WriteLine("2. Send Notification");
                Console.WriteLine("3. Display Sent Notification Details");
                Console.WriteLine("4. Exit");

                Console.Write("\nEnter your choice : ");
                int choice;
                while(!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 4)
                {
                    Console.Write("Invalid input. Please enter a number : ");
                }
                try
                {
                    switch (choice)
                    {
                        case 1:
                            Console.Write("Enter User Name : ");
                            string name = Console.ReadLine() ?? string.Empty;
                            Console.Write("Enter Email : ");
                            string email = Console.ReadLine() ?? string.Empty;
                            Console.Write("Enter Phone Number : ");
                            string phoneNumber = Console.ReadLine() ?? string.Empty;
                            User user = new User
                            {
                                Name = name,
                                Email = email,
                                PhoneNumber = phoneNumber
                            };
                            userService.AddUser(user);
                            Console.WriteLine("User added successfully.");
                            break;
                        case 2:
                            Console.Write("Enter User Id : ");
                            int id;
                            while(!int.TryParse(Console.ReadLine(), out id))
                            {
                                Console.Write("Invalid input. Please enter a number : ");
                            }
                             User? existingUser =userService.GetUserById(id);
                            if (existingUser == null){
                                Console.WriteLine("User not found. Please add user details first.");
                                break;
                            }
                            Console.WriteLine("\nChoose Notification Type");
                            Console.WriteLine("1. Email");
                            Console.WriteLine("2. SMS");
                            Console.Write("Enter choice : ");
                            int notificationChoice;
                            while(!int.TryParse(Console.ReadLine(), out notificationChoice) || notificationChoice < 1 || notificationChoice > 2)
                            {
                                Console.Write("Invalid input. Please enter a number : ");
                            }
                            NotificationType notificationType =
                                notificationChoice == 1
                                    ? NotificationType.Email
                                    : NotificationType.SMS;
                            Console.Write("Enter Message : ");
                             string message = Console.ReadLine() ?? string.Empty;
                            Notification createdNotification =notificationService.SendNotification(existingUser,notificationType,message);
                            Console.WriteLine($"\nNotification sent successfully with Id : {createdNotification.Id}");
                            break;
                        case 3:
                            List<Notification>? notifications = notificationService.GetAllNotifications();
                            if (notifications == null || notifications.Count == 0)
                            {
                                Console.WriteLine("No notifications found.");
                                break;
                            }

                            Console.WriteLine("\n===== SENT NOTIFICATIONS =====");

                            foreach (Notification notification in notifications)
                            {
                                Console.WriteLine(notification);
                                Console.WriteLine();
                            }

                            break;
                        case 4:
                            isRunning = false;
                            Console.WriteLine("Exiting Application...");
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
        }
    }
}

}