using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AchMaps
{
    public class CAccount
    {
        public string steamid;
        public string success;
    }
    public class AccountContainer
    {
        public CAccount response;
        public AccountContainer()
        {
            CAccount response = new CAccount();
        }
    }
}
