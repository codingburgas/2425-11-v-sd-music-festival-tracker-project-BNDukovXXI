using System;
using System.Threading.Tasks;
using Program.Models;
using Program.BusinessLogic;

namespace Program.Presentation
{
    public static class AdminMenu
    {
        // Displays the menu for admin users
        public static async Task ShowMenuAsync(Database db)
        {
            var userService = new UserService(db);
            var eventService = new EventService(db);

            while (true)
            {
                Console.WriteLine("\n--- Admin Menu ---");
                Console.WriteLine("1. Add User");
                Console.WriteLine("2. Add Musical Event");
                Console.WriteLine("3. List Users");
                Console.WriteLine("4. List Musical Events");
                Console.WriteLine("5. Edit User");
                Console.WriteLine("6. Edit Musical Event");
                Console.WriteLine("7. Delete User");
                Console.WriteLine("8. Delete Musical Event");
                Console.WriteLine("9. Logout");
                Console.Write("Select an option: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        await AddUserAsync(userService);
                        break;
                    case "2":
                        await AddEventAsync(eventService);
                        break;
                    case "3":
                        await ListUsersAsync(userService);
                        break;
                    case "4":
                        await ListEventsAsync(eventService);
                        break;
                    case "5":
                        await EditUserAsync(userService);
                        break;
                    case "6":
                        await EditEventAsync(eventService);
                        break;
                    case "7":
                        await DeleteUserAsync(userService);
                        break;
                    case "8":
                        await DeleteEventAsync(eventService);
                        break;
                    case "9":
                        return; // Return to main menu to logout
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        // Adds a new user
        private static async Task AddUserAsync(UserService userService)
        {
            Console.Write("Enter User ID: ");
            int id = int.Parse(Console.ReadLine());
            Console.Write("Enter User Name: ");
            string name = Console.ReadLine();
            Console.Write("Enter User Email: ");
            string email = Console.ReadLine();
            Console.Write("Enter User Password: ");
            string password = Console.ReadLine();
            Console.Write("Enter User Role (0 for Basic, 1 for Admin): ");
            UserRole role = (UserRole)int.Parse(Console.ReadLine());

            await userService.AddUserAsync(new User { Id = id, Name = name, Email = email, Password = password, Role = role });
            Console.WriteLine("User added successfully.");
        }

        // Adds a new musical event
        private static async Task AddEventAsync(EventService eventService)
        {
            Console.Write("Enter Event ID: ");
            int id = int.Parse(Console.ReadLine());
            Console.Write("Enter Event Name: ");
            string eventName = Console.ReadLine();
            Console.Write("Enter Event Date (yyyy-MM-dd): ");
            DateTime eventDate = DateTime.Parse(Console.ReadLine());
            Console.Write("Enter Event Location: ");
            string location = Console.ReadLine();
            Console.Write("Enter Available Tickets: ");
            int availableTickets = int.Parse(Console.ReadLine());

            await eventService.AddEventAsync(new MusicalEvent { Id = id, EventName = eventName, EventDate = eventDate, Location = location, AvailableTickets = availableTickets });
            Console.WriteLine("Musical Event added successfully.");
        }

        // Lists all users
        private static async Task ListUsersAsync(UserService userService)
        {
            var users = await userService.ListUsersAsync();
            Console.WriteLine("\n--- Users ---");
            foreach (var user in users)
            {
                Console.WriteLine($"ID: {user.Id}, Name: {user.Name}, Email: {user.Email}, Role: {user.Role}");
            }
        }

        // Lists all musical events
        private static async Task ListEventsAsync(EventService eventService)
        {
            var events = await eventService.ListEventsAsync();
            Console.WriteLine("\n--- Musical Events ---");
            foreach (var ev in events)
            {
                Console.WriteLine($"ID: {ev.Id}, Name: {ev.EventName}, Date: {ev.EventDate.ToShortDateString()}, Location: {ev.Location}, Available Tickets: {ev.AvailableTickets}");
            }
        }

        // Edits an existing user
        private static async Task EditUserAsync(UserService userService)
        {
            Console.Write("Enter User ID to edit: ");
            int id = int.Parse(Console.ReadLine());
            Console.Write("Enter new Name: ");
            string name = Console.ReadLine();
            Console.Write("Enter new Email: ");
            string email = Console.ReadLine();
            Console.Write("Enter new Password: ");
            string password = Console.ReadLine();
            Console.Write("Enter new Role (0 for Basic, 1 for Admin): ");
            UserRole role = (UserRole)int.Parse(Console.ReadLine());

            await userService.EditUserAsync(id, name, email, password, role);
            Console.WriteLine("User updated successfully.");
        }

        // Edits an existing musical event
        private static async Task EditEventAsync(EventService eventService)
        {
            Console.Write("Enter Event ID to edit: ");
            int id = int.Parse(Console.ReadLine());
            Console.Write("Enter new Event Name: ");
            string eventName = Console.ReadLine();
            Console.Write("Enter new Event Date (yyyy-MM-dd): ");
            DateTime eventDate = DateTime.Parse(Console.ReadLine());
            Console.Write("Enter new Location: ");
            string location = Console.ReadLine();
            Console.Write("Enter new Available Tickets: ");
            int availableTickets = int.Parse(Console.ReadLine());

            await eventService.EditEventAsync(id, eventName, eventDate, location, availableTickets);
            Console.WriteLine("Musical Event updated successfully.");
        }

        // Deletes a user by ID
        private static async Task DeleteUserAsync(UserService userService)
        {
            Console.Write("Enter User ID to delete: ");
            int id = int.Parse(Console.ReadLine());
            await userService.DeleteUserAsync(id);
            Console.WriteLine("User deleted successfully.");
        }

        // Deletes a musical event by ID
        private static async Task DeleteEventAsync(EventService eventService)
        {
            Console.Write("Enter Event ID to delete: ");
            int id = int.Parse(Console.ReadLine());
            await eventService.DeleteEventAsync(id);
            Console.WriteLine("Musical Event deleted successfully.");
        }
    }
}