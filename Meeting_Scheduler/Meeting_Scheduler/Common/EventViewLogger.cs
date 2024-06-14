using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meeting_Scheduler.Common
{
    public class EventViewLogger : ILogger
    {
        private string logName = "SchedulerApp";
        string sourceName = "SourceSchedulerApp";

        public EventViewLogger()
        {

            if (!EventLog.SourceExists(sourceName, Environment.MachineName))
            {

                EventLog.CreateEventSource(new EventSourceCreationData(sourceName, logName));

            }


        }

        public void Log(string message, EventLogEntryType type)
        {
            EventLog log = new EventLog(logName, Environment.MachineName, sourceName);
            log.WriteEntry(message, type);
            log.Close();

        }
    }
}
