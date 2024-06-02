using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meeting_Scheduler.Models
{
    public abstract class User
    {
       

        public Guid Id { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public Role Role { get; set; }

        
        protected User() { }
        protected User(Guid id, string userName, string password, string name, string surname, string email, string phone)
        {
            Id = id;
            UserName = userName;
            Password = password;
            Name = name;
            Surname = surname;
            Email = email;
            Phone = phone;
        }




    }
}
