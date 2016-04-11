using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Models;

namespace DL
{
    public class UserDAOMySQL
    {
        private MySqlConnection conn;
        private static UserDAOMySQL userDAOMySQL = null;

        private UserDAOMySQL()
        {
            String connectionString = String.Format("server={0};user id={1}; password={2}; database={3}; pooling=false", "localhost", "root", "", "piata");
            conn = new MySqlConnection(connectionString);            
        }

        public static UserDAOMySQL getInstance()
        {
            if (userDAOMySQL == null)
            {
                userDAOMySQL = new UserDAOMySQL();
            }
            return userDAOMySQL;
        }

        public User getUser(String username, String password)
        {
            User u = null;
            String sql = "SELECT * FROM user WHERE username='" + username + "' AND password='"
                + password + "'";
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {
                    u = new User(reader["username"].ToString(), reader["password"].ToString(), (int)reader["role"]);
                }
                else
                {
                    u = null;
                }
                conn.Close();
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);
                conn.Close();
            }

            return u;
        }

        public void addUser(User u)
        {
            String sql = "INSERT INTO user(`username`,`password`,`role`) VALUES('"
                + u.username + "','" + u.password + "',0);";
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

        public void updatePassword(string username, string newPassword)
        {
            String sql = "UPDATE user SET password='" + newPassword + 
                "' WHERE username='" + username + "'";
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
    }
}
