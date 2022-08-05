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
                return null;
            }
        }

        private void ShowMessageBox(string fileName)
        {
            string messageBoxText = "The file could not be opened! " +fileName;
            string caption = "Unable to open file!";
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Error;
            MessageBoxResult result;

            MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);
        }
    }
}
