using NUnit.Framework;
using System;
using WeThePeople_ModdingTool.DataSets;
using WeThePeople_ModdingTool.Validators;

namespace WeThePeople_TestProject
{
    public class DataSetValidator_TEST
    {
        DataSetBase dataSetBase_Null;
        DataSetBase dataSetBase_Empty = new DataSetBase();
        DataSetBase dataSetBase_TemplateNameEmpty = new DataSetBase();
        DataSetBase dataSetBase_TemplateFileNameRelativEmpty = new DataSetBase();
        DataSetBase dataSetBase_TemplateFileNameAbsoluteEmpty = new DataSetBase();
        DataSetBase dataSetBase_TemplateFileExtensionEmpty = new DataSetBase();
        DataSetBase dataSetBase_TemplateFileNameConcreteEmpty = new DataSetBase();
        DataSetBase dataSetBase_Valid = new DataSetBase();

        DataSetXML dataSetXML_XmlDocumentTemplateEmpty = new DataSetXML();
        DataSetXML dataSetXML_XmlRootNodeEmpty = new DataSetXML();
        DataSetXML dataSet_XMLValid = new DataSetXML();

        DataSetPython dataSetPython_PythonContentTemplateEmpty = new DataSetPython();
        DataSetPython dataSetPython_Valid = new DataSetPython();

        [SetUp]
        public void Setup()
        {
            dataSetBase_Null = null;
            dataSetBase_Empty = new DataSetBase();
            
            CreateValid(dataSetBase_TemplateNameEmpty);
            dataSetBase_TemplateNameEmpty.TemplateName = "";

            CreateValid(dataSetBase_TemplateFileNameRelativEmpty);
            dataSetBase_TemplateFileNameRelativEmpty.TemplateFileNameRelativ = "";

            CreateValid(dataSetBase_TemplateFileNameAbsoluteEmpty);
            dataSetBase_TemplateFileNameAbsoluteEmpty.TemplateFileNameAbsolute = "";

            CreateValid(dataSetBase_TemplateFileExtensionEmpty);
            dataSetBase_TemplateFileExtensionEmpty.TemplateFileExtension = "";

            CreateValid(dataSetBase_TemplateFileNameConcreteEmpty);
            dataSetBase_TemplateFileNameConcreteEmpty.TemplateFileNameConcrete = "";

            CreateValid(dataSetBase_Valid);

            CreateValid(dataSetXML_XmlDocumentTemplateEmpty);
            dataSetXML_XmlDocumentTemplateEmpty.XmlDocumentTemplate = null;

            CreateValid(dataSetXML_XmlRootNodeEmpty);
            dataSetXML_XmlRootNodeEmpty.XmlRootNode = "";

            CreateValid(dataSet_XMLValid);

            CreateValid(dataSetPython_PythonContentTemplateEmpty);
            dataSetPython_PythonContentTemplateEmpty.PythonContentTemplate = "";

            CreateValid(dataSetPython_Valid);
        }

        private void CreateValid( DataSetBase dataSetBase )
        {
            string validString = "Im a valid string";
            dataSetBase.TemplateName = validString;
            dataSetBase.TemplateFileNameRelativ = validString;
            dataSetBase.TemplateFileNameAbsolute = validString;
            dataSetBase.TemplateFileExtension = validString;
            dataSetBase.TemplateFileNameConcrete = validString;
        }

        private void CreateValid( DataSetXML dataSetXML )
        {
            CreateValid( (DataSetBase)dataSetXML );
            dataSetXML.XmlDocumentTemplate = new System.Xml.XmlDocument();
            dataSetXML.XmlRootNode = "RootNode";
        }
        private void CreateValid(DataSetPython dataSetPython)
        {
            CreateValid((DataSetBase)dataSetPython);
            dataSetPython.PythonContentTemplate = "Im a valid python file";
        }

        [Test]
        public void DataSetBaseNullTest()
        {
            Assert.IsFalse(DataSetValidator.Validate(dataSetBase_Null));
        }

        [Test]
        public void DataSetBaseEmptyTest()
        {
            Assert.IsFalse(DataSetValidator.Validate(dataSetBase_Empty));
        }

        [Test]
        public void DataSetBaseTemplateNameEmptyTest()
        {
            Assert.IsFalse(DataSetValidator.Validate(dataSetBase_TemplateNameEmpty));
        }

        [Test]
        public void DataSetBaseTemplateFileNameRelativEmptyTest()
        {
            Assert.IsFalse(DataSetValidator.Validate(dataSetBase_TemplateFileNameRelativEmpty));
        }

        [Test]
        public void DataSetBaseTemplateFileNameAbsoluteEmptyTest()
        {
            Assert.IsFalse(DataSetValidator.Validate(dataSetBase_TemplateFileNameAbsoluteEmpty));
        }

        [Test]
        public void DataSetBaseTemplateFileExtensionEmptyTest()
        {
            Assert.IsFalse(DataSetValidator.Validate(dataSetBase_TemplateFileExtensionEmpty));
        }

        [Test]
        public void DataSetBaseTemplateFileNameConcreteEmptyTest()
        {
            Assert.IsFalse(DataSetValidator.Validate(dataSetBase_TemplateFileNameConcreteEmpty));
        }

        [Test]
        public void DataSetBaseValidTest()
        {
            Assert.IsTrue(DataSetValidator.Validate(dataSetBase_Valid));
        }

        [Test]
        public void DataSetXMLXmlDocumentTemplateEmptyTest()
        {
            Assert.IsFalse(DataSetValidator.Validate(dataSetXML_XmlDocumentTemplateEmpty));
        }

        [Test]
        public void DataSetBaseXmlRootNodeEmptyTest()
        {
            Assert.IsFalse(DataSetValidator.Validate(dataSetXML_XmlRootNodeEmpty));
        }

        [Test]
        public void DataSetXMLValidTest()
        {
            Assert.IsTrue(DataSetValidator.Validate(dataSet_XMLValid));
        }

        [Test]
        public void DataSetPythonPythonContentTemplateEmptyTest()
        {
            Assert.IsFalse(DataSetValidator.Validate(dataSetPython_PythonContentTemplateEmpty));
        }

        [Test]
        public void DataSetPythonValidTest()
        {
            Assert.IsTrue(DataSetValidator.Validate(dataSetPython_Valid));
        }
    }
}