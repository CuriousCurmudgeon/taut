using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taut.Files
{
    public class FileListResponse : BaseResponse
    {
        public FileListResponse()
        {
            Files = new List<File>();
        }

        public IEnumerable<File> Files { get; set; }

        public Paging Paging { get; set; }
    }
}
