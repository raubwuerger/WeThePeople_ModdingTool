using System;
using System.Collections.Generic;
using System.Text;

namespace WeThePeople_ModdingTool.Creators
{
    interface IEventCreator
    {
        public string Harbour
        {
            set { }
        }
        public string YieldType
        {
            set { }
        }

        public string SavePath
        {
            set { }
        }

        public bool Create();
    }
}
