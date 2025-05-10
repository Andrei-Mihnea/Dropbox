using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonModels;
using DataAccess;

namespace BusinessLogic
{
    public class ShareManager
    {
        private readonly ShareRepository _repo = new ShareRepository();

        public void ShareFile(int fileId, int sharedWithUserId)
        {
            var share = new ShareInfo
            {
                FileId = fileId,
                SharedWithUserId = sharedWithUserId
            };

            _repo.Share(share);
        }

        public List<FileItem> GetFilesSharedWithUser(int userId)
        {
            return _repo.GetSharedFilesForUser(userId);
        }
    }
}
