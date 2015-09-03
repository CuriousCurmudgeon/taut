using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taut.Authorizations;

namespace Taut.Files
{
    public class FileService : BaseAuthenticatedApiService, IFileService
    {
        private const string DELETE_METHOD = "files.delete";
        private const string INFO_METHOD = "files.info";
        private const string LIST_METHOD = "files.list";

        public FileService(IUserCredentialService userCredentialService)
            : base(userCredentialService) { }

        public IObservable<BaseResponse> Delete(string fileId)
        {
            fileId.ThrowIfNull("fileId");

            return ObservableApiCall(DELETE_METHOD,
                    new { file = fileId },
                    async (requestUrl, cancellationToken) => await GetResponseAsync<BaseResponse>(requestUrl, cancellationToken));
        }

        public IObservable<FileInfoResponse> Info(string fileId, int? count = null, int? page = null)
        {
            fileId.ThrowIfNull("fileId");

            var queryParams = new Dictionary<string, object>
            {
                { "file", fileId },
                { "count", count },
                { "page", page },
            };
            return ObservableApiCall(INFO_METHOD, queryParams,
                    async (requestUrl, cancellationToken) => await GetResponseAsync<FileInfoResponse>(requestUrl, cancellationToken));
        }

        public IObservable<FileListResponse> List(string fileId, string userId = null, double? timestampFrom = null,
            double? timestampTo = null, FileTypes types = FileTypes.All, int? count = null, int? page = null)
        {
            fileId.ThrowIfNull("fileId");

            var queryParams = new Dictionary<string, object>
            {
                { "file", fileId },
                { "user", userId },
                { "ts_from", timestampFrom },
                { "ts_to", timestampTo },
                { "types", FileTypesToString(types) },
                { "count", count },
                { "page", page },
            };
            return ObservableApiCall(LIST_METHOD, queryParams,
                    async (requestUrl, cancellationToken) => await GetResponseAsync<FileListResponse>(requestUrl, cancellationToken));
        }

        #region Helpers

        private string FileTypesToString(FileTypes fileTypes)
        {
            if (fileTypes.HasFlag(FileTypes.All))
            {
                return FileTypes.All.ToString().ToLower();
            }

            var scopes = new List<string>();
            foreach (Enum value in Enum.GetValues(typeof(FileTypes)))
            {
                if (fileTypes.HasFlag(value))
                {
                    scopes.Add(((FileTypes)value).ToString().ToLower());
                }
            }
            return string.Join(",", scopes);
        }

        #endregion
    }
}
