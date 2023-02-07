using System.Collections.Generic;

namespace WeThePeople_ModdingTool.DataSets
{
    public class DataSetEventInfoStart
    {
        private KeyValuePair<string, string> trigger_Value_Start = new KeyValuePair<string, string>(ReplaceItems.TRIGGER_VALUE_START, "100");
        public KeyValuePair<string, string> Trigger_Value_Start
        {
            get { return trigger_Value_Start; }
        }
        private KeyValuePair<string, string> trigger_Value_Done = new KeyValuePair<string, string>(ReplaceItems.TRIGGER_VALUE_DONE, "1000");
        public KeyValuePair<string, string> Trigger_Value_Done
        {
            get { return trigger_Value_Done; }
        }
        public void SetTriggerValueStart(string value)
        {
            trigger_Value_Start = new KeyValuePair<string, string>(ReplaceItems.TRIGGER_VALUE_START, value);
        }

        public string GetTriggerValueStart()
        {
            return trigger_Value_Start.Value;
        }

        public void SetTriggerValueDone(string value)
        {
            trigger_Value_Done = new KeyValuePair<string, string>(ReplaceItems.TRIGGER_VALUE_DONE, value);
        }

        public string GetTriggerValueDone()
        {
            return trigger_Value_Done.Value;
        }
    }
}
