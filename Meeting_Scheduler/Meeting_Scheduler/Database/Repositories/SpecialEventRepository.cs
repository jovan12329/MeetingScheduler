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
    public class SpecialEventRepository
    {
        private CloudStorageAccount _storageAccount;
        private CloudTable _table;
        public SpecialEventRepository()
        {
            _storageAccount =
           CloudStorageAccount.Parse(ConfigurationManager.AppSettings["DataConnectionString"]);
            CloudTableClient tableClient = new CloudTableClient(new
           Uri(_storageAccount.TableEndpoint.AbsoluteUri), _storageAccount.Credentials);
            _table = tableClient.GetTableReference("SpecialEventTable");
            _table.CreateIfNotExists();
        }
       
        

        public void AddEvent(SpecialEvent a)
        {
            TableOperation tb = TableOperation.Insert(a);
            _table.Execute(tb);

        }

        public int GetEventsNumber() 
        {

            var results = from g in _table.CreateQuery<Appointment>()
                          select g;

            return results.ToList().Count;


        }

        



    }
}
