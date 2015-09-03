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

        public FileService(IUserCredentialService userCredentialService)
            : base(userCredentialService) { }

        public IObservable<BaseResponse> Delete(string fileId)
        {
            fileId.ThrowIfNull("fileId");

            return ObservableApiCall(DELETE_METHOD,
                    new { file = fileId },
                    async (requestUrl, cancellationToken) => await GetResponseAsync<BaseResponse>(requestUrl, cancellationToken));
        }

        public IObservable<FileInfoResponse> Info(string fileId, int? count = default(int?), int? page = default(int?))
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
    }
}
