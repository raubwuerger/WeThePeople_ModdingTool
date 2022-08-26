using NUnit.Framework;
using System;
using System.Xml;
using WeThePeople_ModdingTool.FileUtilities;
using WeThePeople_ModdingTool.Helper;

namespace WeThePeople_TestProject
{
    public class XMLHelper_Tests
    {
        string findeType = "Type";
        string findeEvents = "Events";
        private static string RootNode_EventTriggerInfo = "/Civ4EventTriggerInfos";
        XmlDocument xmlDocument;

        [SetUp]
        public void Setup()
        {
            string fileNameAbsolute = @"D:\Projekte\C#\WeThePeople_ModdingTool\WeThePeople_ModdingTool\WeThePeople_ModdingTool\templates\Assets\XML\Events\CIV4EventTriggerInfos_Start_Template.xml";
            xmlDocument = XMLFileUtility.Load(fileNameAbsolute);
            XmlNodeList nodeList = xmlDocument.GetElementsByTagName("Events");
        }

        [Test]
        public void FindShouldSucceed()
        {
            Assert.IsNotNull(XMLHelper.FindNodeByName(xmlDocument.DocumentElement.SelectNodes(RootNode_EventTriggerInfo), findeType));
        }

        [Test]
        public void FindShouldSucceedNewMethod()
        {
            Assert.IsNotNull(XMLHelper.GetElementById(xmlDocument.DocumentElement, findeEvents));
        }

        [Test]
        public void FindShouldAlsoSucceed()
        {
            Assert.IsNotNull(XMLHelper.FindNodeByName(xmlDocument.DocumentElement.SelectNodes(RootNode_EventTriggerInfo), findeEvents));
        }

    }
}