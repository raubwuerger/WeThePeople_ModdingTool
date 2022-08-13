using System;
using System.Collections.Generic;
using System.Text;
using WeThePeople_ModdingTool.FileUtilities;

namespace WeThePeople_ModdingTool
{
    public class TextReplacer
    {
        public static string replace( string content, KeyValuePair<string, string> replaceItem )
        {
            StringBuilder builder = new StringBuilder(content);
            try
            {
                builder.Replace(replaceItem.Key, replaceItem.Value);
                return builder.ToString();
            }
            catch( Exception ex )
            {
                CommonMessageBox.Show_OK_Error(CommonVariables.MESSAGE_BOX_EXCEPTION, "Replacing text failed!" + CommonVariables.CR + ex.Message);
                return null;
            }
        }
        public static string replace(string content, IDictionary<string, string> replaceItems )
        {
            StringBuilder builder = new StringBuilder(content);
            try
            {
                foreach (KeyValuePair<string, string> entry in replaceItems)
                {
                    builder.Replace(entry.Key, entry.Value);
                }
                return builder.ToString();
            }
            catch (Exception ex)
            {
                CommonMessageBox.Show_OK_Error(CommonVariables.MESSAGE_BOX_EXCEPTION, "Replacing text failed!" + CommonVariables.CR + ex.Message);
                return null;
            }
        }
    }
}
