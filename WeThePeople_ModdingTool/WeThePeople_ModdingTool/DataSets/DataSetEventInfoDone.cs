using System;
using System.Collections.Generic;
using System.Text;

namespace WeThePeople_ModdingTool.DataSets
{
    public class DataSetEventInfoDone
    {
        private KeyValuePair<string, string> gold = new KeyValuePair<string, string>(ReplaceItems.GOLD,"1000");
        public KeyValuePair<string, string> Gold
        {
            get { return gold; }
        }
        public void SetGold( string value )
        {
            gold = new KeyValuePair<string, string>(ReplaceItems.GOLD, value);
        }
        public string GetGold()
        {
            return gold.Value;
        }

        private KeyValuePair<string, string> unitClass = new KeyValuePair<string, string>(ReplaceItems.UNIT_CLASS, "");
        public KeyValuePair<string, string> UnitClass
        {
            get { return unitClass; }
        }
        public void SetUnitClass(string value)
        {
            unitClass = new KeyValuePair<string, string>(ReplaceItems.UNIT_CLASS, value);
        }
        public string GetUnitClass()
        {
            return unitClass.Value;
        }

        private KeyValuePair<string, string> unitCount = new KeyValuePair<string, string>(ReplaceItems.UNIT_COUNT, "0");
        public KeyValuePair<string, string> UnitCount
        {
            get { return unitCount; }
        }
        public void SetUnitCount(string value)
        {
            unitCount = new KeyValuePair<string, string>(ReplaceItems.UNIT_COUNT, value);
        }
        public string GetUnitCount()
        {
            return unitCount.Value;
        }

        private KeyValuePair<string, string> unitExperience = new KeyValuePair<string, string>(ReplaceItems.UNIT_EXPERIENCE, "0");
        public KeyValuePair<string, string> UnitExperience
        {
            get { return unitExperience; }
        }
        public void SetUnitExperience(string value)
        {
            unitExperience = new KeyValuePair<string, string>(ReplaceItems.UNIT_EXPERIENCE, value);
        }
        public string GetUnitExperience()
        {
            return unitExperience.Value;
        }

        private KeyValuePair<string, string> kingRelation = new KeyValuePair<string, string>(ReplaceItems.KING_RELATION, "0");
        public KeyValuePair<string, string> KingRelation
        {
            get { return kingRelation; }
        }
        public void SetKingRelation(string value)
        {
            kingRelation = new KeyValuePair<string, string>(ReplaceItems.KING_RELATION, value);
        }
        public string GetKingRelation()
        {
            return kingRelation.Value;
        }

        private KeyValuePair<string, string> yieldPrice = new KeyValuePair<string, string>(ReplaceItems.YIELD_PRICE, "0");
        public KeyValuePair<string, string> YieldPrice
        {
            get { return yieldPrice; }
        }
        public void SetYieldPrice(string value)
        {
            yieldPrice = new KeyValuePair<string, string>(ReplaceItems.YIELD_PRICE, value);
        }
        public string GetYieldPrice()
        {
            return yieldPrice.Value;
        }
    }
}
