using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using WeThePeople_ModdingTool.DataSets;
using WeThePeople_ModdingTool.FileUtilities;
using WeThePeople_ModdingTool.Helper;

namespace WeThePeople_ModdingTool.Windows
{
    /// <summary>
    /// Interaction logic for EventInfoStartWindow.xaml
    /// </summary>
    public partial class EventInfoStartWindow : Window
    {
        private string yield;
        public string Yield
        {
            set { yield = value; }
        }

        private string harbour;
        public string Harbour
        {
            set { harbour = value; }
        }
        private DataSetEventInfoStart dataSetEventInfoStart = new DataSetEventInfoStart();
        public DataSetEventInfoStart DataSetEventInfoStart
        {
            get
            {
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
        }
        public void Init()
        {
            SetDataToGUI();
        }

        private void button_EventInfoStart_Ok_Click(object sender, RoutedEventArgs e)
        {
            if (false == IsAtLeastOneInputValid())
            {
                if (MessageBoxResult.No == CommonMessageBox.Show_YesNo("Input not valid!", "No changes made!\r\nDo you want to proceed?"))
                {
                    return;
                }
            }
            else
            {
                if (false == IsRelationStartDoneValueValid())
                {
                    CommonMessageBox.Show_OK_Error("Input not valid!", "StartValue is equal or greater DoneValue!");
                    return;
                }
            }
            DialogResult = true;
        }

        private void button_EventInfoStart_Cancel_Click(object sender, RoutedEventArgs e)
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
            label_EventInfoStart.Content = "Create EventInfoStart: " + yield + " - " + harbour;
            textBox_StartValue.Text = dataSetEventInfoStart.GetTriggerValueStart();
            textBox_DoneValue.Text = dataSetEventInfoStart.GetTriggerValueDone();
        }

        private void GetFromGUI()
        {
            dataSetEventInfoStart.SetTriggerValueStart(textBox_StartValue.Text);
            dataSetEventInfoStart.SetTriggerValueDone(textBox_DoneValue.Text);
        }

        private bool IsAtLeastOneInputValid()
        {
            if (true == IsStartValueValid())
            {
                return true;
            }

            if (true == IsDoneValueValid())
            {
                return true;
            }

            return false;
        }

        private bool IsRelationStartDoneValueValid()
        {
            int startValue;
            if (false == StringHelper.StringToInteger(textBox_StartValue.Text, out startValue))
            {
                return false;
            }

            int doneValue;
            if (false == StringHelper.StringToInteger(textBox_DoneValue.Text, out doneValue))
            {
                return false;
            }

            return doneValue > startValue;
        }

        private bool IsStartValueValid()
        {
            return StringHelper.IsNumberGreaterZero(textBox_StartValue.Text);
        }
        private bool IsDoneValueValid()
        {
            return StringHelper.IsNumberGreaterZero(textBox_DoneValue.Text);
        }

        private void button_StartValue_Clear_Click(object sender, RoutedEventArgs e)
        {
            textBox_StartValue.Text = "100";
        }

        private void button_DoneValue_Clear_Click(object sender, RoutedEventArgs e)
        {
            textBox_DoneValue.Text = "1000";
        }
    }
}
