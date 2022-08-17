using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using WeThePeople_ModdingTool.DataSets;

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
                    dataSetEventInfoStart.SetTriggerValueStart(StartValue_TextBox.Text);
                    dataSetEventInfoStart.SetTriggerValueDone(DoneValue_TextBox.Text);
                    return dataSetEventInfoStart; 
                }
            set 
                {
                    StartValue_TextBox.Text = dataSetEventInfoStart.GetTriggerValueStart();
                    DoneValue_TextBox.Text = dataSetEventInfoStart.GetTriggerValueDone();
                    dataSetEventInfoStart = value; 
                }
        }
        public EventInfoStartWindow()
        {
            InitializeComponent();
        }

        private void EventInfoStart_Ok_Click(object sender, RoutedEventArgs e)
        {
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
    }
}
