using Meeting_Scheduler.Enums;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Meeting_Scheduler.Database.Entities;

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
        public IQueryable<User> RetrieveAllUsers()
        {
            var results = from g in _table.CreateQuery<User>()
                          select g;
            return results;
        }
        public void AddUser(User newUser)
        {
            TableOperation insertOperation = TableOperation.Insert(newUser);
            _table.Execute(insertOperation);
        }

        public void RemoveUser(User oldUser)
        {
            TableOperation delOperation = TableOperation.Delete(oldUser);
            _table.Execute(delOperation);
        }


        public void UpdateUser(User u)
        {

            TableOperation delOperation = TableOperation.Replace(u);
            _table.Execute(delOperation);


        }


        public User GetUserByEmail(string mail)
        {
            var query = from t in _table.CreateQuery<User>()
                        where t.Email.Equals(mail)
                        select t;
            return query.FirstOrDefault();

        }





        public User GetUser(string username, string password)
        {
            var query = from t in _table.CreateQuery<User>()
                        where t.UserName == username && t.Password == password
                        select t;
            return query.FirstOrDefault();

        }

        public User GetEmployeeById(string userId)
        {
            var query = from t in _table.CreateQuery<User>()
                        where t.RowKey == userId
                        select t;

            return query.FirstOrDefault();

        }


        public User GetByUsername(string username)
        {
            var query = from t in _table.CreateQuery<User>()
                        where t.UserName == username
                        select t;

            return query.FirstOrDefault();

        }


        public List<User> GetByUsernameEmployee(string username)
        {
            var query = from t in _table.CreateQuery<User>()
                        where t.PartitionKey.Equals("EMPLOYEE") && t.UserName != username
                        select t;

            return query.ToList();

        }




        public IList<User> GetEmployees()
        {
            var results = from g in _table.CreateQuery<User>()
                          where g.PartitionKey.Equals(Role.EMPLOYEE.ToString())
                          select g;
            return results.ToList();

        }

    }
}
