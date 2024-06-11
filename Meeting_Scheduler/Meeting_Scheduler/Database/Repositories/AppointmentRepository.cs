using Meeting_Scheduler.Database.Entities;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Meeting_Scheduler.Database.Repositories
{
    public class AppointmentRepository
    {

        private CloudStorageAccount _storageAccount;
        private CloudTable _table;
        public AppointmentRepository()
        {
            _storageAccount =
           CloudStorageAccount.Parse(ConfigurationManager.AppSettings["DataConnectionString"]);
            CloudTableClient tableClient = new CloudTableClient(new
           Uri(_storageAccount.TableEndpoint.AbsoluteUri), _storageAccount.Credentials);
            _table = tableClient.GetTableReference("AppointmentTable");
            _table.CreateIfNotExists();
        }
        public Appointment GetAppointmentsByDateStartFinish(DateTime start, DateTime finish)
        {
            var results = from g in _table.CreateQuery<Appointment>()
                          select g;

            var list = results.ToList();

            var res = list.Where(g => start <= g.StartTime.ToLocalTime() && finish >= g.EndTime.ToLocalTime()).ToList();


            return res.FirstOrDefault();
        }

        public List<string> GetHosts() 
        {
            List<string> h = new List<string>();

            var results = from g in _table.CreateQuery<Appointment>()
                          select g.Host;

            var list = results.ToList();

            list.ForEach(k => { 
                
                if(!h.Contains(k))
                    h.Add(k);


            });

            return h;

        }


        public void CancelAppointment(Appointment appointment) 
        {
            TableOperation tb = TableOperation.Delete(appointment);
            _table.Execute(tb);

        }

        public List<Appointment> AppointmentsByDateStartFinish(DateTime start, DateTime finish)
        {
            var results = from g in _table.CreateQuery<Appointment>()
                          select g;

            var list = results.ToList();

            var res = list.Where(g => start == g.StartTime.ToLocalTime() && finish == g.EndTime.ToLocalTime()).ToList();


            return res;
        }



        public IQueryable<Appointment> GetAppointmentsByHost(string host) 
        {
            var results = from g in _table.CreateQuery<Appointment>()
                          where g.Host.Equals(host)
                          select g;

            Dictionary<string, Appointment> cdbl = new Dictionary<string, Appointment>();

            foreach (var a in results.ToList()) 
            {
                DateTime first = a.StartTime.ToLocalTime();
                DateTime second = a.EndTime.ToLocalTime();
                string key = first.ToString() + second.ToString();

                if (!cdbl.ContainsKey(key)) 
                {
                    cdbl.Add(key, a);

                }


            
            }

            return cdbl.Values.ToList().AsQueryable();


        }


        public void AddApointment(Appointment a)
        {
            TableOperation tb = TableOperation.Insert(a);
            _table.Execute(tb);

        }


    }
}
