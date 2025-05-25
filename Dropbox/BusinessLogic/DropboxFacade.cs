using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using CommonModels;

namespace BusinessLogic
{
    public class DropboxFacade 
    {
        private readonly AuthManager _authManager = new AuthManager();
        private readonly FileManager _fileManager = new FileManager();
        private readonly ShareManager _shareManager = new ShareManager();

        //login
        public User Login(string username, string password) => _authManager.Login(username, password);
        public void Register(string username, string password) => _authManager.Register(username, password);

        //file saving
        public async Task UploadFile(User user, string filepath) => await _fileManager.UploadFile(user, filepath);
        public async Task<List<FileItem>> GetUserFiles(int userId) => await _fileManager.GetFilesForUser(userId);
        public async Task DeleteFile(int userId, string fileName) => await _fileManager.DeleteFile(userId, fileName);
        public async Task DownloadFile(int userId, string fileName, string destinationPath) => await _fileManager.DownloadFile(userId, fileName, destinationPath);


        //sharing files 
        public void ShareFile(int fileId, int withUserId) => _shareManager.ShareFile(fileId, withUserId);
        public List<FileItem> GetSharedFiles(int userId) => _shareManager.GetFilesSharedWithUser(userId);




    }
}
