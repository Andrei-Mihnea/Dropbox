using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using CommonModels;
using DataAccess;


//Implementare servicii de autentificare
namespace BusinessLogic
{
    public class AuthManager
    {
        private readonly UserRepository _repo = new UserRepository();

        //Autor implementare login Irimescu Dragos-Andrei
        public User Login(string username, string password)
        {

            //verificare text box-uri sa nu fie goale
            if(string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("User-ul și parola nu pot fi goale");
            }

            //verificare user din baza de date daca exista
            var user = _repo.GetUserByUsername(username);

            if (user == null)
            {
                throw new UnauthorizedAccessException("Utilizator inexistent");
            }

            
            //verificarea parolelor daca coincid
            if(user.PasswordHash != HashPassword(password))
            {
                throw new UnauthorizedAccessException("Parolă incorectă");
            }

            return user;
        }

        //Autor implementare Register Ionescu Vlad-Gabriel
        public void Register(string username, string password)
        {
            //verificare existenta cont de utilizator
            var user = _repo.GetUserByUsername(username);

            if (user != null) throw new Exception("Utilizatorul exista deja");

            var newUser = new User
            {
                Username = username,
                PasswordHash = HashPassword(password)
            };
            //inserare in baza de date
            _repo.InsertUser(newUser);
        }

        //Autor implementare Hashing Ionescu Vlad-Gabriel
        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder builder = new StringBuilder();

                for(int i = 0; i < bytes.Length; ++i)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }

                return builder.ToString();
            }

        }
    }

}
