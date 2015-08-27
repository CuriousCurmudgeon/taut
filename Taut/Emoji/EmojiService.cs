using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taut.Authorizations;

namespace Taut.Emoji
{
    public class EmojiService : BaseAuthenticatedApiService, IEmojiService
    {
        private const string LIST_METHOD = "emoji.list";

        public EmojiService(IUserCredentialService userCredentialService)
            : base(userCredentialService) { }

        public IObservable<EmojiListResponse> List()
        {
            return ObservableApiCall(LIST_METHOD,
                    new { },
                    async (requestUrl, cancellationToken) => await GetResponseAsync<EmojiListResponse>(requestUrl, cancellationToken));
        }
    }
}
