using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meeting_Scheduler.Database.Entities
{
    public class SeekingLeave:TableEntity
    {

        public string Username { get; set; }
        public string Reason { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string Description { get; set; }

        public string Approved { get; set; } = false.ToString();

        public SeekingLeave()
        {
            
        }

        public SeekingLeave(string username,string reason, DateTime startDate, DateTime endDate, string description)
        {
            PartitionKey = "SEEKING";
            RowKey=Guid.NewGuid().ToString();
            Username=username;
            Reason = reason;
            StartDate = startDate;
            EndDate = endDate;
            Description = description;
        }
    }
}
