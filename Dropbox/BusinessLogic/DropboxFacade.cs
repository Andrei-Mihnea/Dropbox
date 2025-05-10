using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class DropboxFacade 
    {
        private readonly AuthManager _authManager = new AuthManager();

        public bool Login(string username, string password)
        {
            return _authManager.Login(username, password);
        }
        public void Register(string username, string password) 
        {
            _authManager.Register(username, password);
        }
    }
}
