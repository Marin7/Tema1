using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class User
    {
        public enum Role { Admin, Employee };

        public string username { get; set; }
        public string password { get; set; }
        public Role role;

        public User(string username, string password, string role)
        {
            this.username = username;
            this.password = password;
            if (role.Equals("1"))
            {
                this.role = Role.Admin;
            }
            else
            {
                if (role.Equals("0"))
                {
                    this.role = Role.Employee;
                }
            }
        }

        public User(string username, string password, Role role)
        {
            this.username = username;
            this.password = password;
            this.role = role;
        }

        public User(string username, string password, int role)
        {
            this.username = username;
            this.password = password;
            if (role == 1)
            {
                this.role = Role.Admin;
            }
            else
            {
                if (role == 0)
                {
                    this.role = Role.Employee;
                }
            }
        }
    }

    
}
