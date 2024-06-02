using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meeting_Scheduler.Database.Entities
{
    public class UserDTO:TableEntity
    {

        public string UserName { get; set; }
        public string Password { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }

        public string Email { get; set; }
        public string Phone { get; set; }

        

       
        public UserDTO() { }

        public UserDTO(Guid id, string userName, string password, string name, string surname, Role role, string email, string phone)
        {
            RowKey = id.ToString();
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
