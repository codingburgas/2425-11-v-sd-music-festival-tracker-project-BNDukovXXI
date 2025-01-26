using System.Collections.Generic;
using Program.Models;

namespace Program.DataAccess
{
    public class Database 
    {
        public List<User> Users { get; set; } = new List<User>(); // List of users
        public List<MusicalEvent> MusicalEvents { get; set; } = new List<MusicalEvent>(); // List of musical events

        private readonly object _lock = new object(); // Lock object for thread safety

        // Adds a user to the database
        public void AddUser(User user)
        {
            lock (_lock) // Ensure thread safety
            {
                Users.Add(user);
            }
        }

        // Adds a musical event to the database
        public void AddEvent(MusicalEvent ev)
        {
            lock (_lock) // Ensure thread safety
            {
                MusicalEvents.Add(ev);
            }
        }

        // Returns a copy of the list of users
        public List<User> GetUsers()
        {
            lock (_lock) // Ensure thread safety
            {
                return new List<User>(Users);
            }
        }

        // Returns a copy of the list of musical events
        public List<MusicalEvent> GetEvents()
        {
            lock (_lock) // Ensure thread safety
            {
                return new List<MusicalEvent>(MusicalEvents);
            }
        }

        // Updates an existing musical event
        public void UpdateEvent(MusicalEvent ev)
        {
            lock (_lock) // Ensure thread safety
            {
                var existingEvent = MusicalEvents.FirstOrDefault(e => e.Id == ev.Id);
                if (existingEvent != null)
                {
                    existingEvent.EventName = ev.EventName;
                    existingEvent.EventDate = ev.EventDate;
                    existingEvent.Location = ev.Location;
                    existingEvent.AvailableTickets = ev.AvailableTickets;
                }
            }
        }

        // Updates an existing user
        public void UpdateUser(User user)
        {
            lock (_lock) // Ensure thread safety
            {
                var existingUser = Users.FirstOrDefault(u => u.Id == user.Id);
                if (existingUser != null)
                {
                    existingUser.Name = user.Name;
                    existingUser.Email = user.Email;
                    existingUser.Password = user.Password;
                    existingUser.Role = user.Role;
                }
            }
        }

        // Deletes a user by ID
        public void DeleteUser(int id)
        {
            lock (_lock) // Ensure thread safety
            {
                var user = Users.FirstOrDefault(u => u.Id == id);
                if (user != null)
                {
                    Users.Remove(user);
                }
            }
        }

        // Deletes a musical event by ID
        public void DeleteEvent(int id)
        {
            lock (_lock) // Ensure thread safety
            {
                var ev = MusicalEvents.FirstOrDefault(e => e.Id == id);
                if (ev != null)
                {
                    MusicalEvents.Remove(ev);
                }
            }
        }
    }
}