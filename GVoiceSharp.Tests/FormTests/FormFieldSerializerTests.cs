using GVoiceSharp.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GVoiceSharp.Tests.FormTests
{
    [TestClass]
    public class FormFieldSerializerTests
    {
        [TestMethod]
        public void TestUrlEncodedSerialize()
        {
            // Arrange
            var fields = new FormFieldCollection
            {
                new FormField("test1", "test1Value"),
                new FormField("Test2", "test2Value"),
                new FormField("Test3", "test \n=&?/+")
            };
            var serializer = new FormFieldSerializer();
            
            // Act
            var serialized = serializer.Serialize(fields);

            // Assert
            Assert.AreEqual("test1=test1Value&Test2=test2Value&Test3=test+%0a%3d%26%3f%2f%2b", serialized);
        }
    }
}