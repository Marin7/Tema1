using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using DL;

namespace BL
{
    public class TicketManager
    {
        private TicketDAOMySQL ticketDAO = TicketDAOMySQL.getInstance();
        private EventDAOMySQL eventDAO = EventDAOMySQL.getInstance();

        public bool addTicket(Ticket ticket)
        {
            int ticketCount = ticketDAO.getTicketCountEvent(ticket._event);
            if (ticketCount == ticket._event.ticketCount)
            {
                return false;
            }
            bool ok = ticketDAO.checkTicket(ticket);
            if(!ok) {
                return false;
            }
            ticketDAO.addTicket(ticket);
            return true;
        }

        public List<Ticket> getTickets()
        {
            List<Ticket> tickets = ticketDAO.getTickets();
            return tickets;
        }

        public List<Ticket> getTicketsOfEvent(Event _event)
        {
            List<Ticket> tickets = ticketDAO.getTicketsOfEvent(_event);
            return tickets;
        }
    }
}
