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
    public class SeekingLeaveRepository
    {


        private CloudStorageAccount _storageAccount;
        private CloudTable _table;
        public SeekingLeaveRepository()
        {
            _storageAccount =
           CloudStorageAccount.Parse(ConfigurationManager.AppSettings["DataConnectionString"]);
            CloudTableClient tableClient = new CloudTableClient(new
           Uri(_storageAccount.TableEndpoint.AbsoluteUri), _storageAccount.Credentials);
            _table = tableClient.GetTableReference("SeekingTable");
            _table.CreateIfNotExists();
        }
        public SeekingLeave GetSeeksByDateStartFinish(DateTime start, DateTime finish)
        {
            var results = from g in _table.CreateQuery<SeekingLeave>()
                          select g;

            var list = results.ToList();

            var res = list.Where(g => start <= g.StartDate.ToLocalTime() && finish >= g.EndDate.ToLocalTime()).ToList();


            return res.FirstOrDefault();
        }


        public int TotalApprovedSeekNumber() 
        {
            var results = from g in _table.CreateQuery<SeekingLeave>()
                          where g.Approved.Equals("True")
                          select g;

            return results.ToList().Count;

        }


        public List<SeekingLeave> GetFalseSeekingLeaves() 
        {
            var results = from g in _table.CreateQuery<SeekingLeave>()
                          where g.Approved.Equals("False")
                          select g;

            return results.ToList();

        }

        public SeekingLeave GetById(string id) 
        {
            var results = from g in _table.CreateQuery<SeekingLeave>()
                          where g.RowKey.Equals(id)
                          select g;

            return results.FirstOrDefault();

        }

        public int GetByRequestNum(string id)
        {
            var results = from g in _table.CreateQuery<SeekingLeave>()
                          where g.Username.Equals(id) && g.Approved.Equals("True")
                          select g;

            return results.ToList().Count;

        }


        public void UpdateSeek(SeekingLeave s) 
        {

            TableOperation tb = TableOperation.Replace(s);
            _table.Execute(tb);

        }

        public void RemoveSeek(SeekingLeave s)
        {

            TableOperation tb = TableOperation.Delete(s);
            _table.Execute(tb);

        }

        public void AddSeek(SeekingLeave a)
        {
            TableOperation tb = TableOperation.Insert(a);
            _table.Execute(tb);

        }


    }
}
