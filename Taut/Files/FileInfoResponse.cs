using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taut.Files
{
    public class FileInfoResponse : BaseResponse
    {
        public FileInfoResponse()
        {
            Comments = new List<FileComment>();
        }

        public File File { get; set; }

        public IEnumerable<FileComment> Comments { get; set; }

        public Paging Paging { get; set; }
    }
}
