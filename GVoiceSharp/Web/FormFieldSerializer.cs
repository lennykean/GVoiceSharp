using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace GVoiceSharp.Web
{
    /// <summary>
    /// Serializes and deserializes form data
    /// </summary>
    internal class FormFieldSerializer : IFormFieldSerializer
    {
        /// <summary>
        /// Serializes form data with the default serialization method.
        /// </summary>
        /// <param name="fields">Form data to serialize</param>
        public string Serialize(FormFieldCollection fields)
        {
            return Serialize(fields, FormFieldSerializationType.UrlEncoded);
        }

        /// <summary>
        /// Serializes form data
        /// </summary>
        /// <param name="fields">Form data to serialize</param>
        /// <param name="serializationType">Serialization method to use</param>
        public string Serialize(FormFieldCollection fields, FormFieldSerializationType serializationType)
        {
            switch (serializationType)
            {
                case FormFieldSerializationType.UrlEncoded:
                    return UrlEncodedSerialize(fields);
                case FormFieldSerializationType.PlainText:
                case FormFieldSerializationType.Multipart:
                    throw new NotImplementedException();
                default:
                    throw new ArgumentOutOfRangeException("serializationType");
            }
        }

        /// <summary>
        /// Deserializes form data using the default serialization method
        /// </summary>
        /// <param name="formData">Form data to deserialize</param>
        public FormFieldCollection Deserialize(string formData)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deserializes form data
        /// </summary>
        /// <param name="formData">Form data to deserialize</param>
        /// <param name="serializationType">Type of serialization to use</param>
        public FormFieldCollection Deserialize(string formData, FormFieldSerializationType serializationType)
        {
            throw new NotImplementedException();
        }

        private static string UrlEncodedSerialize(IEnumerable<FormField> fields)
        {
            var sb = new StringBuilder();
            var separator = "";

            foreach (var field in fields)
            {
                sb.Append(separator);
                sb.Append(HttpUtility.UrlEncode(field.Name));
                sb.Append("=");
                sb.Append(HttpUtility.UrlEncode(field.Value));
                separator = "&";
            }
            return sb.ToString();
        }
    }
}
