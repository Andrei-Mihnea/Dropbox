using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Autor Ionescu Vlad-Gabriel
namespace CommonModels
{
    //implementare clasa cu datele pe care dorim sa le salvam in baza de date despre utilizator
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
    }
}
