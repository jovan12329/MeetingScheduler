using log4net.Core;
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
    public class EmployeeReportCommand : CommandBase
    {
        string user;
        AppointmentRepository ap = new AppointmentRepository();
        SpecialEventRepository specialEventRepository = new SpecialEventRepository();
        SeekingLeaveRepository seekingLeaveRepository = new SeekingLeaveRepository();
        AbsenceRepository absenceRepository = new AbsenceRepository();
        private Common.ILogger logger = new FileLogger(typeof(EmployeeReportCommand));
        public EmployeeReportCommand(string user)
        {
            this.user=user;
        }

        public override void Execute(object parameter)
        {
            int appCnt = ap.GetAppointmentsByHost(user).ToList().Count;
            int evenCnt = specialEventRepository.GetEventsNumber();
            int seekCnt = seekingLeaveRepository.GetByRequestNum(user);
            int absCnt = absenceRepository.GetMyAbsencsApproved(user);

            logger.Log("Starting report generation for employee !", System.Diagnostics.EventLogEntryType.Information);

            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Total number of appointments,{appCnt}");
            sb.AppendLine($"Total number of events,{evenCnt}");
            sb.AppendLine($"Total number of seeking leaves,{seekCnt}");
            sb.AppendLine($"Total number of absences,{absCnt}");

            using (FileStream fs = new FileStream("employeeReport.csv", FileMode.OpenOrCreate))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {

                    sw.WriteLine(sb.ToString());

                }


            }
            logger.Log("Report generation was successfully done !", System.Diagnostics.EventLogEntryType.Information);

            MessageBox.Show("Report is exported to .csv file !");

        }
    }
}
