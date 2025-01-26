using System;
using System.Linq;
using System.Threading.Tasks;
using Program.Models;
using Program.DataAccess;

namespace Program.BusinessLogic
{
    public class EventService
    {
        private readonly Database _db; // Database instance

        public EventService(Database db)
        {
            _db = db;
        }

        // Adds a new musical event to the database
        public async Task AddEventAsync(MusicalEvent ev)
        {
            await Task.Run(() => _db.AddEvent(ev));
        }

        // Edits an existing musical event
        public async Task EditEventAsync(int id, string eventName, DateTime eventDate, string location, int availableTickets)
        {
            await Task.Run(() =>
            {
                var ev = new MusicalEvent { Id = id, EventName = eventName, EventDate = eventDate, Location = location, AvailableTickets = availableTickets };
                _db.UpdateEvent(ev);
            });
        }

        // Deletes a musical event by ID
        public async Task DeleteEventAsync(int id)
        {
            await Task.Run(() => _db.DeleteEvent(id));
        }

        // Returns a list of all musical events
        public async Task<List<MusicalEvent>> ListEventsAsync()
        {
            return await Task.Run(() => _db.GetEvents().OrderBy(e => e.EventDate).ToList());
        }

        // Allows a user to buy a ticket for an event
        public async Task<bool> BuyTicketAsync(int eventId)
        {
            return await Task.Run(() =>
            {
                var ev = _db.GetEvents().FirstOrDefault(e => e.Id == eventId);
                if (ev != null && ev.AvailableTickets > 0)
                {
                    ev.AvailableTickets--;
                    _db.UpdateEvent(ev);
                    return true;
                }
                return false;
            });
        }
    }
}