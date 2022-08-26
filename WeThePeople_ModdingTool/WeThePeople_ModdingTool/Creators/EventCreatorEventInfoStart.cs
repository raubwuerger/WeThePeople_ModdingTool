using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using WeThePeople_ModdingTool.DataSets;
using WeThePeople_ModdingTool.Factories;
using WeThePeople_ModdingTool.FileUtilities;
using WeThePeople_ModdingTool.Processors;
using WeThePeople_ModdingTool.Windows;

namespace WeThePeople_ModdingTool.Creators
{
    public class EventCreatorEventInfoStart : IEventCreator
    {
        private EventProcessor eventProcessor;
        public EventProcessor EventProcessor
        {
            set { eventProcessor = value; }
        }

        private TextBox textBoxEventInfoStart;
        public TextBox TextBoxEventInfoStart
        {
            set { textBoxEventInfoStart = value; }
        }

        private TabItem tabItemEventInfoStart;
        public TabItem TabItemEventInfoStart
        {
            set { tabItemEventInfoStart = value; }
        }

        private TabControl tabControl;
        public TabControl TabControl
        {
            set { tabControl = value; }
        }

        private Button buttonAddEventInfoDone;
        public Button ButtonAddEventInfoDone
        {
            set { buttonAddEventInfoDone = value; }
        }
        public override bool Create()
        {
            if( false == Validate() )
            {
                return false;
            }
            EventInfoStartWindow eventInfoStartWindow = new EventInfoStartWindow();
            if (false == eventInfoStartWindow.ShowDialog())
            {
                return false;
            }

            DataSetEventInfoStart dataSetEventInfo = eventInfoStartWindow.DataSetEventInfoStart;
            DataSetXML dataSetEventInfos_Start = TemplateRepository.Instance.FindByNameXML(DataSetFactory.EventInfos_Start);
            dataSetEventInfos_Start.TemplateReplaceItems[ReplaceItems.TRIGGER_VALUE_START] = dataSetEventInfo.GetTriggerValueStart();
            dataSetEventInfos_Start.TemplateReplaceItems[ReplaceItems.TRIGGER_VALUE_DONE] = dataSetEventInfo.GetTriggerValueDone();

            dataSetEventInfos_Start.XmlDocumentProcessed = eventProcessor.Process(dataSetEventInfos_Start);
            Debug.Assert(dataSetEventInfos_Start.XmlDocumentProcessed != null, "Should succeed!s");
            textBoxEventInfoStart.Text = XMLHelper.FormatKeepIndention(XMLHelper.GetRootNodeListProcessedXML(dataSetEventInfos_Start));
            tabItemEventInfoStart.Visibility = Visibility.Visible;
            tabControl.SelectedItem = tabItemEventInfoStart;
            buttonAddEventInfoDone.IsEnabled = true;
            return true;
        }

        private bool Validate()
        {
            if( null == eventProcessor )
            {
                return false;
            }

            if( null == textBoxEventInfoStart )
            {
                return false;
            }

            if( null == tabItemEventInfoStart )
            {
                return false;
            }

            if( null == tabControl )
            {
                return false;
            }

            if( null == buttonAddEventInfoDone )
            {
                return false;
            }

            return true;
        }
    }
}
