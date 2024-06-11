using Meeting_Scheduler.Enums;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meeting_Scheduler.Database.Entities
{
    public class AbsenceRequest:TableEntity
    {
        public string Username { get; set; }
        public string Reason { get; set; }

        public string AbsenceType { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
        public string Description { get; set; }

        public string Approved { get; set; }=false.ToString();

        public AbsenceRequest()
        {
            
        }

        public AbsenceRequest(string username, string reason, string absenceType, DateTime startDate, DateTime endDate, string description)
        {
            PartitionKey = "ABSENCE";
            RowKey = Guid.NewGuid().ToString();
            Username = username;
            Reason = reason;
            AbsenceType = absenceType;
            StartDate = startDate;
            EndDate = endDate;
            Description = description;
        }
    }
}
