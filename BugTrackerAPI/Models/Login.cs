using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackerAPI.Models
{
    public class Login
    {

        public Login(Guid UserId, string UserName, string Password)
        {
            User_Id = UserId;
            User_Name = UserName;
            this.Password = Password;
        }

        public Login()
        {
            User_Id = Guid.NewGuid();
            User_Name = "default";
            Password = "Password";
        }

        public Guid User_Id { get; set; }
        public string User_Name {get; set;}
        public string Password { get; set; }

    }
}
