using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taut.Files
{
    public interface IFileService
    {
        IObservable<BaseResponse> Delete(string fileId);

        IObservable<FileInfoResponse> Info(string fileId, int? count = null, int? page = null);
    }
}
