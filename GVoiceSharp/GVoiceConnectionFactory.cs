using GVoiceSharp.Web;

namespace GVoiceSharp
{
    /// <summary>
    /// Creates google voice connections
    /// </summary>
    public static class GVoiceConnectionFactory
    {
        /// <summary>
        /// Create a connection
        /// </summary>
        public static GVoiceConnection Create()
        {
            return new GVoiceConnection(new WebManager(new FormFieldSerializer()));
        }
    }
}