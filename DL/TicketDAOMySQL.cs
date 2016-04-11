using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Models;

namespace DL
{
    public class TicketDAOMySQL
    {
        private MySqlConnection conn;
        private static TicketDAOMySQL ticketDAOMySQL;
        private EventDAOMySQL eventDAO;

        private TicketDAOMySQL()
        {
            String connectionString = String.Format("server={0};user id={1}; password={2}; database={3}; pooling=false", "localhost", "root", "", "piata");
            conn = new MySqlConnection(connectionString);            
        }

        public static TicketDAOMySQL getInstance()
        {
            if (ticketDAOMySQL == null)
            {
                ticketDAOMySQL = new TicketDAOMySQL();
            }
            return ticketDAOMySQL;
        }

        public void addTicket(Ticket ticket)
        {
            EventDAOMySQL eventDAO = EventDAOMySQL.getInstance();
            int eventId = eventDAO.getId(ticket._event);
            String sql = "INSERT INTO ticket(`row`,`number`,`event_id`) VALUES(" + ticket.row +
                "," + ticket.number + "," + eventId + ");";

            conn.Open();
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
        }

        public int getTicketCountEvent(Event _event)
        {
            int count = 0;
            eventDAO = EventDAOMySQL.getInstance();
            int id = eventDAO.getId(_event);
            String sql = "SELECT id FROM ticket WHERE event_id=" + id;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    id = (int)reader["id"];
                    count++;
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                conn.Close();
            }
            return count;
        }

        public bool checkTicket(Ticket ticket)
        {
            bool ok = true;
            eventDAO = EventDAOMySQL.getInstance();
            int id = eventDAO.getId(ticket._event);
            String sql = "SELECT id FROM ticket WHERE event_id=" + id + " AND number=" + ticket.number +
                " AND row=" + ticket.row;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    id = (int)reader["id"];
                    ok = false;
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                conn.Close();
            }
            return ok;
        }

        public List<Ticket> getTickets()
        {
            List<Ticket> tickets = new List<Ticket>();
            eventDAO = EventDAOMySQL.getInstance();
            String sql = "SELECT * FROM ticket";
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Ticket ticket = new Ticket(eventDAO.getEvent((int)reader["event_id"]),
                        (int)reader["number"],(int)reader["row"]);
                    tickets.Add(ticket);
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                conn.Close();
            }
            return tickets;
        }

        public List<Ticket> getTicketsOfEvent(Event _event)
        {
            List<Ticket> tickets = new List<Ticket>();
            eventDAO = EventDAOMySQL.getInstance();
            int id = eventDAO.getId(_event);
            String sql = "SELECT * FROM ticket WHERE event_id=" + id;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Ticket ticket = new Ticket(_event, (int)reader["number"], (int)reader["row"]);
                    tickets.Add(ticket);
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                conn.Close();
            }
            return tickets;
       }
    }
}
