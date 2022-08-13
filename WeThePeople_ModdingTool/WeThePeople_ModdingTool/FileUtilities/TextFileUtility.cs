using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;

namespace WeThePeople_ModdingTool.FileUtilities
{
    public class TextFileUtility
    {
        public string Load(string fileName)
        {
            try
            {
                if( false == File.Exists(fileName) )
                {
                    CommonMessageBox.Show_OK_Error(CommonVariables.MESSAGE_BOX_UNABLE_OPEN_CAPTION, CommonVariables.MESSAGE_BOX_FILE_DOESNT_EXIST_CR + fileName);
                }
                return File.ReadAllText(fileName,Encoding.UTF8);
            }
            catch (Exception ex)
            {
                CommonMessageBox.Show_OK_Error(CommonVariables.MESSAGE_BOX_UNABLE_OPEN_CAPTION, CommonVariables.MESSAGE_BOX_EXCEPTION_CR + fileName + CommonVariables.CR + ex.Message);
                return null;
            }
        }

        public bool Save( string fileName, string content )
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
                CommonMessageBox.Show_OK_Error(CommonVariables.MESSAGE_BOX_UNABLE_SAVE_CAPTION, CommonVariables.MESSAGE_BOX_EXCEPTION_CR + fileName + CommonVariables.CR + ex.Message);
                return false;
            }
        }
    }
}
