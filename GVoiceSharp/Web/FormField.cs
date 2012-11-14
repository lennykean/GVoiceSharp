
using System;
using System.Text.RegularExpressions;

namespace GVoiceSharp.Web
{
    /// <summary>
    /// Represents an html form field
    /// </summary>
    internal sealed class FormField
    {
        internal const string ATTRIBUTE_GROUP = "attribute";
        internal const string NAME_GROUP = "name";
        internal const string VALUE_GROUP = "value";
        
        internal static readonly Regex FormFieldRegex = new Regex(@"<\s*input\s*(?<attribute>(\w|-| )+\s*=\s*(""|')[^""^']*(""|')\s*)+/{0,1}>", RegexOptions.Compiled);
        internal static readonly Regex AttributeRegex = new Regex(@"(?<name>(\w|-| )+)\s*=\s*(""|')(?<value>[^""^']*)(""|')", RegexOptions.Compiled);

        /// <param name="name">Name of the field</param>
        /// <param name="value">Value of the field</param>
        public FormField(string name, string value)
        {
            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentException("name is required", "name");

            Value = value;
            Name = name;
        }

        /// <summary>
        /// Name of the field
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// Value of the field
        /// </summary>
        public string Value { get; set; }
    }
}
