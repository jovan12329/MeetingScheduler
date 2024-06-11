using Meeting_Scheduler.Database.Entities;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meeting_Scheduler.Database.Repositories
{
    public class AbsenceRepository
    {

        private CloudStorageAccount _storageAccount;
        private CloudTable _table;
        public AbsenceRepository()
        {
            _storageAccount =
           CloudStorageAccount.Parse(ConfigurationManager.AppSettings["DataConnectionString"]);
            CloudTableClient tableClient = new CloudTableClient(new
           Uri(_storageAccount.TableEndpoint.AbsoluteUri), _storageAccount.Credentials);
            _table = tableClient.GetTableReference("AbsenceTable");
            _table.CreateIfNotExists();
        }
        public AbsenceRequest GetSeeksByDateStartFinish(DateTime start, DateTime finish)
        {
            var results = from g in _table.CreateQuery<AbsenceRequest>()
                          select g;

            var list = results.ToList();

            var res = list.Where(g => start <= g.StartDate.ToLocalTime() && finish >= g.EndDate.ToLocalTime()).ToList();


            return res.FirstOrDefault();
        }


        public int GetApprovedAbsences() 
        {

            var results = from g in _table.CreateQuery<AbsenceRequest>()
                          where g.Approved.Equals("True")
                          select g;

            return results.ToList().Count;


        }


        public int GetMyAbsencsApproved(string id) 
        {
            var results = from g in _table.CreateQuery<AbsenceRequest>()
                          where g.Approved.Equals("True") && g.Username.Equals(id)
                          select g;

            return results.ToList().Count;


        }



        public List<AbsenceRequest> GetFalseAbsenceLeaves()
        {
            var results = from g in _table.CreateQuery<AbsenceRequest>()
                          where g.Approved.Equals("False")
                          select g;

            return results.ToList();

        }

        public AbsenceRequest GetById(string id)
        {
            var results = from g in _table.CreateQuery<AbsenceRequest>()
                          where g.RowKey.Equals(id)
                          select g;

            return results.FirstOrDefault();

        }


        public void AddAbsence(AbsenceRequest a)
        {
            TableOperation tb = TableOperation.Insert(a);
            _table.Execute(tb);

        }

        public void RemoveAbsence(AbsenceRequest a)
        {
            TableOperation tb = TableOperation.Delete(a);
            _table.Execute(tb);

        }

        public void UpdateAbsence(AbsenceRequest a)
        {
            TableOperation tb = TableOperation.Replace(a);
            _table.Execute(tb);

        }




    }
}
