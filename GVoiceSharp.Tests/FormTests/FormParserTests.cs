using GVoiceSharp.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GVoiceSharp.Tests.FormTests
{
    [TestClass]
    public class FormParserTests
    {
        [TestMethod]
        public void ParseMultipleForms()
        {
            // Arrange
            const string html = @"
<html><body>
<form action=""/testing"" name=""form1"">
<input type=""hidden"" name=""test1"" value=""testvalue1""></input>
<input type=""hidden"" name=""test2"" value=""testvalue2""/>
<input type='hidden' name='test3' value='testvalue3'/>
</form>
<form action=""https://www.testing.test/testing"">
<input type=""hidden"" name=""test4"" value=""testvalue4""></input>
<input type=""hidden"" name=""test5"" value=""testvalue5""/>
<input type='hidden' name='test6' value='testvalue6'/>
</form>
</body></html>";
            var parser = new FormParser();

            // Act
            var forms = parser.ParseForms(html);
            
            // Assert
            Assert.AreEqual(2, forms.Count);
            Assert.AreEqual(3, forms[0].Fields.Count);
            Assert.AreEqual(3, forms[1].Fields.Count);
        }
    }
}
