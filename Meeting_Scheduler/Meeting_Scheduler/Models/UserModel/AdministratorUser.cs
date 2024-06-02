using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meeting_Scheduler.Models.UserModel
{
    public class AdministratorUser : User
    {
        public AdministratorUser():base()
        {
        }

        public AdministratorUser(Guid id, string userName, string password, string name, string surname,string email,string phone) : base(id, userName, password, name, surname,email,phone)
        {
            Role = Role.ADMINISTRATOR;
        }
    }
}
