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
        private readonly string _ConnectionString = "User Id=dropbox;Password=ip2025;Data Source=localhost:1521/XEPDB1;"
;
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
            }
            catch (OracleException ex)
            {
                throw new ApplicationException($"Eroare la accesarea bazei de date.(1) ", ex);
            }
        }

        public void InsertUser(User user)
        {
            try
            {
                using (var conn = new OracleConnection(this._ConnectionString))
                {
                    conn.Open();
                    var cmd = conn.CreateCommand();

                    cmd.CommandText = @"INSERT INTO USERS( USERNAME, PASSWORD ) VALUES(:username, :password)";
                    cmd.Parameters.Add(new OracleParameter("username", user.Username));
                    cmd.Parameters.Add(new OracleParameter("password", user.PasswordHash));

                    cmd.ExecuteNonQuery();
                }
            }
            catch (OracleException ex)
            {
                throw new ApplicationException($"Eroare la accesarea bazei de date.(2) {ex.Message}", ex);
            }
        }
    }
}
