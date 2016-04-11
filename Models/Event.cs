using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Event
    {
        public string title { get; set; }
        public string directed { get; set; }
        public string distribution { get; set; }
        public DateTime openingDate { get; set; }
        public int ticketCount { get; set; }

        public Event(string title, string directed, string distribution, DateTime openingDate, int ticketCount)
        {
            this.title = title;
            this.directed = directed;
            this.distribution = distribution;
            this.openingDate = openingDate;
            this.ticketCount = ticketCount;
        }

        override
        public String ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(title).Append(" ").Append(" ").Append(openingDate);
            return stringBuilder.ToString();
        }
    }
}
