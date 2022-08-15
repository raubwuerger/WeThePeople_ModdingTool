using System;
using System.Collections.Generic;
using System.Text;
using WeThePeople_ModdingTool.FileUtilities;
using Serilog;

namespace WeThePeople_ModdingTool
{
    public class TextReplacer
    {
        public static string replace( string content, KeyValuePair<string, string> replaceItem )
        {
            Log.Debug("Content to replace : " + content);
            StringBuilder builder = new StringBuilder(content);
            try
            {
                builder.Replace(replaceItem.Key, replaceItem.Value);
                string contentReplaced = builder.ToString();
                Log.Debug("Content replaced : " + contentReplaced);
                return contentReplaced;
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
                Log.Debug("Content to replace : " + content);
                foreach (KeyValuePair<string, string> entry in replaceItems)
                {
                    builder.Replace(entry.Key, entry.Value);
                }
                string contentReplaced = builder.ToString();
                Log.Debug("Content replaced : " + contentReplaced);
                return contentReplaced;
            }
            catch (Exception ex)
            {
                CommonMessageBox.Show_OK_Error(CommonVariables.MESSAGE_BOX_EXCEPTION, "Replacing text failed!" + CommonVariables.CR + ex.Message);
                return null;
            }
        }
    }
}
