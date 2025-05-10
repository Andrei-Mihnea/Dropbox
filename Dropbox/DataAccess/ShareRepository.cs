using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using CommonModels;
using Oracle.ManagedDataAccess.Client;

namespace DataAccess
{
    public class ShareRepository
    {
        public void Share(ShareInfo share)
        {
            try
            {
                using (var conn = OracleConnectionHelper.GetConnection())
                {
                    conn.Open();

                    var cmd = conn.CreateCommand();

                    cmd.CommandText = @"INSERT INTO SHAREDFILES(FILE_ID, SHAREDWITHUSER_ID) VALUES(:fileId, :shareId)";

                    cmd.Parameters.Add(new OracleParameter(":fileId",share.FileId));
                    cmd.Parameters.Add(new OracleParameter(":shareId", share.SharedWithUserId));

                    cmd.ExecuteNonQuery();
                }
            }
            catch(Exception ex)
            {
                throw new Exception($"Eroare la partajarea fisierului {ex.Message}");
            }
        }

        public List<FileItem> GetSharedFilesForUser(int userId)
        {
            List<FileItem> sharedFiles = new List<FileItem>();
            try
            {

                using(var conn = OracleConnectionHelper.GetConnection())
                {
                    conn.Open();
                    var cmd = conn.CreateCommand();

                    cmd.CommandText = @"SELECT f.ID, f.FILENAME, f.FILEPATH, f.UPLOADED_AT FROM FILES f
                                        JOIN SHARED_FILES s ON f.ID = s.FILE_ID WHERE s.SHARED_WITH_USER_ID = :user_id";

                    cmd.Parameters.Add(":user_id", userId);

                    using(var outputFile = cmd.ExecuteReader())
                    {
                        while(outputFile.Read())
                        {
                            sharedFiles.Add(new FileItem
                            {
                                Id = outputFile.GetInt32(0),
                                FileName = outputFile.GetString(1),
                                FilePath = outputFile.GetString(2),
                                UploadedAt = outputFile.GetDateTime(3)
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"A apărut o eroare la obținerea fișierelor {ex.Message}");
            }

            return sharedFiles;
        }

    }
}
