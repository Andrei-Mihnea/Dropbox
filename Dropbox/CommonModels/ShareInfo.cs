using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonModels
{
    public class ShareInfo
    {
        public int Id { get; set; }
        public int FileId { get; set; }
        public int SharedWithUserId { get; set; }
    }
}
