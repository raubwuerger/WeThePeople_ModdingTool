using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace WeThePeople_ModdingTool.FileUtilities
{
    public class CommonMessageBox
    {
        public static bool ShowMessageBoxes = true;
        static public MessageBoxResult Show( string caption, string messageBoxText, MessageBoxButton button, MessageBoxImage icon )
        {
            if( false == ShowMessageBoxes )
            {
                return MessageBoxResult.None;
            }
            return MessageBox.Show(messageBoxText, caption, button, icon );
        }

        static public MessageBoxResult Show_OK_Error(string caption, string messageBoxText)
        {
            if (false == ShowMessageBoxes)
            {
                return MessageBoxResult.None;
            }
            return MessageBox.Show(messageBoxText, caption, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        static public MessageBoxResult Show_OK_Warning(string caption, string messageBoxText)
        {
            if (false == ShowMessageBoxes)
            {
                return MessageBoxResult.None;
            }
            return MessageBox.Show(messageBoxText, caption, MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        static public MessageBoxResult Show_YesNo(string caption, string messageBoxText)
        {
            if (false == ShowMessageBoxes)
            {
                return MessageBoxResult.None;
            }
            return MessageBox.Show(messageBoxText, caption, MessageBoxButton.YesNo, MessageBoxImage.Question);
        }
    }
}
