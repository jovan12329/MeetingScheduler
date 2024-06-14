using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Core;
using log4net.Layout;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meeting_Scheduler.Common
{
    public class FileLogger : ILogger
    {
        private PatternLayout pattern;
        private FileAppender appender;
        private ILog logging;

        public FileLogger(Type t) 
        {
            
            pattern = new PatternLayout();
            pattern.ConversionPattern = "%date{dd-MMM-yyyy} [%thread] [%level] [%class] [%method] - %message%newline";
            pattern.ActivateOptions();

            appender= new FileAppender() 
            {

                Name = "fileAppender",
                Layout = pattern,
                Threshold = Level.All,
                AppendToFile = true,
                File = "SchedulerLogger.log"

            };

            appender.ActivateOptions();
            BasicConfigurator.Configure(appender);

            logging = LogManager.GetLogger(t);


        }


        public void Log(string message, EventLogEntryType type)
        {

            switch (type) 
            {
                case EventLogEntryType.Information:
                    logging.Info(message);
                    break;
                case EventLogEntryType.Warning:
                    logging.Warn(message);
                    break;
                case EventLogEntryType.Error:
                    logging.Error(message);
                    break;
               
                default:
                    Trace.WriteLine("Current type cannot be used !");
                    Environment.Exit(1);
                    break;
                    

            }

        }
    }
}
