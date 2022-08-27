﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Xml;
using WeThePeople_ModdingTool.DataSets;
using WeThePeople_ModdingTool.Factories;
using WeThePeople_ModdingTool.FileUtilities;
using WeThePeople_ModdingTool.Processors;
using WeThePeople_ModdingTool.Windows;

namespace WeThePeople_ModdingTool.Creators
{
    public class EventCreatorEventInfoDone : EventCreatorBase
    {
        private EventProcessor eventProcessor;
        public EventProcessor EventProcessor
        {
            set { eventProcessor = value; }
        }
        private List<KeyValuePair<TextBox, TabItem>> eventInfoDone_TextBox_List = new List<KeyValuePair<TextBox, TabItem>>();
        public System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<System.Windows.Controls.TextBox, System.Windows.Controls.TabItem>> EventInfoDone_TextBox_List
        {
            set { eventInfoDone_TextBox_List = value; }
        }
        
        private TextBox textBox_TriggerInfo_Done;
        public TextBox TextBox_TriggerInfo_Done
        {
            set { textBox_TriggerInfo_Done = value; }
        }

        private TabControl tabControl_templates;
        public TabControl TabControl_templates
        {
            set { tabControl_templates = value; }
        }

        private Button button_CreateEvents;
        public Button Button_CreateEvents
        {
            set { button_CreateEvents = value; }
        }
        public override bool Create()
        {
            if( false == Validate() )
            {
                return false;
            }
            EventInfoDoneWindow eventInfoDoneWindow = new EventInfoDoneWindow();
            if (false == eventInfoDoneWindow.ShowDialog())
            {
                return false;
            }

            KeyValuePair<TextBox, TabItem> keyValuePair = GetCorrespondingTextBox();

            DataSetXML dataSetEventInfos_Done = CreateDataSetXML_EventInfosDone();
            DataSetEventInfoDone dataSetEventInfoDone = eventInfoDoneWindow.DataSetEventInfoDone;
            dataSetEventInfos_Done.TemplateReplaceItems[ReplaceItems.GOLD] = dataSetEventInfoDone.GetGold();
            dataSetEventInfos_Done.TemplateReplaceItems[ReplaceItems.UNIT_CLASS] = dataSetEventInfoDone.GetUnitClass();
            dataSetEventInfos_Done.TemplateReplaceItems[ReplaceItems.UNIT_COUNT] = dataSetEventInfoDone.GetUnitCount();
            dataSetEventInfos_Done.TemplateReplaceItems[ReplaceItems.UNIT_EXPERIENCE] = dataSetEventInfoDone.GetUnitExperience();
            dataSetEventInfos_Done.TemplateReplaceItems[ReplaceItems.KING_RELATION] = dataSetEventInfoDone.GetKingRelation();
            dataSetEventInfos_Done.TemplateReplaceItems[ReplaceItems.YIELD_PRICE] = dataSetEventInfoDone.GetYieldPrice();
            dataSetEventInfos_Done.TemplateReplaceItems[ReplaceItems.DONE_INDEX] = GetDoneIndex();

            if (false == eventProcessor.ProcessAndSet(dataSetEventInfos_Done))
            {
                return false;
            }

            keyValuePair.Key.Text = XMLHelper.FormatKeepIndention(XMLHelper.GetRootNodeListProcessedXML(dataSetEventInfos_Done));
            keyValuePair.Value.Visibility = Visibility.Visible;
            tabControl_templates.SelectedItem = keyValuePair.Value;

            if (false == AddEventTriggerInfoDone(dataSetEventInfos_Done))
            {
                return false;
            }

            button_CreateEvents.IsEnabled = true;
            return true;
        }

        private bool Validate()
        {
            if (null == eventProcessor)
            {
                return false;
            }

            if ( eventInfoDone_TextBox_List.Count <= 0 )
            {
                return false;
            }

            if (null == textBox_TriggerInfo_Done)
            {
                return false;
            }

            if (null == tabControl_templates)
            {
                return false;
            }

            if (null == button_CreateEvents)
            {
                return false;
            }

            return true;
        }
        private string GetDoneIndex()
        {
            return TemplateRepository.Instance.XmlDocumentEventDone.Count.ToString();
        }
        private KeyValuePair<TextBox, TabItem> GetCorrespondingTextBox()
        {
            return eventInfoDone_TextBox_List[TemplateRepository.Instance.XmlDocumentEventDone.Count];
        }

        private DataSetXML CreateDataSetXML_EventInfosDone()
        {
            DataSetFactory dataSetFactory = new DataSetFactory();
            DataSetXML eventInfoDone = dataSetFactory.CreateEventInfo_Done();
            TemplateRepository.Instance.RegisterTemplateEventDone(eventInfoDone);
            return eventInfoDone;
        }

        private bool AddEventTriggerInfoDone(DataSetXML dataSetXML_EventInfoDone)
        {
            DataSetXML eventTriggerInfo_Done = TemplateRepository.Instance.FindByNameXML(DataSetFactory.EventTriggerInfos_Done);
            XmlNodeList nodeEvents = eventTriggerInfo_Done.XmlDocumentProcessed.GetElementsByTagName(DataSetFactory.NODE_EVENTS);
            if (nodeEvents.Count != 1)
            {
                return false;
            }

            XmlNode nodeEvent = nodeEvents[0];

            XmlNode xmlNodeEvent = dataSetXML_EventInfoDone.XmlDocumentProcessed.CreateNode(XmlNodeType.Element, DataSetFactory.NODE_EVENT, eventTriggerInfo_Done.XmlDocumentProcessed.NamespaceURI);
            xmlNodeEvent.InnerText = GetEventInfoDoneTypeName(dataSetXML_EventInfoDone);

            if (true == XMLHelper.ContainsInnerNode(nodeEvent, xmlNodeEvent.InnerText))
            {
                return true;
            }

            XmlNode importedNode = eventTriggerInfo_Done.XmlDocumentProcessed.ImportNode(xmlNodeEvent, true);
            nodeEvent.AppendChild(importedNode);
            textBox_TriggerInfo_Done.Text = XMLHelper.FormatKeepIndention(XMLHelper.GetRootNodeListProcessedXML(eventTriggerInfo_Done));

            return true;
        }

        private string GetEventInfoDoneTypeName(DataSetXML dataSetXML)
        {
            XmlNodeList rootNodeList = XMLHelper.GetRootNodeListProcessedXML(dataSetXML);
            if (rootNodeList.Count != 1)
            {
                return String.Empty;
            }

            XmlNode xmlNodeType = GetEventInfoDoneNode_TYPE(rootNodeList[0].ChildNodes);
            return xmlNodeType.InnerText;
        }

        private XmlNode GetEventInfoDoneNode_TYPE(XmlNodeList xmlNodeList)
        {
            return XMLHelper.FindNodeByName(xmlNodeList, DataSetFactory.NODE_TYPE);
        }
    }
}