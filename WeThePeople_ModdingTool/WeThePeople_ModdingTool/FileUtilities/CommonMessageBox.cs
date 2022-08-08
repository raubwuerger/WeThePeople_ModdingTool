using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace WeThePeople_ModdingTool.FileUtilities
{
    public class CommonMessageBox
    {
        static public MessageBoxResult Show( string caption, string messageBoxText, MessageBoxButton button, MessageBoxImage icon )
        {
            return MessageBox.Show(messageBoxText, caption, button, icon );
        }

        static public MessageBoxResult Show_OK_Error(string caption, string messageBoxText)
        {
            return MessageBox.Show(messageBoxText, caption, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        static public MessageBoxResult Show_OK_Warning(string caption, string messageBoxText)
        {
            return MessageBox.Show(messageBoxText, caption, MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}
