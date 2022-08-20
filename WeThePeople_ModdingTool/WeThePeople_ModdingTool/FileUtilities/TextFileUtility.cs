using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using Serilog;
using WeThePeople_ModdingTool.DataSets;

namespace WeThePeople_ModdingTool.FileUtilities
{
    public class TextFileUtility
    {
        public static string Load(string fileName)
        {
            try
            {
                if( false == File.Exists(fileName) )
                {
                    Log.Warning(CommonVariables.MESSAGE_BOX_FILE_DOESNT_EXIST + CommonVariables.BLANK_MINUS_BLANK + fileName);
                }
                return File.ReadAllText(fileName,Encoding.UTF8);
            }
            catch (Exception ex)
            {
                Log.Error(CommonVariables.MESSAGE_BOX_EXCEPTION + CommonVariables.COLON_BLANK + ex.Message);
                CommonMessageBox.Show_OK_Error(CommonVariables.MESSAGE_BOX_UNABLE_OPEN_CAPTION, CommonVariables.MESSAGE_BOX_EXCEPTION_CR + ex.Message);
                return null;
            }
        }

        public static bool Save( string fileName, string content )
        {
            if (true == File.Exists(fileName))
            {
                if( MessageBoxResult.No == CommonMessageBox.Show_YesNo(CommonVariables.MESSAGE_BOX_UNABLE_SAVE_CAPTION, CommonVariables.MESSAGE_BOX_OVERWRITE_CR + fileName) )
                {
                    return false;
                }
            }
            try
            {
                File.WriteAllText(fileName, content);
                return true;
            }
            catch( Exception ex )
            {
                Log.Error(CommonVariables.MESSAGE_BOX_EXCEPTION + CommonVariables.BLANK_MINUS_BLANK + ex.Message);
                CommonMessageBox.Show_OK_Error(CommonVariables.MESSAGE_BOX_UNABLE_SAVE_CAPTION, CommonVariables.MESSAGE_BOX_EXCEPTION_CR + ex.Message);
                return false;
            }
        }

        public static bool SaveCreatePath( string fileName, string content )
        {
            string path = Path.GetDirectoryName(fileName);
            Directory.CreateDirectory(Path.GetDirectoryName(fileName));
            return Save(fileName, content);
        }

        public static String LoadFileText(DataSetBase dataSetBase)
        {
            return Load(dataSetBase.TemplateFileNameAndPathAbsolute);
        }
    }
}
