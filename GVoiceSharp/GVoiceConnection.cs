using System;
using System.IO;
using GVoiceSharp.Web;
using GVoiceSharp.Web.JsonObjects;
using Newtonsoft.Json;

namespace GVoiceSharp
{
    /// <summary>
    /// Represents a connection to google voice services
    /// </summary>
    public class GVoiceConnection : IGVoiceConnection
    {
        private const string EMAIL_FIELD = "Email";
        private const string PASSWORD_FIELD = "Passwd";
        private const string RNR_SE_FIELD = "_rnr_se";
        private const string PHONENUMBER_FIELD = "phoneNumber";
        private const string TEXT_FIELD = "text";

        private static readonly Uri LoginUri = new Uri("https://www.google.com/voice/");
        private static readonly Uri SendSmsUri = new Uri("https://www.google.com/voice/sms/send/");

        private readonly IWebManager _webManager;
        private bool _isLoggedIn;
        private bool _isDisposed;
        private string _rnrSe;

        /// <param name="webManager">Object to manage the web requests</param>
        internal GVoiceConnection(IWebManager webManager)
        {
            _webManager = webManager;
        }

        /// <summary>
        /// Login to google voice
        /// </summary>
        /// <param name="email">Google voice email</param>
        /// <param name="password">Google voice password</param>
        public void Login(string email, string password)
        {
            if (_isLoggedIn)
                throw new InvalidOperationException("Already logged in");
            if (_isDisposed)
                throw new ObjectDisposedException(GetType().Name);

            var formParser = new FormParser();
            var loginPageResponse = _webManager.RequestPage(LoginUri);
            var loginForms = formParser.ParseForms(loginPageResponse.Response);

            if (loginForms.Count > 1 || 
                !loginForms[0].Fields.ContainsField(EMAIL_FIELD) || 
                !loginForms[0].Fields.ContainsField(PASSWORD_FIELD))
            {
                throw new Exception("Error logging in, expected token not found.");
            }
            loginForms[0].Fields[EMAIL_FIELD].Value = email;
            loginForms[0].Fields[PASSWORD_FIELD].Value = password;

            var loginPostAddress = WebUtility.Combine(loginForms[0].Action, loginPageResponse.ResponseUri);
            var loginPostResponse = _webManager.PerformPost(loginPostAddress, loginForms[0].Fields, FormFieldSerializationType.UrlEncoded);

            var voiceForms = formParser.ParseForms(loginPostResponse.Response);

            if (voiceForms[0].Fields.ContainsField(RNR_SE_FIELD))
                _rnrSe = voiceForms[0].Fields[RNR_SE_FIELD].Value;
            else
                throw new Exception("Error logging in, expected token not found.");

            _isLoggedIn = true;
        }

        /// <summary>
        /// Send an SMS to a specified phone number
        /// </summary>
        /// <param name="phoneNumber">Phone number to send the sms to</param>
        /// <param name="message">SMS Message</param>
        public void SendSms(string phoneNumber, string message)
        {
            if (!_isLoggedIn)
                throw new InvalidOperationException("Not logged in");
            if (_isDisposed)
                throw new ObjectDisposedException(GetType().Name);

            var formData = new FormFieldCollection
            {
                new FormField(PHONENUMBER_FIELD, phoneNumber),
                new FormField(TEXT_FIELD, message),
                new FormField(RNR_SE_FIELD, _rnrSe)
            };
            var response = _webManager.PerformPost(SendSmsUri, formData, FormFieldSerializationType.UrlEncoded);

            using (var streamReader = new StreamReader(response.Response))
            {
                var result = JsonConvert.DeserializeObject<Result>(streamReader.ReadToEnd());

                if (!result.Ok)
                    throw new GVoiceException(result.Data.Code);
            }
        }

        /// <summary>
        /// Close the connection
        /// </summary>
        public void Close()
        {
            _isDisposed = true;
            _webManager.Dispose();
        }

        void IDisposable.Dispose()
        {
            Close();
        }
    }
}
