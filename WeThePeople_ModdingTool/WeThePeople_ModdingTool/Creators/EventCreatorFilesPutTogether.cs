﻿using Serilog;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using System.Xml;
using WeThePeople_ModdingTool.ContentInserter;
using WeThePeople_ModdingTool.DataSets;
using WeThePeople_ModdingTool.Factories;
using WeThePeople_ModdingTool.FileUtilities;
using WeThePeople_ModdingTool.Helper;
using WeThePeople_ModdingTool.Validators;

namespace WeThePeople_ModdingTool.Creators
{
    public class EventCreatorFilesPutTogether : EventCreatorBase
    {
        public static string _RootNode_CIV4EVENTTRIGGERINFOS = "Civ4EventTriggerInfos";
        public static string _Subnode_EventTriggerInfos = "EventTriggerInfos";

        public static string _RootNode_CIV4EVENTINFOS = "Civ4EventInfos";
        public static string _Subnode_EventInfos = "EventInfos";

        public override bool Create()
        {
            if (false == GetSavePath())
            {
                return false;
            }

            if (false == EventCreatorHelper.IsValid(this))
            {
                return false;
            }

            if (false == ConcatenatePythonFiles())
            {
                return false;
            }

            if (false == ConcatenateXMLFiles())
            {
                return false;
            }

            return true;
        }

        private bool GetSavePath()
        {
            var dialog = new FolderBrowserDialog();
            if (DialogResult.OK != dialog.ShowDialog())
            {
                return false;
            }

            SavePath = dialog.SelectedPath;
            return true;
        }

        private bool ConcatenatePythonFiles()
        {
            DataSetPython dataSetPythonStart = TemplateRepository.Instance.FindByNamePython(DataSetFactory.CvRandomEventInterface_Start);
            DataSetPython dataSetPythonDone = TemplateRepository.Instance.FindByNamePython(DataSetFactory.CvRandomEventInterface_Done);

            if (false == DataSetValidator.ValidateFull(dataSetPythonStart))
            {
                Log.Debug("DataSetPyton start is not valid!");
                return false;
            }
            if (false == DataSetValidator.ValidateFull(dataSetPythonDone))
            {
                Log.Debug("DataSetPyton start is not valid!");
                return false;
            }

            string concatenatedFiles = dataSetPythonStart.PythonContentProcessed;
            concatenatedFiles += CommonVariables.CR;
            concatenatedFiles += dataSetPythonDone.PythonContentProcessed;

            string savePathExtended = PathHelper.CombinePath(SavePath, Path.GetDirectoryName(dataSetPythonDone.TemplateFileNameRelativ));
            string completeFileName = PathHelper.CombinePathAndFileName(savePathExtended, dataSetPythonStart.OriginalFileName);

            MessageBoxResult messageBoxResult = CheckResultFile(completeFileName);
            if (messageBoxResult == MessageBoxResult.Cancel)
            {
                return true;
            }
            else if (messageBoxResult == MessageBoxResult.Yes)
            {
                return Overwrite(completeFileName, concatenatedFiles);
            }
            else if (messageBoxResult == MessageBoxResult.No)
            {
                return Append(completeFileName, concatenatedFiles);
            }
            return false;
        }

        private bool Append(string fileName, string content)
        {
            ContentInserterBase contentInserterBase = ContentInserterFactory.CreateContentInserterPython(fileName);
            return contentInserterBase.Insert(content);
        }

        private bool Overwrite(string fileName, string content)
        {
            return TextFileUtility.SaveCreatePathOverwrite(fileName, content);
        }

        private bool ConcatenateXMLFiles()
        {
            if (false == CreateCiv4GameText())
            {
                return false;
            }

            if (false == CreateEventTriggerInfo())
            {
                return false;
            }

            if (false == CreateEventInfo())
            {
                return false;
            }

            return true;
        }

        private bool CreateCiv4GameText()
        {
            DataSetXML eventGameText = TemplateRepository.Instance.FindByNameXML(DataSetFactory.CIV4GameText_Colonization_Events_utf8_Start);

            string completeFileName = PathHelper.CombinePathAndFileName(PathHelper.CombinePath(SavePath, Path.GetDirectoryName(eventGameText.TemplateFileNameRelativ)), eventGameText.OriginalFileName);

            MessageBoxResult messageBoxResult = CheckResultFile(completeFileName);
            if (messageBoxResult == MessageBoxResult.Cancel)
            {
                return true;
            }
            else if (messageBoxResult == MessageBoxResult.Yes)
            {
                return Overwrite(completeFileName, eventGameText.XmlDocumentProcessed);
            }
            else if (messageBoxResult == MessageBoxResult.No)
            {
                return Append(completeFileName, eventGameText);
            }
            return false;
        }

        private bool Append(string fileName, DataSetXML content)
        {
            ContentInserterBase contentInserterBase = ContentInserterFactory.CreateContentInserterByXmlDocument(fileName, content);
            return contentInserterBase.Insert(content.XmlDocumentProcessed);
        }

        private bool Overwrite(string fileName, XmlDocument content)
        {
            return XMLFileUtility.SaveCreatePathOverwrite(fileName, content);
        }

