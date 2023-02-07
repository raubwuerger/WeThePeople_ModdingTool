using System.Collections.Generic;
using WeThePeople_ModdingTool.Helper;

namespace WeThePeople_ModdingTool.Factories
{
    public class ListComparatorFactory
    {
        public ListHelperXML CreateListComparatorGameEventText()
        {
            ListHelperXML listComparator = new ListHelperXML();
            List<string> ignoreItems = new List<string>();
            ignoreItems.Add("TXT_KEY_EVENT_EUROPE_TRADE_QUEST_START");
            ignoreItems.Add("TXT_KEY_EVENT_AFRICA_TRADE_QUEST_START");
            ignoreItems.Add("TXT_KEY_EVENT_PORTROYAL_TRADE_QUEST_START");

            ignoreItems.Add("TXT_KEY_EVENT_EUROPE_TRADE_QUEST_DONE_1");
            ignoreItems.Add("TXT_KEY_EVENT_EUROPE_TRADE_QUEST_DONE_2");
            ignoreItems.Add("TXT_KEY_EVENT_EUROPE_TRADE_QUEST_DONE_3");
            ignoreItems.Add("TXT_KEY_EVENT_EUROPE_TRADE_QUEST_DONE_4");
            ignoreItems.Add("TXT_KEY_EVENT_EUROPE_TRADE_QUEST_DONE_5");
            ignoreItems.Add("TXT_KEY_EVENT_EUROPE_TRADE_QUEST_DONE_6");

            ignoreItems.Add("TXT_KEY_EVENT_AFRICA_TRADE_QUEST_DONE_1");
            ignoreItems.Add("TXT_KEY_EVENT_AFRICA_TRADE_QUEST_DONE_2");
            ignoreItems.Add("TXT_KEY_EVENT_AFRICA_TRADE_QUEST_DONE_3");
            ignoreItems.Add("TXT_KEY_EVENT_AFRICA_TRADE_QUEST_DONE_4");
            ignoreItems.Add("TXT_KEY_EVENT_AFRICA_TRADE_QUEST_DONE_5");
            ignoreItems.Add("TXT_KEY_EVENT_AFRICA_TRADE_QUEST_DONE_6");

            ignoreItems.Add("TXT_KEY_EVENT_PORTROYAL_TRADE_QUEST_DONE_1");
            ignoreItems.Add("TXT_KEY_EVENT_PORTROYAL_TRADE_QUEST_DONE_2");
            ignoreItems.Add("TXT_KEY_EVENT_PORTROYAL_TRADE_QUEST_DONE_3");
            ignoreItems.Add("TXT_KEY_EVENT_PORTROYAL_TRADE_QUEST_DONE_4");
            ignoreItems.Add("TXT_KEY_EVENT_PORTROYAL_TRADE_QUEST_DONE_5");
            ignoreItems.Add("TXT_KEY_EVENT_PORTROYAL_TRADE_QUEST_DONE_6");

            listComparator.IgnoreList = ignoreItems;
            return listComparator;
        }
    }
}
