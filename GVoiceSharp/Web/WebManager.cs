using System;
using System.Net;
using System.Text;

namespace GVoiceSharp.Web
{
    /// <summary>
    /// Manages web requests
    /// </summary>
    internal class WebManager : IWebManager
    {
        private readonly IFormFieldSerializer _serializer;

        private CookieContainer _cookieContainer;
        private bool _isDisposed;
        
        /// <summary>
        /// Initializes a new instace of <see cref="WebManager"/>
        /// </summary>
        /// <param name="serializer">Form field serializer</param>
        public WebManager(IFormFieldSerializer serializer)
        {
            _serializer = serializer;
            _cookieContainer = new CookieContainer();
        }

        /// <summary>
        /// Request a web resource
        /// </summary>
        /// <param name="address"><see cref="Uri"/> of the requested resource</param>
        public WebManagerResponse RequestPage(Uri address)
        {
            if (_isDisposed)
                throw new ObjectDisposedException(GetType().Name);

            var request = (HttpWebRequest)WebRequest.Create(address);
            request.CookieContainer = _cookieContainer;

            var response = (HttpWebResponse)request.GetResponse();
            return new WebManagerResponse((int)response.StatusCode, response.ResponseUri, response.Headers.ToDictionary(), response.GetResponseStream());
        }

        /// <summary>
        /// Perform post to a resource
        /// </summary>
        /// <param name="address"><see cref="Uri"/> of the resource to post data to</param>
        /// <param name="fields">Collection of fields to post</param>
        /// <param name="serializationType">Type of form serialization to use</param>
        public WebManagerResponse PerformPost(Uri address, FormFieldCollection fields, FormFieldSerializationType serializationType)
        {
            if (_isDisposed)
                throw new ObjectDisposedException(GetType().Name);

            var request = (HttpWebRequest)WebRequest.Create(address);
            request.CookieContainer = _cookieContainer;
            request.Method = "POST";
            
            switch (serializationType)
            {
                case FormFieldSerializationType.PlainText:
                    request.ContentType = "text/plain";
                    break;
                case FormFieldSerializationType.UrlEncoded:
                    request.ContentType = "application/x-www-form-urlencoded";
                    break;
                case FormFieldSerializationType.Multipart:
                    request.ContentType = "multipart/form-data";
                    break;
                default:
                    throw new ArgumentOutOfRangeException("serializationType");
            }
            var body = _serializer.Serialize(fields, serializationType);
            var buffer = Encoding.Default.GetBytes(body);
            request.ContentLength = buffer.Length;
            var requestStream = request.GetRequestStream();
            requestStream.Write(buffer, 0, buffer.Length);

            var response = (HttpWebResponse)request.GetResponse();
            return new WebManagerResponse((int)response.StatusCode, response.ResponseUri, response.Headers.ToDictionary(), response.GetResponseStream());
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
            _isDisposed = true;
            _cookieContainer = null;
        }
    }
}