using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure;
using Meeting_Scheduler.Database.Entities;
using System.Configuration;

namespace Meeting_Scheduler.Database.Repositories
{
    public class UserRepository
    {

        private CloudStorageAccount _storageAccount;
        private CloudTable _table;
        public UserRepository()
        {
            _storageAccount =
           CloudStorageAccount.Parse(ConfigurationManager.AppSettings["DataConnectionString"]);
            CloudTableClient tableClient = new CloudTableClient(new
           Uri(_storageAccount.TableEndpoint.AbsoluteUri), _storageAccount.Credentials);
            _table = tableClient.GetTableReference("UserTable");
            _table.CreateIfNotExists();
        }
        public IQueryable<UserDTO> RetrieveAllUsers()
        {
            var results = from g in _table.CreateQuery<UserDTO>()
                          select g;
            return results;
        }
        public void AddUser(UserDTO newUser)
        {
            TableOperation insertOperation = TableOperation.Insert(newUser);
            _table.Execute(insertOperation);
        }

        public void RemoveUser(UserDTO oldUser)
        {
            TableOperation delOperation = TableOperation.Delete(oldUser);
            _table.Execute(delOperation);
        }


        public void UpdateUser(UserDTO u) 
        {

            TableOperation delOperation = TableOperation.Replace(u);
            _table.Execute(delOperation);


        }


        public UserDTO GetUserByEmail(string mail) 
        {
            var query = from t in _table.CreateQuery<UserDTO>()
                        where t.Email.Equals(mail)
                        select t;
            return query.FirstOrDefault();

        }

        



        public UserDTO GetUser(string username,string password) 
        {
            var query = from t in _table.CreateQuery<UserDTO>()
                        where t.UserName == username && t.Password == password
                        select t;
            return query.FirstOrDefault();
            
        }

        public UserDTO GetEmployeeById(string userId) 
        {
            var query = from t in _table.CreateQuery<UserDTO>()
                        where t.RowKey == userId
                        select t;

            return query.FirstOrDefault();

        }


        public IList<UserDTO> GetEmployees()
        {
            var results = from g in _table.CreateQuery<UserDTO>()
                          where g.PartitionKey.Equals(Role.EMPLOYEE.ToString())
                          select g;
            return results.ToList();

        }




    }
}
