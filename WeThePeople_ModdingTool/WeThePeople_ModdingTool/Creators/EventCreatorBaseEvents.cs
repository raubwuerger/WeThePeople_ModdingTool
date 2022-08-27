using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using WeThePeople_ModdingTool.DataSets;
using WeThePeople_ModdingTool.Factories;
using WeThePeople_ModdingTool.FileUtilities;
using WeThePeople_ModdingTool.Processors;

namespace WeThePeople_ModdingTool.Creators
{
    public class EventCreatorBaseEvents : EventCreatorBase
    {
        private EventProcessor eventProcessor;
        public EventProcessor EventProcessor
        {
            set { eventProcessor = value; }
        }

        private TextBox textBox_Python_Start;
        public TextBox TextBox_Python_Start
        {
            set { textBox_Python_Start = value; }
        }

        private TextBox textBox_Python_Done;
        public TextBox TextBox_Python_Done
        {
            set { textBox_Python_Done = value; }
        }

        private TextBox textBox_TriggerInfo_Start;
        public TextBox TextBox_TriggerInfo_Start
        {
            set { textBox_TriggerInfo_Start = value; }
        }

        private TextBox textBox_TriggerInfo_Done;
        public TextBox TextBox_TriggerInfo_Done
        {
            set { textBox_TriggerInfo_Done = value; }
        }

        private TextBox textBox_EventGameText;
        public TextBox TextBox_EventGameText
        {
            set { textBox_EventGameText = value; }
        }

        private ComboBox comboBox_Yield;
        public ComboBox ComboBox_Yield
        {
            set { comboBox_Yield = value; }
        }

        private ComboBox comboBox_Harbours;
        public ComboBox ComboBox_Harbours
        {
            set { comboBox_Harbours = value; }
        }

        private Button button_LoadTemplates;
        public Button Button_LoadTemplates
        {
            set { button_LoadTemplates = value; }
        }

        private Button button_CreateEventInfoStartXML;
        public Button Button_CreateEventInfoStartXML
        {
            set { button_CreateEventInfoStartXML = value; }
        }

        private Button button_CreateEvents;
        public Button Button_CreateEvents
        {
            set { button_CreateEvents = value; }
        }
        public override bool Create()
        {
            textBox_Python_Start.Text = eventProcessor.Process(TemplateRepository.Instance.FindByNamePython(DataSetFactory.RandomEvent_Start));
            textBox_Python_Done.Text = eventProcessor.Process(TemplateRepository.Instance.FindByNamePython(DataSetFactory.RandomEvent_Done));

            DataSetXML dataSetXMLTriggerInfos_Start = TemplateRepository.Instance.FindByNameXML(DataSetFactory.EventTriggerInfos_Start);
            if (true == eventProcessor.ProcessAndSet(dataSetXMLTriggerInfos_Start))
            {
                textBox_TriggerInfo_Start.Text = XMLHelper.FormatKeepIndention(XMLHelper.GetRootNodeListProcessedXML(dataSetXMLTriggerInfos_Start));
            }

            DataSetXML dataSetXMLTriggerInfos_Done = TemplateRepository.Instance.FindByNameXML(DataSetFactory.EventTriggerInfos_Done);
            if (true == eventProcessor.ProcessAndSet(dataSetXMLTriggerInfos_Done))
            {
                textBox_TriggerInfo_Done.Text = XMLHelper.FormatKeepIndention(XMLHelper.GetRootNodeListProcessedXML(dataSetXMLTriggerInfos_Done));
            }

            DataSetXML dataSetGameText = TemplateRepository.Instance.FindByNameXML(DataSetFactory.EventGameText);
            if (true == eventProcessor.ProcessAndSet(dataSetGameText))
            {
                textBox_EventGameText.Text = XMLHelper.FormatKeepIndention(XMLHelper.GetRootNodeListProcessedXML(dataSetGameText));
            }

            comboBox_Yield.IsEnabled = false;
            comboBox_Harbours.IsEnabled = false;
            button_LoadTemplates.IsEnabled = false;
            button_CreateEventInfoStartXML.IsEnabled = true;
            button_CreateEvents.IsEnabled = false;

            return true;
        }

        private bool Validate()
        {
            if (null == eventProcessor)
            {
                return false;
            }

            if (null == textBox_Python_Start)
            {
                return false;
            }

            if (null == textBox_Python_Done)
            {
                return false;
            }

            if (null == textBox_TriggerInfo_Start)
            {
                return false;
            }

            if (null == textBox_TriggerInfo_Done)
            {
                return false;
            }

            if (null == textBox_EventGameText)
            {
                return false;
            }

            if (null == comboBox_Yield)
            {
                return false;
            }

            if (null == comboBox_Harbours)
            {
                return false;
            }

            if (null == button_LoadTemplates)
            {
                return false;
            }

            if (null == button_CreateEventInfoStartXML)
            {
                return false;
            }

            if (null == button_CreateEvents)
            {
                return false;
            }

            return true;
        }
    }
}
