using System;
using System.Threading.Tasks;
using Program.Models;
using Program.BusinessLogic;
using Program.DataAccess;
namespace Program.Presentation
{
    public static class BasicUserMenu
    {
        // Displays the menu for basic users
        public static async Task ShowMenuAsync(Database db, User currentUser)
        {
            var eventService = new EventService(db);

            while (true)
            {
                Console.WriteLine("\n--- Basic User Menu ---");
                Console.WriteLine("1. Buy Ticket");
                Console.WriteLine("2. Edit My Credentials");
                Console.WriteLine("3. List Musical Events");
                Console.WriteLine("4. Logout");
                Console.Write("Select an option: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        await BuyTicketAsync(eventService);
                        break;
                    case "2":
                        EditMyCredentials(currentUser);
                        break;
                    case "3":
                        await ListMusicalEventsAsync(eventService);
                        break;
                    case "4":
                        return; // Return to main menu to logout
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        // Allows the user to buy a ticket for an event
        private static async Task BuyTicketAsync(EventService eventService)
        {
            Console.Write("Enter Event ID to buy a ticket: ");
            int id = int.Parse(Console.ReadLine());
            bool success = await eventService.BuyTicketAsync(id);
            Console.WriteLine(success ? "Ticket purchased successfully." : "Failed to purchase ticket. No tickets available.");
        }

        // Allows the user to edit their credentials
        private static void EditMyCredentials(User currentUser)
        {
            Console.Write("Enter new Name: ");
            currentUser.Name = Console.ReadLine();
            Console.Write("Enter new Email: ");
            currentUser.Email = Console.ReadLine();
            Console.Write("Enter new Password: ");
            currentUser.Password = Console.ReadLine();
            Console.WriteLine("Credentials updated successfully.");
        }

        // Lists all musical events
        private static async Task ListMusicalEventsAsync(EventService eventService)
        {
            var events = await eventService.ListEventsAsync();
            Console.WriteLine("\n--- Musical Events ---");
            foreach (var ev in events)
            {
                Console.WriteLine($"ID: {ev.Id}, Name: {ev.EventName}, Date: {ev.EventDate.ToShortDateString()}, Location: {ev.Location}, Available Tickets: {ev.AvailableTickets}");
            }
        }
    }
}