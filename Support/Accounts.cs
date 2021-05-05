using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Support
{
    public class Accounts
    {
        public class Account
        {
            public int ID;
            public string Name;
            public string Username;
            public string Password;
            public DateTime CreatedTS;
            public int CreatedBy_User_ID;
            public string CreatedBy_User_Name;
            public DateTime UpdatedTS;
            public int UpdatedBy_User_ID;
            public string UpdatedBy_User_Name;
            public bool IsActive; 
        }
    }
}
