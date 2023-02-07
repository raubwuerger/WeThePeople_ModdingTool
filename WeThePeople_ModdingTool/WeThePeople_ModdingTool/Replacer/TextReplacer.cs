using Serilog;
using System;
using System.Collections.Generic;
using System.Text;
using WeThePeople_ModdingTool.FileUtilities;
using WeThePeople_ModdingTool.Validators;

namespace WeThePeople_ModdingTool
{
    public class TextReplacer
    {
        public static string Replace(string content, KeyValuePair<string, string> replaceItem)
        {
            if (true == StringValidator.IsNull(replaceItem.Value))
            {
                Log.Debug("Replace item value is invalid! " + replaceItem.Key);
                return content;
            }

            Log.Debug("Content to replace : " + content + " - key: " + replaceItem.Key + " - value: " + replaceItem.Value);
            StringBuilder builder = new StringBuilder(content);
            try
            {
                builder.Replace(replaceItem.Key, replaceItem.Value);
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
        public static string Replace(string content, IDictionary<string, string> replaceItems)
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
