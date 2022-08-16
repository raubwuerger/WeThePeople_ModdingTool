using System;
using System.Collections.Generic;
using System.Text;

namespace WeThePeople_ModdingTool
{
    public sealed class HarbourList
    {
        private static readonly HarbourList instance = new HarbourList();

        static HarbourList()
        {
        }

        public static HarbourList Instance
        {
            get
            {
                return instance;
            }
        }

        public static string EUROPE = "Europe";
        public static string EUROPE_UPPERCASE = "EUROPE";
        public static string AFRICA = "Afric";
        public static string AFRICA_UPPERCASE = "AFRICA";
        public static string PORTROYAL = "PortRoyal";
        public static string PORTROYAL_UPPERCASE = "PORTROYAL";

        private List<String> harbours = new List<string>();

        public List<string> Harbours { get => harbours; set => harbours = value; }

        public void Clear()
        {
            harbours.Clear();
        }
    }
}
