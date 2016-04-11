using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using DL;

namespace BL
{
    public class EventManager
    {
        private EventDAOMySQL eventDAO = EventDAOMySQL.getInstance();

        public void addEvent(string title, string directed, string distribution, DateTime openingDate, int ticketCount)
        {
            Event _event = new Event(title, directed, distribution, openingDate, ticketCount);
            eventDAO.addEvent(_event);
        }

        public List<Event> getEvents()
        {
            List<Event> events = new List<Event>();
            events = eventDAO.getEvents();
            return events;
        }

        public void removeEvent(Event _event)
        {
            eventDAO.removeEvent(_event);
        }

        public void updateEvent(string oldTitle, string oldDirected, string oldDistribution, DateTime oldOpeningDate, int oldTicketCount,
                                string newTitle, string newDirected, string newDistribution, DateTime newOpeningDate, int newTicketCount)
        {
            Event oldEvent = new Event(oldTitle, oldDirected, oldDistribution, oldOpeningDate, oldTicketCount);
            Event newEvent = new Event(newTitle, newDirected, newDistribution, newOpeningDate, newTicketCount);
            eventDAO.updateEvent(oldEvent, newEvent);
        }

        public List<string> getEventsTitles()
        {
            List<string> titles = new List<string>();
            List<Event> events = eventDAO.getEvents();
            foreach (Event e in events) {
                titles.Add(e.title);
            }
            return titles;
        }
    }
}
