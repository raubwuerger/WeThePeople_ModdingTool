using System;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using WeThePeople_ModdingTool.DataSets;
using WeThePeople_ModdingTool.FileUtilities;
using WeThePeople_ModdingTool.Helper;
using WeThePeople_ModdingTool.Validators;

namespace WeThePeople_ModdingTool.Windows
{
    /// <summary>
    /// Interaction logic for EventInfoStartWindow.xaml
    /// </summary>
    public partial class EventInfoStartWindow : Window
    {
        private DataSetEventInfoStart dataSetEventInfoStart = new DataSetEventInfoStart();
        public DataSetEventInfoStart DataSetEventInfoStart
        {
            get {
                GetFromGUI();
                return dataSetEventInfoStart; 
                }
            set 
                {
                SetDataToGUI();
                dataSetEventInfoStart = value; 
                }
        }
        public EventInfoStartWindow()
        {
            InitializeComponent();
            SetDataToGUI();
        }

        private void EventInfoStart_Ok_Click(object sender, RoutedEventArgs e)
        {
            if( false == IsAtLeastOneInputValid() )
            {
                if (MessageBoxResult.No == CommonMessageBox.Show_YesNo("Input not valid!", "No changes made!\n\rDo you want to proceed?"))
                {
                    return;
                }
            }
            else 
            {
                if( false == IsRelationStartDoneValueValid() )
                {
                    CommonMessageBox.Show_OK_Error("Input not valid!","StartValue is equal or greater DoneValue!");
                    return;
                }
            }
            DialogResult = true;
        }

        private void EventInfoStart_Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void SetDataToGUI()
        {
            StartValue_TextBox.Text = dataSetEventInfoStart.GetTriggerValueStart();
            DoneValue_TextBox.Text = dataSetEventInfoStart.GetTriggerValueDone();
        }

        private void GetFromGUI()
        {
            dataSetEventInfoStart.SetTriggerValueStart(StartValue_TextBox.Text);
            dataSetEventInfoStart.SetTriggerValueDone(DoneValue_TextBox.Text);
        }

        private bool IsAtLeastOneInputValid()
        {
            if( true == IsStartValueValid() )
            {
                return true;
            }

            if( true == IsDoneValueValid() )
            {
                return true;
            }

            return false;
        }

        private bool IsRelationStartDoneValueValid()
        {
            int startValue;
            if( false == StringHelper.StringToInteger(StartValue_TextBox.Text, out startValue) )
            {
                return false;
            }

            int doneValue;
            if (false == StringHelper.StringToInteger(DoneValue_TextBox.Text, out doneValue))
            {
                return false;
            }

            return doneValue > startValue;
        }

        private bool IsStartValueValid()
        {
            return StringHelper.IsNumberGreaterZero(StartValue_TextBox.Text);
        }
        private bool IsDoneValueValid()
        {
            return StringHelper.IsNumberGreaterZero(DoneValue_TextBox.Text);
        }

        private void button_StartValue_Clear_Click(object sender, RoutedEventArgs e)
        {
            StartValue_TextBox.Text = "100";
        }

        private void button_DoneValue_Clear_Click(object sender, RoutedEventArgs e)
        {
            DoneValue_TextBox.Text = "1000";
        }
    }
}
