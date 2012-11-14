using System;

namespace GVoiceSharp.Web
{
    /// <summary>
    /// Manages web requests
    /// </summary>
    internal interface IWebManager : IDisposable
    {
        /// <summary>
        /// Request a web resource
        /// </summary>
        /// <param name="address"><see cref="Uri"/> of the requested resource</param>
        WebManagerResponse RequestPage(Uri address);
        /// <summary>
        /// Perform post to a resource
        /// </summary>
        /// <param name="address"><see cref="Uri"/> of the resource to post data to</param>
        /// <param name="fields">Collection of fields to post</param>
        /// <param name="serializationType">Type of form serialization to use</param>
        WebManagerResponse PerformPost(Uri address, FormFieldCollection fields, FormFieldSerializationType serializationType);
    }
}