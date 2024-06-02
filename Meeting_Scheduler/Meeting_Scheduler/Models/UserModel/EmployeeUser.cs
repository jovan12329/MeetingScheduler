using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meeting_Scheduler.Models.UserModel
{
    public class EmployeeUser : User
    {
        public EmployeeUser(Guid id, string userName, string password, string name, string surname,string email,string phone) : base(id, userName, password, name, surname, email, phone)
        {
            Role = Role.EMPLOYEE;
        }

        public EmployeeUser() : base() { }

    }
}
