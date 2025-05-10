using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using CommonModels;
using DataAccess;

namespace BusinessLogic
{
    public class AuthManager
    {
        private readonly UserRepository _repo = new UserRepository();

        public bool Login(string username, string password)
        {

            if(string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("User-ul și parola nu pot fi goale");
            }

            var user = _repo.GetUserByUsername(username);

            if (user == null)
            {
                throw new UnauthorizedAccessException("Utilizator inexistent");
            }

            

            if(user.PasswordHash != HashPassword(password))
            {
                throw new UnauthorizedAccessException("Parolă incorectă");
            }

            return true;
        }

        public void Register(string username, string password)
        {
            var user = _repo.GetUserByUsername(username);

            if (user != null) throw new Exception("Utilizatorul exista deja");

            var newUser = new User
            {
                Username = username,
                PasswordHash = HashPassword(password)
            };

            _repo.InsertUser(newUser);
        }

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
