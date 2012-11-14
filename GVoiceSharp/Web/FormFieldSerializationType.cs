namespace GVoiceSharp.Web
{
    /// <summary>
    /// Defines how form fields should be serialized
    /// </summary>
    internal enum FormFieldSerializationType
    {
        /// <summary>
        /// Plain text serialization
        /// </summary>
        PlainText = 0,
        /// <summary>
        /// Url encoded serialization
        /// </summary>
        UrlEncoded = 1,
        /// <summary>
        /// Multipart form data serialization
        /// </summary>
        Multipart = 2
    }
}