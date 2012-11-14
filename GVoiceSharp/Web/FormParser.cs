using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace GVoiceSharp.Web
{
    /// <summary> 
    /// Parses form data from html
    /// </summary>
    internal class FormParser
    {
        /// <summary>
        /// Parse form data from html stream
        /// </summary>
        /// <param name="stream">Steam containing html</param>
        public IList<Form> ParseForms(Stream stream)
        {
            using (var streamReader = new StreamReader(stream))
            {
                return ParseForms(streamReader.ReadToEnd());
            }
        }

        /// <summary>
        /// Parse form data from html string
        /// </summary>
        /// <param name="html">Html to parse</param>
        public IList<Form> ParseForms(string html)
        {
            var forms = new List<Form>();
            var matches = Form.FormRegex.Matches(html);

            foreach (Match match in matches)
            {
                if (match.Groups[Form.ACTION_GROUP].Success)
                {
                    Uri action;
                    Uri.TryCreate(match.Groups[Form.ACTION_GROUP].Value, UriKind.RelativeOrAbsolute, out action);
                    
                    forms.Add(new Form(action, match.Value, ParseFormFields(match.Value)));
                }
            }
            return forms;
        }

        /// <summary>
        /// Parse form fields from html string
        /// </summary>
        /// <param name="html">Html to parse</param>
        public FormFieldCollection ParseFormFields(string html)
        {
            var formFields = new FormFieldCollection();

            foreach (Match formFieldMatch in FormField.FormFieldRegex.Matches(html))
            {
                string fieldName = null;
                string fieldValue = null;

                foreach (Capture attributeCapture in formFieldMatch.Groups[FormField.ATTRIBUTE_GROUP].Captures)
                {
                    var attributeMatch = FormField.AttributeRegex.Match(attributeCapture.Value);

                    string attributeName = null;
                    string attributeValue = null;

                    if (attributeMatch.Groups[FormField.NAME_GROUP].Success)
                        attributeName = attributeMatch.Groups[FormField.NAME_GROUP].Value;
                    if (attributeMatch.Groups[FormField.VALUE_GROUP].Success)
                        attributeValue = attributeMatch.Groups[FormField.VALUE_GROUP].Value;

                    if (attributeName == "name")
                        fieldName = attributeValue;
                    if (attributeName == "value")
                        fieldValue = attributeValue;
                }
                if (!String.IsNullOrWhiteSpace(fieldName))
                    formFields.Add(new FormField(fieldName, fieldValue));
            }
            return formFields;
        }
    }
}
