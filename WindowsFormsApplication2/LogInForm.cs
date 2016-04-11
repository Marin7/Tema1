using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using BL;
using Models;

namespace WindowsFormsApplication2
{
    public partial class LogInForm : Form
    {
        public LogInForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            User.Role role;
            String username = textBox1.Text;
            String password = textBox2.Text;
            try
            {
                UserManager userManager = new UserManager();
                String usernameCtypt = UserManager.crypt(username);
                String passwordCrypt = UserManager.crypt(password);
                role = userManager.logIn(username, password);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
                return;
            }
            if (role == User.Role.Admin)
            {
                AdminForm form = new AdminForm();
                form.Show();
            }
            else
            {
                UserForm form = new UserForm();
                form.Show();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            UserManager userManager = new UserManager();
            String newPassword = userManager.resetPassword(textBox3.Text);
            MessageBox.Show("Your new password: " + newPassword);
            panel1.Visible = false;
        }
    }
}
