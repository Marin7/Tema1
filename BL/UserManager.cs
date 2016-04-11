using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using DL;
using Models;

namespace BL
{
    public class UserManager
    {
        private UserDAOMySQL userDAO = UserDAOMySQL.getInstance();

        public User.Role logIn(string username, string password)
        {
            String cryptPassword = crypt(password);
            User u = userDAO.getUser(username, cryptPassword);
            return u.role;
        }

        public void addUser(string username, string password)
        {
            String cryptPassword = crypt(password);
            User u = new User(username, cryptPassword, User.Role.Employee);
            userDAO.addUser(u);
        }

        public string resetPassword(string username)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < 10; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            string newPassword = builder.ToString();
            string newPasswordCrypt = crypt(newPassword);
            userDAO.updatePassword(username, newPasswordCrypt);
            return newPassword;
        }

        public static String crypt(String input)
        {
            // Create a new instance of the MD5CryptoServiceProvider object.
            MD5 md5Hasher = MD5.Create();

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            // Return the hexadecimal string.
            return sBuilder.ToString();
        }
    }
}
