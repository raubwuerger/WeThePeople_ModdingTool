using System;
using System.Collections.Generic;
using System.Text;
using WeThePeople_ModdingTool.Creators;
using WeThePeople_ModdingTool.Processors;

namespace WeThePeople_ModdingTool.Factories
{
    public class EventCreatorFactory
    {
        public IEventCreator CreateEventInfoStart( MainWindow mainWindow )
        {
            EventCreatorEventInfoStart eventCreatorEventInfoStart = new EventCreatorEventInfoStart();
            eventCreatorEventInfoStart.EventProcessor = CreateEventProcessor(mainWindow);
            eventCreatorEventInfoStart.TextBoxEventInfoStart = mainWindow.TextBox_EventInfo_Start;
            eventCreatorEventInfoStart.TabItemEventInfoStart = mainWindow.CIV4EventInfos_Start;
            eventCreatorEventInfoStart.TabControl = mainWindow.tabControl_templates;
            eventCreatorEventInfoStart.ButtonAddEventInfoDone = mainWindow.button_AddEventInfoDone;

            return eventCreatorEventInfoStart;
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
