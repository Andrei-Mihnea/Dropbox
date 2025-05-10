using System;
using CommonModels;
using Oracle.ManagedDataAccess.Client;

namespace DataAccess
{
    public class UserRepository
    {

        public User GetUserByUsername(string username)
        {
            try
            {
                using (var conn = OracleConnectionHelper.GetConnection())
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
                using (var conn = OracleConnectionHelper.GetConnection())
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
