using System;
using System.Collections.Generic;
using System.Text;

namespace WeThePeople_ModdingTool
{
    public sealed class HarbourRepository
    {
        private static readonly HarbourRepository instance = new HarbourRepository();

        static HarbourRepository()
        {
        }

        public static HarbourRepository Instance
        {
            get
            {
                return instance;
            }
        }

        private List<String> harbours = new List<string>();

        public List<string> Harbours { get => harbours; set => harbours = value; }

        public void Clear()
        {
            harbours.Clear();
        }
    }
}
