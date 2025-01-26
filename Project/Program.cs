using System;
using System.Threading.Tasks;
using Program.Models;
using Program.DataAccess;
using Program.BusinessLogic;
using Program.Presentation;
using Program.DataAccess;

namespace Program
{
    public class Program
    {
        private static Database db = new Database(); // In-memory database
        private static User currentUser = null; // Tracks the currently logged-in user

        static async Task Main(string[] args)
        {
            // Seed some initial data
            db.AddUser(new User { Id = 1, Name = "Admin", Email = "admin@example.com", Password = "admin123", Role = UserRole.Admin });
            db.AddUser(new User { Id = 2, Name = "User1", Email = "user1@example.com", Password = "user123", Role = UserRole.Basic });

            bool running = true;
            while (running)
            {
                if (currentUser == null)
                {
                    // Login screen
                    Console.WriteLine("\n--- Login ---");
                    Console.Write("Enter Email: ");
                    string email = Console.ReadLine();
                    Console.Write("Enter Password: ");
                    string password = Console.ReadLine();

                    var userService = new UserService(db);
                    currentUser = await userService.AuthenticateAsync(email, password); // Authenticate user
                    if (currentUser == null)
                    {
                        Console.WriteLine("Invalid email or password. Please try again.");
                        continue;
                    }
                    Console.WriteLine($"Logged in as {currentUser.Name} ({currentUser.Role})");
                }

                // Show appropriate menu based on user role
                if (currentUser.Role == UserRole.Admin)
                {
                    await AdminMenu.ShowMenuAsync(db); // Admin menu
                }
                else
                {
                    await BasicUserMenu.ShowMenuAsync(db, currentUser); // Basic user menu
                }

                // Logout option
                if (currentUser != null)
                {
                    Console.Write("Do you want to logout? (y/n): ");
                    string logoutChoice = Console.ReadLine();
                    if (logoutChoice.ToLower() == "y")
                    {
                        currentUser = null; // Log out the user
                        Console.WriteLine("Logged out.");
                    }
                }
            }
        }
    }
}