using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace WeThePeople_ModdingTool
{
    public class XMLItemReplacer
    {
        private IDictionary<string, string> replaceItems = new Dictionary<string, string>();
        public IDictionary<string, string> ReplaceItems
        {
            get { return replaceItems; }
            set { replaceItems = value; }
        }
        public bool ReplaceItem( XmlDocument xmlDocument )
        {
            if( null == xmlDocument )
            {
                return false;
            }

            if( true == ReplaceItems.Count <= 0 )
            {
                return false;
            }

            return false;
        }
    }
}
