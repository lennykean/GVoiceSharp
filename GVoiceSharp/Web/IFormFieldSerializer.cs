namespace GVoiceSharp.Web
{
    /// <summary>
    /// Serializes and deserializes form data
    /// </summary>
    internal interface IFormFieldSerializer
    {
        /// <summary>
        /// Serializes form data with the default serialization method.
        /// </summary>
        /// <param name="fields">Form data to serialize</param>
        string Serialize(FormFieldCollection fields);

        /// <summary>
        /// Serializes form data
        /// </summary>
        /// <param name="fields">Form data to serialize</param>
        /// <param name="serializationType">Serialization method to use</param>
        string Serialize(FormFieldCollection fields, FormFieldSerializationType serializationType);
        /// <summary>
        /// Deserializes form data using the default serialization method
        /// </summary>
        /// <param name="formData">Form data to deserialize</param>
        FormFieldCollection Deserialize(string formData);

        /// <summary>
        /// Deserializes form data
        /// </summary>
        /// <param name="formData">Form data to deserialize</param>
        /// <param name="serializationType">Type of serialization to use</param>
        FormFieldCollection Deserialize(string formData, FormFieldSerializationType serializationType);
    }
}