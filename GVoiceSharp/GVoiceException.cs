using System;

namespace GVoiceSharp
{
    /// <summary>
    /// Represents errors from google voice services
    /// </summary>
    public class GVoiceException : Exception
    {
        /// <param name="errorCode">Error code from the service</param>
        public GVoiceException(int errorCode) : base(ErrorCodeMessage(errorCode))
        {
            ErrorCode = errorCode;
        }

        /// <summary>
        /// Error code from the service
        /// </summary>
        public int ErrorCode { get; private set; }

        private static string ErrorCodeMessage(int errorCode)
        {
            return String.Format("Error Code {0}", errorCode);
        }
    }
}