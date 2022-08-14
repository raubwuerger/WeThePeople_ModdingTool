using NUnit.Framework;
using System;
using WeThePeople_ModdingTool.FileUtilities;

namespace WeThePeople_TestProject
{
    public class XMLFileUtility_Tests
    {
        string nonExistingFile;
        string notAnXMLFile;
        string invalidXMLFile;
        string validXMLFile;

        XMLFileUtility xmlFileParser;
        [SetUp]
        public void Setup()
        {
            string absoluteProgramPath = AppDomain.CurrentDomain.BaseDirectory;
            string assetPathRelative = @"..\..\..\testData";
            string relativeAssetPath = System.IO.Path.Combine(absoluteProgramPath, assetPathRelative);

            nonExistingFile = System.IO.Path.Combine(relativeAssetPath, @"nonExistingFile.xml");
            notAnXMLFile = System.IO.Path.Combine(relativeAssetPath, @"NotAnXMLFile.txt");
            invalidXMLFile = System.IO.Path.Combine(relativeAssetPath, @"InValidXMLFile.xml");
            validXMLFile = System.IO.Path.Combine(relativeAssetPath, @"ValidXMLFile.xml");

            CommonMessageBox.ShowMessageBoxes = false;

            xmlFileParser = new XMLFileUtility();
        }

        [Test]
        public void NullStringTest()
        {
            Assert.IsNull(xmlFileParser.Load(null));
        }

        [Test]
        public void EmptyStringTest()
        {
            Assert.IsNull(xmlFileParser.Load(""));
        }

        [Test]
        public void BlankStringTest()
        {
            Assert.IsNull(xmlFileParser.Load("    "));
        }

        [Test]
        public void FileNotFoundTest()
        {
            Assert.IsNull(xmlFileParser.Load(nonExistingFile));
        }

        [Test]
        public void NotAnXMLFileTest()
        {
            Assert.IsNull(xmlFileParser.Load(notAnXMLFile));
        }

        [Test]
        public void InvalidXMLFileTest()
        {
            Assert.IsNull(xmlFileParser.Load(invalidXMLFile));
        }

        [Test]
        public void ValidXMLFileTest()
        {
            Assert.IsNotNull(xmlFileParser.Load(validXMLFile));
        }
    }
}