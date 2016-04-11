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
    public partial class AdminForm : Form
    {
        private EventManager eventManager = new EventManager();

        public AdminForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            eventManager.addEvent(textBox1.Text, textBox2.Text, textBox3.Text,
                dateTimePicker1.Value, Int32.Parse(textBox4.Text));
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string title = comboBox1.SelectedItem.ToString();
            Event _event = null;
            List<Event> events = eventManager.getEvents();
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
                textBox5.Text = _event.title;
                textBox6.Text = _event.directed;
                textBox7.Text = _event.distribution;
                textBox8.Text = _event.openingDate.ToString();
                textBox9.Text = _event.ticketCount.ToString();
            }
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            List<string> eventsTitles = eventManager.getEventsTitles();
            comboBox1.Items.Clear();
            foreach (String title in eventsTitles)
            {
                comboBox1.Items.Add(title);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string title = comboBox1.SelectedItem.ToString();
            Event _event = null;
            List<Event> events = eventManager.getEvents();
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
                eventManager.removeEvent(_event);
            }
        }


        private void button3_Click(object sender, EventArgs e)
        {
            string title = comboBox1.SelectedItem.ToString();
            Event oldEvent = null;
            List<Event> events = eventManager.getEvents();
            foreach (Event ev in events)
            {
                if (ev.title.Equals(title))
                {
                    oldEvent = ev;
                    break;
                }
            }

            eventManager.updateEvent(oldEvent.title, oldEvent.directed, oldEvent.distribution, oldEvent.openingDate, oldEvent.ticketCount, 
                                textBox14.Text, textBox13.Text, textBox12.Text, dateTimePicker2.Value, Int32.Parse(textBox11.Text));
        }

        private void button5_Click(object sender, EventArgs e)
        {
            UserManager userManager = new UserManager();
            userManager.addUser(textBox10.Text, textBox15.Text);
        }
    }
}
