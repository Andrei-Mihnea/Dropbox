using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using CommonModels;
using Oracle.ManagedDataAccess.Client;

namespace DataAccess
{
    public class FileRepository
    {
        public void InsertFile(FileItem file)
        {
            try
            {
                using (var conn = OracleConnectionHelper.GetConnection())
                {
                    conn.Open();
                    var cmd = conn.CreateCommand(); ;

                    cmd.CommandText = @"INSERT INTO FILES ( USER_ID, FILENAME, FILEPATH, UPLOADED_AT ) 
                                                    VALUES( :userId, :fileName, :filePath, :uploadedAt )";

                    cmd.Parameters.Add(new OracleParameter(":userId", file.UserId));
                    cmd.Parameters.Add(new OracleParameter(":fileName", file.FileName));
                    cmd.Parameters.Add(new OracleParameter(":filePath", file.FilePath));
                    cmd.Parameters.Add(new OracleParameter(":uploadedAt", file.UploadedAt));

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Eroare la inserarea fisierului.({ex.Message})");
            }

        }

        public List<FileItem> GetFilesByUserId(int userId)
        {
            List<FileItem> files = new List<FileItem>();

            try
            {
                using(var conn = OracleConnectionHelper.GetConnection())
                {
                    conn.Open();
                    var cmd = conn.CreateCommand(); ;

                    cmd.CommandText = @"SELECT ID, USER_ID, FILENAME, FILEPATH, UPLOADED_AT FROM FILES WHERE USER_ID = :userId";
                    cmd.Parameters.Add(new OracleParameter("userId", userId));

                    using (var file = cmd.ExecuteReader())
                    {
                        while (file.Read())
                        {
                            files.Add(new FileItem
                            {
                                Id = file.GetInt32(0),
                                UserId = file.GetInt32(1),
                                FileName = file.GetString(2),
                                FilePath = file.GetString(3),
                                UploadedAt = file.GetDateTime(4)
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Eroare la extragerea utilizatorilor ({ex.Message})");
            }

            return files;
        }

        public FileItem GetFileById(int fileID)
        {
            try
            {
                using (var conn = OracleConnectionHelper.GetConnection())
                {
                    conn.Open();

                    var cmd = conn.CreateCommand();
                    cmd.CommandText = @"SELECT * FROM FILES WHERE ID = :fileID";
                    cmd.Parameters.Add(new OracleParameter("fileID", fileID));

                    using (var files = cmd.ExecuteReader())
                    {
                        while(files.Read())
                        {
                            return new FileItem
                            {
                                Id = files.GetInt32(0),
                                UserId = files.GetInt32(1),
                                FileName = files.GetString(2),
                                FilePath = files.GetString(3),
                                UploadedAt = files.GetDateTime(4)
                            };
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"A apărut o eroare în căutarea fișierului ({ex.Message}).");
            }
        }

        public FileItem GetFileByItemAndName(int userId, string fileName)
        {
            try
            {
                using (var conn = OracleConnectionHelper.GetConnection())
                {
                    conn.Open();
                    var cmd = conn.CreateCommand();
                    cmd.CommandText = @"SELECT * FROM FILES WHERE USER_ID = :userId AND FILENAME = :fileName";

                    cmd.Parameters.Add(new OracleParameter("userId", userId));
                    cmd.Parameters.Add(new OracleParameter("fileName", fileName));

                    using (var files = cmd.ExecuteReader())
                    {
                        if (files.Read())
                        {
                            return new FileItem
                            {
                                Id = files.GetInt32(0),
                                UserId = files.GetInt32(1),
                                FileName = files.GetString(2),
                                FilePath = files.GetString(3),
                                UploadedAt = files.GetDateTime(4)
                            };
                        }

                    }
                }
            }   
            catch (Exception ex)
            {
                throw new Exception($"Eroare la gasirea fisierului.({ex.Message})");
            }
            return null;
        }

        public void DeleteFile(int fileId)
        {
            try
            {
                using(var conn = OracleConnectionHelper.GetConnection())
                {
                    conn.Open();

                    var cmd = conn.CreateCommand();
                    cmd.CommandText = @"DELETE FROM FILES  WHERE ID = :fileId";
                    cmd.Parameters.Add(new OracleParameter("fileId", fileId));
                    cmd.ExecuteNonQuery();

                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Eroare la ștergerea fișierului.({ex.Message})");
            }
        }
    }
}
