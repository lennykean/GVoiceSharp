using System;

namespace GVoiceSharp
{
    /// <summary>
    /// Represents a connection to google voice services
    /// </summary>
    public interface IGVoiceConnection : IDisposable
    {
        /// <summary>
        /// Login to google voice
        /// </summary>
        /// <param name="email">Google voice email</param>
        /// <param name="password">Google voice password</param>
        void Login(string email, string password);
        /// <summary>
        /// Send an SMS to a specified phone number
        /// </summary>
        /// <param name="phoneNumber">Phone number to send the sms to</param>
        /// <param name="message">SMS Message</param>
        void SendSms(string phoneNumber, string message);
        /// <summary>
        /// Close the connection
        /// </summary>
        void Close();
    }
}