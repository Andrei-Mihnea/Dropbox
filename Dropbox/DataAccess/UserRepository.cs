using System;
using System.Collections.Generic;
using System.Data.OracleClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using CommonModels;

namespace DataAccess
{
    public class UserRepository
    {
        private readonly string _ConnectionString = "User Id=dropbox;Password=dropbox123;Data Source=localhost:XEPDB1";
        public User GetUserByUsername(string username)
        {
            try
            {
                using (var conn = new OracleConnection(_ConnectionString))
                {
                    conn.Open();
                    var cmd = conn.CreateCommand();

                    cmd.CommandText = @"SELECT ID, USERNAME, PASSWORD FROM USERS WHERE USERNAME = :username";
                    cmd.Parameters.Add(new OracleParameter("username", username));

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new User
                            {
                                Id = reader.GetInt32(0),
                                Username = reader.GetString(1),
                                PasswordHash = reader.GetString(2)
                            };
                        }
                    }
                }

                return null;
            } catch(OracleException ex)
            {
                throw new ApplicationException("Eroare la accesarea bazei de date.", ex);
            }
        }
    }
}
