using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Ticket
    {
        public Event _event {get; set;}
        public int number {get;set;}
        public int row {get;set;}

        public Ticket(Event _event, int number, int row)
        {
            this._event = _event;
            this.number = number;
            this.row = row;
        }
    }
}
