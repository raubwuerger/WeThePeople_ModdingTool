using NUnit.Framework;
using WeThePeople_ModdingTool;

namespace WeThePeople_TestProject
{
    public class Tests
    {
        WeThePeople_ModdingTool.IO.XMLFileParser xmlFileParser;
        [SetUp]
        public void Setup()
        {
            xmlFileParser = new WeThePeople_ModdingTool.IO.XMLFileParser();
        }

        [Test]
        public void NullStringTest()
        {
            Assert.IsNull(xmlFileParser.LoadFile(null));
        }

        [Test]
        public void EmptyStringTest()
        {
            Assert.IsNull(xmlFileParser.LoadFile(""));
        }

        [Test]
        public void BlankStringTest()
        {
            Assert.IsNull(xmlFileParser.LoadFile("    "));
        }

        [Test]
        public void FileNotFoundTest()
        {
            Assert.IsNull(xmlFileParser.LoadFile("C:\\DieseDateiGibtEsNicht.xml"));
        }

        [Test]
        public void InvalidXMLFileTest()
        {
            Assert.IsNull(xmlFileParser.LoadFile("C:\\DieseDateiGibtEsNicht.xml"));
        }

        [Test]
        public void ValidXMLFileTest()
        {
            Assert.IsNull(xmlFileParser.LoadFile("C:\\DieseDateiGibtEsNicht.xml"));
        }
    }
}