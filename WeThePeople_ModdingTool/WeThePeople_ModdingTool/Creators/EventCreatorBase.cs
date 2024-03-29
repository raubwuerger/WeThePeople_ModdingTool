﻿namespace WeThePeople_ModdingTool.Creators
{
    public abstract class EventCreatorBase
    {
        private string harbour;
        public string Harbour
        {
            get { return harbour; }
            set { harbour = value; }
        }
        private string yieldType;
        public string YieldType
        {
            get { return yieldType; }
            set { yieldType = value; }
        }

        private string savePath;
        public string SavePath
        {
            get { return savePath; }
            set { savePath = value; }
        }

        public abstract bool Create();
    }
}
