using System;
using System.Collections.Generic;
using System.IO;

namespace GVoiceSharp.Web
{
    /// <summary>
    /// Response from a <see cref="IWebManager"/> request
    /// </summary>
    internal class WebManagerResponse
    {
        /// <param name="httpStatusCode">Status code of the response</param>
        /// <param name="responseUri">Resource that responded the request</param>
        /// <param name="headers">Header collection</param>
        /// <param name="response">Response stream</param>
        public WebManagerResponse(int httpStatusCode, Uri responseUri, Dictionary<string, string> headers, Stream response)
        {
            Response = response;
            ResponseUri = responseUri;
            Headers = headers;
            HttpStatusCode = httpStatusCode;
        }

        /// <summary>
        /// Status code of the response
        /// </summary>
        public int HttpStatusCode { get; private set; }
        /// <summary>
        /// Resource that responded to the request
        /// </summary>
        public Uri ResponseUri { get; set; }
        /// <summary>
        /// Collection of headers from the response
        /// </summary>
        public Dictionary<string, string> Headers { get; private set; }
        /// <summary>
        /// Response stream
        /// </summary>
        public Stream Response { get; private set; }
    }
}