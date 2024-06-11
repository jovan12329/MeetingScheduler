using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meeting_Scheduler.Database.Entities
{
    public class Appointment : TableEntity
    {

        public string Host { get; set; }

        public string Attendance { get; set; }

        public string Type { get; set; }

        public string Name { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }



        public Appointment() { }

        public Appointment(string host, string attendance, string type, string name, DateTime startTime, DateTime endTime)
        {
            RowKey = Guid.NewGuid().ToString();
            Host = host;
            Attendance = attendance;
            Type = type;
            Name = name;
            StartTime = startTime;
            EndTime = endTime;
            PartitionKey = "APPOINTMENT";
        }
    }
}
