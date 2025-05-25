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

        public async Task UploadFile(User user, string localPath)
        {
            string fileName = Path.GetFileName(localPath);

            var file = new FileItem
            {
                UserId = user.Id,
                FileName = fileName,
                FilePath = localPath,
                UploadedAt = DateTime.Now
            };

            await _repo.InsertFile(file);
        }

        public async Task<List<FileItem>> GetFilesForUser(int userId)
        {
            return await _repo.GetFilesByUserId(userId);
        }
        public async Task DeleteFile(int userId, string fileName)
        {
            await _repo.DeleteFile(userId, fileName);
        }

        public async Task DownloadFile(int userId, string fileName, string destinationPath)
        {
            await _repo.DownloadFile(userId, fileName, destinationPath);
        }

    }
}
