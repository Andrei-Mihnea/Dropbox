using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonModels;
using DataAccess;

namespace BusinessLogic
{
    public class FileManager
    {
        private readonly FileRepository _repo = new FileRepository();

        public void UploadFile(User user, string localPath)
        {
            string fileName = Path.GetFileName(localPath);
            string storagePath = Path.Combine("C:\\DropboxServerData", user.Id.ToString());

            if(!Directory.Exists(storagePath))
            {
                Directory.CreateDirectory(storagePath);
            }

            string destPath = Path.Combine(storagePath, fileName);

            File.Copy(localPath, destPath, overwrite: true);

            var existingFile = _repo.GetFileByItemAndName(user.Id, fileName);

            if (existingFile != null)
            {
                _repo.DeleteFile(existingFile.Id);
            }

            var file = new FileItem
            {
                UserId = user.Id,
                FileName = fileName,
                FilePath = destPath,
                UploadedAt = DateTime.Now
            };

            _repo.InsertFile(file);
        }

        public List<FileItem> GetFilesForUser(int userId)
        {
            return _repo.GetFilesByUserId(userId);
        }
        public void DeleteFile(int fileId)
        {
            var file = _repo.GetFileById(fileId);
            if (file != null)
            {
                if (File.Exists(file.FilePath))
                {
                    File.Delete(file.FilePath);
                }
                _repo.DeleteFile(fileId);
            }
        }

    }
}
