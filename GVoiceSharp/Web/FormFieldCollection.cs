using System.Collections;
using System.Collections.Generic;

namespace GVoiceSharp.Web
{
    /// <summary>
    /// Collection of form field data
    /// </summary>
    internal sealed class FormFieldCollection : IEnumerable<FormField>
    {
        private readonly Dictionary<string, FormField> _formFields;

        /// <summary>
        /// Initializes a new instance of <see cref="FormFieldCollection"/>
        /// </summary>
        public FormFieldCollection()
        {
            _formFields = new Dictionary<string, FormField>();
        }

        /// <summary>
        /// Gets the number of form fields in the collection
        /// </summary>
        public int Count
        {
            get { return _formFields.Count; }
        }

        /// <summary>
        /// Adds a specified form field to the collection
        /// </summary>
        /// <param name="formField">Form field to add</param>
        public void Add(FormField formField)
        {
            _formFields.Add(formField.Name, formField);
        }

        /// <summary>
        /// Determines whether the collection contains the form field by name
        /// </summary>
        /// <param name="name">Name of the form field</param>
        public bool ContainsField(string name)
        {
            return _formFields.ContainsKey(name);
        }

        /// <summary>
        /// Gets a form field by name
        /// </summary>
        /// <param name="name">Name of the form field</param>
        public FormField this[string name]
        {
            get { return _formFields[name]; }
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        public IEnumerator<FormField> GetEnumerator()
        {
            return _formFields.Values.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}