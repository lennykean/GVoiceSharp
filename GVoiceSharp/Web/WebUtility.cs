using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace GVoiceSharp.Web
{
    internal static class WebUtility
    {
        public static Dictionary<string, string> ToDictionary(this WebHeaderCollection headers)
        {
            return headers.AllKeys.ToDictionary(header => header, header => headers[header]);
        }

        public static Uri Combine(Uri relativeTo, Uri relativeAddress)
        {
            if (relativeAddress.IsAbsoluteUri)
                return relativeAddress;

            var baseUri = new UriBuilder
            {
                Scheme = relativeTo.Scheme,
                Host = relativeTo.Host,
                Port = relativeTo.Port
            };
            return new Uri(baseUri.Uri, relativeAddress);
        }
    }
}