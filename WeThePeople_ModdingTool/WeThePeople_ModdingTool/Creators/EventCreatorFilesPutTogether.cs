﻿using System;
using System.Collections.Generic;
using System.Text;
using WeThePeople_ModdingTool.DataSets;
using WeThePeople_ModdingTool.Factories;
using WeThePeople_ModdingTool.Validators;
using Serilog;
using WeThePeople_ModdingTool.FileUtilities;
using WeThePeople_ModdingTool.Helper;
using System.Xml;
using System.Windows.Forms;
using System.Windows;
using System.IO;
using WeThePeople_ModdingTool.ContentInserter;

namespace WeThePeople_ModdingTool.Creators
{
    public class EventCreatorFilesPutTogether : EventCreatorBase
    {
        public static string RootNodeBase_Civ4GameText = "Civ4GameText";

        public static string RootNode_CIV4EVENTTRIGGERINFOS = "Civ4EventTriggerInfos";
        public static string Subnode_EventTriggerInfos = "EventTriggerInfos";

        public static string RootNode_CIV4EVENTINFOS = "Civ4EventInfos";
        public static string Subnode_EventInfos = "EventInfos";

        public override bool Create()
        {
            if( false == GetSavePath() )
            {
                return false;
            }

            if (false == EventCreatorHelper.IsValid(this))
            {
                return false;
            }

            if ( false == ConcatenatePythonFiles() )
            {
                return false;
            }

            if( false == ConcatenateXMLFiles() )
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

            if( false == DataSetValidator.ValidateFull(dataSetPythonStart) )
            {
                Log.Debug("DataSetPyton start is not valid!");
                return false;
            }
            if( false == DataSetValidator.ValidateFull(dataSetPythonDone) )
            {
                Log.Debug("DataSetPyton start is not valid!");
                return false;
            }

            string concatenatedFiles = dataSetPythonStart.PythonContentProcessed;
            concatenatedFiles += CommonVariables.CR;
            concatenatedFiles += dataSetPythonDone.PythonContentProcessed;

            string savePathExtended = PathHelper.PathCombine(SavePath,Path.GetDirectoryName(dataSetPythonDone.TemplateFileNameRelativ));
            string completeFileName = PathHelper.CombinePathAndFileName(savePathExtended, dataSetPythonStart.OriginalFileName);

            MessageBoxResult messageBoxResult = CheckResultFile(completeFileName);
            if (messageBoxResult == MessageBoxResult.Cancel)
            {
                return true;
            }
            else if (messageBoxResult == MessageBoxResult.Yes)
            {
                return TextFileUtility.SaveCreatePathOverwrite(completeFileName, concatenatedFiles);
            }
            else if (messageBoxResult == MessageBoxResult.No)
            {
                return Append(completeFileName, concatenatedFiles);
            }
            return false;
        }

        private bool Append( string fileName, string content )
        {
            ContentInserterPython contentInserterPython = new ContentInserterPython();
            contentInserterPython.FileName = fileName;
            return contentInserterPython.Insert(content);
        }

        private bool ConcatenateXMLFiles()
        {
            if( false == CreateCiv4GameText() )
            {
                return false;
            }

            if( false == CreateEventTriggerInfo() )
            {
                return false;
            }

            if( false == CreateEventInfo() )
            {
                return false;
            }

            return true;
        }

        private bool CreateCiv4GameText()
        {
            DataSetXML eventGameText = TemplateRepository.Instance.FindByNameXML(DataSetFactory.CIV4GameText_Colonization_Events_utf8_Start);

            string completeFileName = PathHelper.CombinePathAndFileName(PathHelper.PathCombine(SavePath, Path.GetDirectoryName(eventGameText.TemplateFileNameRelativ)), eventGameText.OriginalFileName);

            MessageBoxResult messageBoxResult = CheckResultFile(completeFileName);
            if (messageBoxResult == MessageBoxResult.Cancel)
            {
                return true;
            }
            else if (messageBoxResult == MessageBoxResult.Yes)
            {
                return XMLFileUtility.SaveCreatePathOverwrite(completeFileName, eventGameText.XmlDocumentProcessed);
            }
            else if (messageBoxResult == MessageBoxResult.No)
            {
                CommonMessageBox.Show_Info("Append content", "Append content not implemented!");
                return true;
            }
            return false;
        }

