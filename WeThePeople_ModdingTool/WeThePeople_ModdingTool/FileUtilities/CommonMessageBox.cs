using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace WeThePeople_ModdingTool.FileUtilities
{
    public class CommonMessageBox
    {
        public static bool ShowMessageBoxesNotForUnitTests = true;
        static public MessageBoxResult Show( string caption, string messageBoxText, MessageBoxButton button, MessageBoxImage icon )
        {
            if( false == ShowMessageBoxesNotForUnitTests )
            {
                return MessageBoxResult.None;
            }
            return MessageBox.Show(messageBoxText, caption, button, icon );
        }

        static public MessageBoxResult Show_OK_Error(string caption, string messageBoxText)
        {
            if (false == ShowMessageBoxesNotForUnitTests)
            {
                return MessageBoxResult.None;
            }
            return MessageBox.Show(messageBoxText, caption, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        static public MessageBoxResult Show_OK_Warning(string caption, string messageBoxText)
        {
            if (false == ShowMessageBoxesNotForUnitTests)
            {
                return MessageBoxResult.None;
            }
            return MessageBox.Show(messageBoxText, caption, MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        static public MessageBoxResult Show_YesNo(string caption, string messageBoxText)
        {
            if (false == ShowMessageBoxesNotForUnitTests)
            {
                return MessageBoxResult.None;
            }
            return MessageBox.Show(messageBoxText, caption, MessageBoxButton.YesNo, MessageBoxImage.Question);
        }

        static public MessageBoxResult Show_Info(string caption, string messageBoxText)
        {
            if (false == ShowMessageBoxesNotForUnitTests)
            {
                return MessageBoxResult.None;
            }
            return MessageBox.Show(messageBoxText, caption, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        static public MessageBoxResult Show_Question_YesNoCancel(string caption, string messageBoxText)
        {
            if (false == ShowMessageBoxesNotForUnitTests)
            {
                return MessageBoxResult.None;
            }
            return MessageBox.Show(messageBoxText, caption, MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
        }

    }
}
