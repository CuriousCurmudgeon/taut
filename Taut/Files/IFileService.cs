using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taut.Files
{
    public enum FileTypes
    {
        Posts = 1,
        Snippets = 2,
        Images = 4,
        GDocs = 8,
        Zips = 16,
        PDFs = 32,
        All = 0x7FFFFFFF,
    }

    public interface IFileService
    {
        IObservable<BaseResponse> Delete(string fileId);

        IObservable<FileInfoResponse> Info(string fileId, int? count = null, int? page = null);

        IObservable<FileListResponse> List(string fileId, string userId = null, double? timestampFrom = null,
            double? timestampTo = null, FileTypes types = FileTypes.All, int? count = null, int? page = null);

        // TODO: Implement file.uploads
        //IObservable<FileUploadResponse> Upload(Stream file, string fileType = null, string filename = null,
        //    string title = null, string initialComment = null, IEnumerable<string> channels = null);
    }
}
