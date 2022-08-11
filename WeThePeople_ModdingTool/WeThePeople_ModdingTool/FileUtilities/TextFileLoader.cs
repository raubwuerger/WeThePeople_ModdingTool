using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;

namespace WeThePeople_ModdingTool.FileUtilities
{
    public class TextFileLoader
    {
        public string LoadTextFile(string fileName)
        {
            try
            {
                if( false == File.Exists(fileName) )
                {
                    ShowMessageBox(fileName);
                }
                return File.ReadAllText(fileName,Encoding.UTF8);
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message);
                return null;
            }
        }

        private void ShowMessageBox(string fileName)
        {
            CommonMessageBox.Show_OK_Error("Unable to open file!", "The file could not be opened! " + fileName );

        }

        public bool Save( string fileName, string content )
        {
            if (true == File.Exists(fileName))
            {
                ShowMessageBox(fileName);
                return false;
            }
            try
            {
                File.WriteAllText(fileName, content);
                return true;
            }
            catch( Exception ex )
            {
                return false;
            }
        }
    }
}