        private bool CreateEventTriggerInfo()
        {
            List<DataSetXML> dataSetXMLs = new List<DataSetXML>();
            dataSetXMLs.Add(TemplateRepository.Instance.FindByNameXML(DataSetFactory.CIV4EventTriggerInfos_Start));
            dataSetXMLs.Add(TemplateRepository.Instance.FindByNameXML(DataSetFactory.CIV4EventTriggerInfos_Done));

            List<XmlDocument> xmlDocuments = new List<XmlDocument>();
            xmlDocuments.AddRange(DataSetConverter.CreateList(dataSetXMLs));
            XmlDocument concatenated = Concatenate(xmlDocuments, Subnode_EventTriggerInfos, DataSetFactory.ConcreteNode_EventTriggerInfo);
            if (null == concatenated)
            {
                return false;
            }

            string completeFileName = PathHelper.CombinePathAndFileName(PathHelper.PathCombine(SavePath, Path.GetDirectoryName(dataSetXMLs[0].TemplateFileNameRelativ)), dataSetXMLs[0].OriginalFileName );

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
                CommonMessageBox.Show_Info("Append content", "Append content not implemented!");
                return true;
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
            XmlDocument concatenated = Concatenate(xmlDocuments, Subnode_EventInfos, DataSetFactory.ConcreteNode_EventInfo);
            if (null == concatenated)
            {
                return false;
            }

            string completeFileName = PathHelper.CombinePathAndFileName(PathHelper.PathCombine(SavePath, Path.GetDirectoryName(eventEventInfos[0].TemplateFileNameRelativ)), eventEventInfos[0].OriginalFileName);

            MessageBoxResult messageBoxResult = CheckResultFile(completeFileName);
            if (messageBoxResult == MessageBoxResult.Cancel )
            {
                return true;
            }
            else if(messageBoxResult == MessageBoxResult.Yes)
            {
                return XMLFileUtility.SaveCreatePathOverwrite(completeFileName, concatenated);
            }
            else if(messageBoxResult == MessageBoxResult.No)
            {
                CommonMessageBox.Show_Info("Append content", "Append content not implemented!");
                return true;
            }
            return false;
        }

        private XmlDocument Concatenate( List<XmlDocument> listToConcatenate, string attachDest, string nodeToAttach)
        {
            if( listToConcatenate.Count < 0 )
            {
                return null;
            }

            if (listToConcatenate.Count == 1)
            {
                return listToConcatenate[0];
            }

            XmlDocument concatenated = listToConcatenate[0];
            for ( int i=0;i<listToConcatenate.Count - 1;i++ )
            {
                //TODO: Die Ausgangslage ist schon nicht sauber! Warum ist in der Liste ein null-Object?
                int newIndex = i + 1;
                if(listToConcatenate[newIndex] == null || listToConcatenate[newIndex].DocumentElement == null || newIndex >= listToConcatenate.Count )
                {
                    break;
                }

                XmlNodeList xmlNodeListAttachSource = listToConcatenate[newIndex].GetElementsByTagName(nodeToAttach);
                if( xmlNodeListAttachSource.Count != 1 )
                {
                    Log.Debug("XmlDocument has invalid count of sub node: " +nodeToAttach);
                    return null;
                }

                XmlNodeList xmlNodeListAttachDest = concatenated.GetElementsByTagName(attachDest);
                if( xmlNodeListAttachDest.Count != 1 )
                {
                    Log.Debug("XmlDocument has invalid count of sub node: " + attachDest);
                    return null;
                }

                concatenated = Concatenate(concatenated, xmlNodeListAttachDest[0], xmlNodeListAttachSource[0] );
            }

            return concatenated;
        }

        private XmlDocument Concatenate( XmlDocument baseXML, XmlNode xmlDest, XmlNode addXMLNode )
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
