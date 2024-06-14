using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meeting_Scheduler.Common
{
    public interface ILogger
    {

        void Log(string message,EventLogEntryType type);

    }
}
