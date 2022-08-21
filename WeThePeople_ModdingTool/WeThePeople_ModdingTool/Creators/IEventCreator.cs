using System;
using System.Collections.Generic;
using System.Text;

namespace WeThePeople_ModdingTool.Creators
{
    public interface IEventCreator
    {
        public string Harbour
        {
            get;
            set;
        }
        public string YieldType
        {
            get;
            set;
        }

        public string SavePath
        {
            get;
            set;
        }

        public bool Create();
    }
}
