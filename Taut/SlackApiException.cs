using System;

namespace Taut
{
    public class SlackApiException : Exception
    {
        public SlackApiException(string error)
        {
            Error = error;
        }

        /// <summary>
        /// The error returned by the Slack API.
        /// </summary>
        public string Error { get; private set; }
    }
}
