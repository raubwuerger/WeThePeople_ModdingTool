using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WeThePeople_ModdingTool.Windows
{
    /// <summary>
    /// Interaction logic for EventInfoStartWindow.xaml
    /// </summary>
    public partial class EventInfoStartWindow : Window
    {
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
