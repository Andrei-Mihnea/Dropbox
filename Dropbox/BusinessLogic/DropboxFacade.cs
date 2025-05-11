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
        public void UploadFile(User user, string filepath) => _fileManager.UploadFile(user, filepath);
        public List<FileItem> GetUserFiles(int userId) => _fileManager.GetFilesForUser(userId);
        public void DeleteFile(int userId) => _fileManager.DeleteFile(userId);

        //sharing files 
        public void ShareFile(int fileId, int withUserId) => _shareManager.ShareFile(fileId, withUserId);
        public List<FileItem> GetSharedFiles(int userId) => _shareManager.GetFilesSharedWithUser(userId);




    }
}
