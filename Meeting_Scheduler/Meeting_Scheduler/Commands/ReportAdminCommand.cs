using log4net.Repository.Hierarchy;
using Meeting_Scheduler.Common;
using Meeting_Scheduler.Database.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Meeting_Scheduler.Commands
{
    public class ReportAdminCommand : CommandBase
    {
        AppointmentRepository ap= new AppointmentRepository();
        SpecialEventRepository specialEventRepository = new SpecialEventRepository();
        SeekingLeaveRepository seekingLeaveRepository = new SeekingLeaveRepository();
        AbsenceRepository absenceRepository = new AbsenceRepository();
        private ILogger logger = new EventViewLogger();

        
        public override void Execute(object parameter)
        {
            logger.Log("Admin report generation starting...", System.Diagnostics.EventLogEntryType.Information);

            int appCnt = ap.GetHosts().Count;
            int evenCnt = specialEventRepository.GetEventsNumber();
            int seekCnt = seekingLeaveRepository.TotalApprovedSeekNumber();
            int absCnt = absenceRepository.GetApprovedAbsences();

            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Total number of appointments,{appCnt}");
            sb.AppendLine($"Total number of events,{evenCnt}");
            sb.AppendLine($"Total number of seeking leaves,{seekCnt}");
            sb.AppendLine($"Total number of absences,{absCnt}");

            using (FileStream fs = new FileStream("adminReport.csv", FileMode.OpenOrCreate)) 
            {
                using (StreamWriter sw = new StreamWriter(fs)) 
                {

                    sw.WriteLine(sb.ToString());
                
                }

            
            }

            logger.Log("Admin report generation was successfully done!", System.Diagnostics.EventLogEntryType.Information);
            MessageBox.Show("Report is exported to .csv file !");


            
        }
    }
}