        private bool CreateEventTriggerInfo()
        {
            List<DataSetXML> dataSetXMLs = new List<DataSetXML>();
            dataSetXMLs.Add(TemplateRepository.Instance.FindByNameXML(DataSetFactory.CIV4EventTriggerInfos_Start));
            dataSetXMLs.Add(TemplateRepository.Instance.FindByNameXML(DataSetFactory.CIV4EventTriggerInfos_Done));

            List<XmlDocument> xmlDocuments = new List<XmlDocument>();
            xmlDocuments.AddRange(DataSetConverter.CreateList(dataSetXMLs));
            XmlDocument concatenated = Concatenate(xmlDocuments, DataSetFactory.EventTriggerInfo_ParentNode, DataSetFactory.EventTriggerInfo_InsertNode);
            if (null == concatenated)
            {
                return false;
            }

            string completeFileName = PathHelper.CombinePathAndFileName(PathHelper.CombinePath(SavePath, Path.GetDirectoryName(dataSetXMLs[0].TemplateFileNameRelativ)), dataSetXMLs[0].OriginalFileName);

            MessageBoxResult messageBoxResult = CheckResultFile(completeFileName);
            if (messageBoxResult == MessageBoxResult.Cancel)
            {
                return true;
            }
            else if (messageBoxResult == MessageBoxResult.Yes)
            {
                return XMLFileUtility.SaveCreatePathOverwrite(completeFileName, concatenated);
            }
            else if (messageBoxResult == MessageBoxResult.No)
            {
                dataSetXMLs[0].XmlDocumentProcessed = concatenated;
                return Append(completeFileName, dataSetXMLs[0]);
            }
            return false;
        }

        private bool CreateEventInfo()
        {
            List<DataSetXML> eventEventInfos = new List<DataSetXML>();
            eventEventInfos.Add(TemplateRepository.Instance.FindByNameXML(DataSetFactory.CIV4EventInfos_Start));
            foreach (KeyValuePair<string, DataSetXML> entry in TemplateRepository.Instance.XmlDocumentEventDone)
            {
                eventEventInfos.Add(entry.Value);
            }

            List<XmlDocument> xmlDocuments = new List<XmlDocument>();
            xmlDocuments.AddRange(DataSetConverter.CreateList(eventEventInfos));
            XmlDocument concatenated = Concatenate(xmlDocuments, DataSetFactory.EventInfo_ParentNode, DataSetFactory.EventInfo_InsertNode);
            if (null == concatenated)
            {
                return false;
            }

            string completeFileName = PathHelper.CombinePathAndFileName(PathHelper.CombinePath(SavePath, Path.GetDirectoryName(eventEventInfos[0].TemplateFileNameRelativ)), eventEventInfos[0].OriginalFileName);

            MessageBoxResult messageBoxResult = CheckResultFile(completeFileName);
            if (messageBoxResult == MessageBoxResult.Cancel)
            {
                return true;
            }
            else if (messageBoxResult == MessageBoxResult.Yes)
            {
                return XMLFileUtility.SaveCreatePathOverwrite(completeFileName, concatenated);
            }
            else if (messageBoxResult == MessageBoxResult.No)
            {
                eventEventInfos[0].XmlDocumentProcessed = concatenated;
                return Append(completeFileName, eventEventInfos[0]);
            }
            return false;
        }

        private XmlDocument Concatenate(List<XmlDocument> listToConcatenate, string attachDest, string nodeToAttach)
        {
            if (listToConcatenate.Count < 0)
            {
                return null;
            }

            if (listToConcatenate.Count == 1)
            {
                return listToConcatenate[0];
            }

            XmlDocument concatenated = listToConcatenate[0];
            for (int i = 0; i < listToConcatenate.Count - 1; i++)
            {
                //TODO: Die Ausgangslage ist schon nicht sauber! Warum ist in der Liste ein null-Object?
                int newIndex = i + 1;
                if (listToConcatenate[newIndex] == null || listToConcatenate[newIndex].DocumentElement == null || newIndex >= listToConcatenate.Count)
                {
                    break;
                }

                XmlNodeList xmlNodeListAttachSource = listToConcatenate[newIndex].GetElementsByTagName(nodeToAttach);
                if (xmlNodeListAttachSource.Count != 1)
                {
                    Log.Debug("XmlDocument has invalid count of sub node: " + nodeToAttach);
                    return null;
                }

                XmlNodeList xmlNodeListAttachDest = concatenated.GetElementsByTagName(attachDest);
                if (xmlNodeListAttachDest.Count != 1)
                {
                    Log.Debug("XmlDocument has invalid count of sub node: " + attachDest);
                    return null;
                }

                concatenated = Concatenate(concatenated, xmlNodeListAttachDest[0], xmlNodeListAttachSource[0]);
            }

            return concatenated;
        }

        private XmlDocument Concatenate(XmlDocument baseXML, XmlNode xmlDest, XmlNode addXMLNode)
        {
            XmlNode importedDocument = baseXML.ImportNode(addXMLNode, true);
            xmlDest.AppendChild(importedDocument);
            return baseXML;
        }

        private MessageBoxResult CheckResultFile(string fileToSave)
        {
            if (false == File.Exists(fileToSave))
            {
                return MessageBoxResult.Yes;
            }

            return CommonMessageBox.Show_Question_YesNoCancel("File already exists!", "The file already exists! " + fileToSave + CommonVariables.CR + CommonVariables.CR
                + CommonVariables.MESSAGE_BOX_YESNOCANCEL_QUESTION + CommonVariables.CR + CommonVariables.MESSAGE_BOX_YESNOCANCEL);
        }
    }
}
