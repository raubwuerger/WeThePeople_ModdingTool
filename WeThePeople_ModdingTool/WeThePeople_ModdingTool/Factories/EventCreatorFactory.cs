using ICSharpCode.AvalonEdit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using WeThePeople_ModdingTool.Creators;
using WeThePeople_ModdingTool.Processors;

namespace WeThePeople_ModdingTool.Factories
{
    public class EventCreatorFactory
    {
        public EventCreatorBase CreateEventInfoStart( MainWindow mainWindow )
        {
            EventCreatorEventInfoStart eventCreatorEventInfoStart = new EventCreatorEventInfoStart();
            eventCreatorEventInfoStart.EventProcessor = CreateEventProcessor(mainWindow);
            eventCreatorEventInfoStart.TextBoxEventInfoStart = mainWindow.TextBox_EventInfo_Start;
            eventCreatorEventInfoStart.TabItemEventInfoStart = mainWindow.CIV4EventInfos_Start;
            eventCreatorEventInfoStart.TabControl = mainWindow.tabControl_templates;
            eventCreatorEventInfoStart.ButtonAddEventInfoDone = mainWindow.button_AddEventInfoDone;

            return eventCreatorEventInfoStart;
        }

        public EventCreatorBase CreateEventInfoDone( MainWindow mainWindow )
        {
            EventCreatorEventInfoDone eventCreatorEventInfoDone = new EventCreatorEventInfoDone();
            eventCreatorEventInfoDone.EventProcessor = CreateEventProcessor(mainWindow);
            eventCreatorEventInfoDone.Button_CreateEvents = mainWindow.button_CreateEvents;
            eventCreatorEventInfoDone.EventInfoDone_TextBox_List = CreateEventInfoDoneTextBoxTabItemList(mainWindow);
            eventCreatorEventInfoDone.TabControl_templates = mainWindow.tabControl_templates;
            eventCreatorEventInfoDone.TextBox_TriggerInfo_Done = mainWindow.TextBox_TriggerInfo_Done;

            return eventCreatorEventInfoDone;
        }

        public EventCreatorFilesPutTogether CreateEventCreatorFilesPutTogether( MainWindow mainWindow )
        {
            EventCreatorFilesPutTogether eventCreatorFilesPutTogether = new EventCreatorFilesPutTogether();
            eventCreatorFilesPutTogether.YieldType = mainWindow.ComboBox_Yield.SelectedItem.ToString();
            eventCreatorFilesPutTogether.Harbour = mainWindow.comboBox_Harbours.SelectedItem.ToString();
            return eventCreatorFilesPutTogether;
        }

        public EventCreatorBaseEvents CreateEventCreatorBaseEvents( MainWindow mainWindow )
        {
            EventCreatorBaseEvents eventCreatorBaseEvents = new EventCreatorBaseEvents();
            eventCreatorBaseEvents.EventProcessor = CreateEventProcessor(mainWindow);
            eventCreatorBaseEvents.ComboBox_Harbours = mainWindow.comboBox_Harbours;
            eventCreatorBaseEvents.ComboBox_Yield = mainWindow.ComboBox_Yield;
            eventCreatorBaseEvents.Button_CreateEventInfoStartXML = mainWindow.button_CreateEventInfoStartXML;
            eventCreatorBaseEvents.Button_CreateEvents = mainWindow.button_CreateEvents;
            eventCreatorBaseEvents.Button_LoadTemplates = mainWindow.button_LoadTemplates;
            eventCreatorBaseEvents.TextBox_EventGameText = mainWindow.TextBox_EventGameText;
            eventCreatorBaseEvents.TextBox_Python_Done = mainWindow.TextBox_Python_Done;
            eventCreatorBaseEvents.TextBox_Python_Start = mainWindow.TextBox_Python_Start;
            eventCreatorBaseEvents.TextBox_TriggerInfo_Done = mainWindow.TextBox_TriggerInfo_Done;
            eventCreatorBaseEvents.TextBox_TriggerInfo_Start = mainWindow.TextBox_TriggerInfo_Start;
            return eventCreatorBaseEvents;
        }

        public EventCreatorRemoveEventTriggerInfoDone CreateEventCreatorRemoveEventTriggerInfoDone( MainWindow mainWindow )
        {
            EventCreatorRemoveEventTriggerInfoDone eventCreatorRemoveEventTriggerInfoDone = new EventCreatorRemoveEventTriggerInfoDone();
            eventCreatorRemoveEventTriggerInfoDone.TextBox_TriggerInfo_Done = mainWindow.TextBox_TriggerInfo_Done;
            eventCreatorRemoveEventTriggerInfoDone.TabItem = mainWindow.tabItemToDelete;
            return eventCreatorRemoveEventTriggerInfoDone;
        }

        private List<KeyValuePair<TextEditor, TabItem>> CreateEventInfoDoneTextBoxTabItemList( MainWindow mainWindow )
        {
            List<KeyValuePair<TextEditor, TabItem>> list = new List<KeyValuePair<TextEditor, TabItem>>();

            list.Add(new KeyValuePair<TextEditor, TabItem>(mainWindow.TextBox_EventInfo_Done_1, mainWindow.TabItem_EventInfo_Done_1));
            list.Add(new KeyValuePair<TextEditor, TabItem>(mainWindow.TextBox_EventInfo_Done_2, mainWindow.TabItem_EventInfo_Done_2));
            list.Add(new KeyValuePair<TextEditor, TabItem>(mainWindow.TextBox_EventInfo_Done_3, mainWindow.TabItem_EventInfo_Done_3));
            list.Add(new KeyValuePair<TextEditor, TabItem>(mainWindow.TextBox_EventInfo_Done_4, mainWindow.TabItem_EventInfo_Done_4));
            list.Add(new KeyValuePair<TextEditor, TabItem>(mainWindow.TextBox_EventInfo_Done_5, mainWindow.TabItem_EventInfo_Done_5));
            list.Add(new KeyValuePair<TextEditor, TabItem>(mainWindow.TextBox_EventInfo_Done_6, mainWindow.TabItem_EventInfo_Done_6));
            return list;
        }

        private EventProcessor CreateEventProcessor( MainWindow mainWindow )
        {
            EventProcessor eventProcessor = new EventProcessor();
            eventProcessor.YieldType = mainWindow.ComboBox_Yield.SelectedItem.ToString();
            eventProcessor.Harbour = mainWindow.comboBox_Harbours.SelectedItem.ToString();
            return eventProcessor;
        }
    }
}
