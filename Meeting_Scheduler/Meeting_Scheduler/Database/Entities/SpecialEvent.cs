using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meeting_Scheduler.Database.Entities
{
    public class SpecialEvent:TableEntity
    {

        public string Username { get; set; }
        public string Name { get; set; }

        public string EventType { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
        public string Description { get; set; }

        public SpecialEvent()
        {
            
        }

        public SpecialEvent(string username, string name, string eventType, DateTime startDate, DateTime endDate, string description)
        {
            PartitionKey = "EVENT";
            RowKey = Guid.NewGuid().ToString();
            Username = username;
            Name = name;
            EventType = eventType;
            StartDate = startDate;
            EndDate = endDate;
            Description = description;
        }
    }
}
