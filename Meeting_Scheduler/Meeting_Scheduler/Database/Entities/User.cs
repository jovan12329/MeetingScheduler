using Meeting_Scheduler.Enums;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meeting_Scheduler.Database.Entities
{
    public class User:TableEntity
    {

        public string UserName { get; set; }
        public string Password { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }

        public string Email { get; set; }
        public string Phone { get; set; }
        public User() { }

        public User(string userName, string password, string name, string surname, Role role, string email, string phone)
        {
            RowKey = Guid.NewGuid().ToString();
            UserName = userName;
            Password = password;
            Name = name;
            Surname = surname;
            PartitionKey = role.ToString();
            Email = email;
            Phone = phone;
        }



    }
}
