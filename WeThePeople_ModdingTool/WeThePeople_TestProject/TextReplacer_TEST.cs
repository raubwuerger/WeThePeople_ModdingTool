using NUnit.Framework;
using WeThePeople_ModdingTool;
using WeThePeople_ModdingTool.FileUtilities;

namespace WeThePeople_TestProject
{
    public class TextReplacer_Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void NullStringTest()
        {
            
            Assert.IsNull( TextReplacer.replace(null, null) );
        }

        [Test]
        public void EmptyStringTest()
        {
            Assert.IsNull(TextReplacer.replace("", null) );
        }

        [Test]
        public void BlankStringTest()
        {
//            Assert.IsNull(xmlFileParser.Load("    "));
        }

        [Test]
        public void FileNotFoundTest()
        {
//            Assert.IsNull(xmlFileParser.Load("C:\\DieseDateiGibtEsNicht.xml"));
        }

        [Test]
        public void NotAnXMLFileTest()
        {
//            Assert.IsNull(xmlFileParser.Load("D:\\C#\\WeThePeople_ModdingTool\\WeThePeople_ModdingTool\\WeThePeople_TestProject\\bin\\Debug\\netcoreapp3.1\\..\\..\\..\\testData\\NotAnXMLFile.txt"));
        }

        [Test]
        public void InvalidXMLFileTest()
        {
//            Assert.IsNull(xmlFileParser.Load("D:\\C#\\WeThePeople_ModdingTool\\WeThePeople_ModdingTool\\WeThePeople_TestProject\\bin\\Debug\\netcoreapp3.1\\..\\..\\..\\testData\\InValidXMLFile.xml"));
        }

        [Test]
        public void ValidXMLFileTest()
        {
//            Assert.IsNotNull(xmlFileParser.Load("D:\\C#\\WeThePeople_ModdingTool\\WeThePeople_ModdingTool\\WeThePeople_TestProject\\bin\\Debug\\netcoreapp3.1\\..\\..\\..\\testData\\ValidXMLFile.xml"));
        }
    }
}