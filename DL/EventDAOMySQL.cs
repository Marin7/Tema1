using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using MySql.Data.MySqlClient;

namespace DL
{
    public class EventDAOMySQL
    {
        private MySqlConnection conn;
        private static EventDAOMySQL eventDAOMySQL;

        private EventDAOMySQL()
        {
            String connectionString = String.Format("server={0};user id={1}; password={2}; database={3}; pooling=false", "localhost", "root", "", "piata");
            conn = new MySqlConnection(connectionString);            
        }

        public static EventDAOMySQL getInstance()
        {
            if (eventDAOMySQL == null)
            {
                eventDAOMySQL = new EventDAOMySQL();
            }
            return eventDAOMySQL;
        }

        public void addEvent(Event _event)
        {
            String sql = "INSERT INTO event(`title`,`directed`,`distribution`,`openingDate`,`ticketCount`) VALUES('"
                + _event.title + "','" + _event.directed + "','" + _event.distribution + "','" 
                + Utils.getSQLDateFromDate(_event.openingDate) + "'," + _event.ticketCount + ");";
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public List<Event> getEvents()
        {
            List<Event> events = new List<Event>();
            String sql = "SELECT * FROM event";
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Event _event = new Event(reader["title"].ToString(), reader["directed"].ToString(), 
                        reader["distribution"].ToString(), DateTime.Parse(reader["openingDate"].ToString()),
                        (int)reader["ticketCount"]);
                    events.Add(_event);   
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
            return events;
        }

        public int getId(Event _event)
        {
            int id = 0;
            String sql = "SELECT id FROM event WHERE title='" + _event.title + "' AND openingDate='"
                + Utils.getSQLDateFromDate(_event.openingDate) + "';";
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {
                    id = (int)reader["id"];
                }
                conn.Close();
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                conn.Close();
            }
            return id;
        }

        public void removeEvent(Event _event)
        {
            int id = getId(_event);
            String sql = "Remove from event where id=" + id;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public void updateEvent(Event oldEvent, Event newEvent)
        {
            int id = getId(oldEvent);

            String sql = "UPDATE event SET title='" + newEvent.title + "',directed='" + newEvent.directed +
                "',distribution='" + newEvent.distribution + "',openingDate='" + Utils.getSQLDateFromDate(newEvent.openingDate) +
                "',ticketCount=" + newEvent.ticketCount + " WHERE id=" + id;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public Event getEvent(int id)
        {
            Event _event = null;
            String sql = "SELECT * FROM event WHERE id=" + id;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {
                    _event = new Event(reader["title"].ToString(), reader["directed"].ToString(),
                        reader["distribution"].ToString(), DateTime.Parse(reader["openingDate"].ToString()),
                        (int)reader["ticketCount"]);
                }
                conn.Close();
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                conn.Close();
            }
            return _event;
        }
    }
}
