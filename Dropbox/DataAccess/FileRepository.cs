using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using CommonModels;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Oracle.ManagedDataAccess.Client;
//Autor Mihnea Andrei
namespace DataAccess
{
    public class FileRepository
    {
        private readonly string _bucketName = "insert_the_bucketName";
        private readonly StorageClient _storageClient;

        public FileRepository()
        {
            var credentialPath = @"insert_key_for_db";
            var credential = GoogleCredential.FromFile(credentialPath);
            _storageClient = StorageClient.Create(credential);
        }
        public async Task InsertFile(FileItem file)
        {
            try
            {
                using (var stream = new FileStream(file.FilePath, FileMode.Open))
                {
                    await _storageClient.UploadObjectAsync(_bucketName, $"{file.UserId}/{file.FileName}", null, stream);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Eroare la inserarea fisierului.({ex.Message})");
            }

        }

        public async Task<List<FileItem>> GetFilesByUserId(int userId)
        {
            List<FileItem> files = new List<FileItem>();

            try
            {
                foreach(var obj in _storageClient.ListObjects(_bucketName, $"{userId}/"))
                {
                    files.Add(new FileItem
                    {
                        UserId = userId,
                        FileName = Path.GetFileName(obj.Name),
                        FilePath = obj.Name,
                        UploadedAt = obj.Updated ?? DateTime.Now
                    }) ; 
                }
                return files;
            }
            catch (Exception ex)
            {
                throw new Exception($"Eroare la extragerea utilizatorilor ({ex.Message})");
            } 
        }

        

        public async Task DeleteFile(int userId, string fileName)
        {
            try
            {
                await _storageClient.DeleteObjectAsync(_bucketName, $"{userId}/{fileName}");
            }
            catch(Exception ex)
            {
                throw new Exception($"Eroare la stergerea fisierului: {ex.Message}");
            }
        }

        public async Task DownloadFile(int userId, string fileName, string destinationPath)
        {
            try
            {
                using (var outputFile = File.OpenWrite(destinationPath))
                {
                    await _storageClient.DownloadObjectAsync(_bucketName, $"{userId}/{fileName}", outputFile);
                }
            }
            catch(Exception ex)
            {
                throw new Exception($"Eroare la descarcarea fisierului: {ex.Message} ");
            }
        }
    }
}
