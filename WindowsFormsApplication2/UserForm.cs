using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Models;
using BL;

namespace WindowsFormsApplication2
{
    public partial class UserForm : Form
    {
        private EventManager eventManager = new EventManager();
        private TicketManager ticketManager = new TicketManager();

        public UserForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<Event> events = eventManager.getEvents();
            Event _event = null;
            string title = comboBox1.SelectedItem.ToString();
            foreach (Event ev in events)
            {
                if (ev.title.Equals(title))
                {
                    _event = ev;
                    break;
                }
            }
            if(_event != null) 
            {
                Ticket ticket = new Ticket(_event, Int32.Parse(textBox3.Text), Int32.Parse(textBox2.Text));
                if (!ticketManager.addTicket(ticket))
                {
                    MessageBox.Show("Nu se poate aduga acest bilet");
                }
                
            }
        }

        private void UserForm_Load(object sender, EventArgs e)
        {
            List<string> eventsTitles = eventManager.getEventsTitles();
            comboBox1.Items.Clear();
            foreach (String title in eventsTitles)
            {
                comboBox1.Items.Add(title);
                comboBox2.Items.Add(title);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            List<Event> events = eventManager.getEvents();
            Event _event = null;
            string title = comboBox2.SelectedItem.ToString();
            foreach (Event ev in events)
            {
                if (ev.title.Equals(title))
                {
                    _event = ev;
                    break;
                }
            }
            if (_event != null)
            {
                List<Ticket> ticketsOfEvent = ticketManager.getTicketsOfEvent(_event);
                dataGridView1.DataSource = ticketsOfEvent;
            }
        }
    }
}
