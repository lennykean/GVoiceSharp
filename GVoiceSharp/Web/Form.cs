using System;
using System.Text.RegularExpressions;

namespace GVoiceSharp.Web
{
    /// <summary>
    /// Represents an html form
    /// </summary>
    internal sealed class Form
    {
        internal const string ACTION_GROUP = "action";

        internal static readonly Regex FormRegex = new Regex(@"<\s*form(.|\s)*?action=(""|')(?<action>[^""^']*)(""|')\s*[^>]*>(.|\s)*?<\s*/\s*form\s*>", RegexOptions.Compiled);

        /// <param name="action">Form action</param>
        /// <param name="html">Html of the form</param>
        /// <param name="fields">Form fields</param>
        public Form(Uri action, string html, FormFieldCollection fields)
        {
            Fields = fields;
            Html = html;
            Action = action;
        }

        /// <summary>
        /// Form action
        /// </summary>
        public Uri Action { get; private set; }
        /// <summary>
        /// Form html
        /// </summary>
        public string Html { get; private set; }
        /// <summary>
        /// Form field data
        /// </summary>
        public FormFieldCollection Fields { get; private set; }
    }
}