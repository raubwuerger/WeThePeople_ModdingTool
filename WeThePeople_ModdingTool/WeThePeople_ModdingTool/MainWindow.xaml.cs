using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WeThePeople_ModdingTool.FileUtilities;

namespace WeThePeople_ModdingTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            if( false == MainSettingsLoader.Instance.Init() )
            {
                CommonMessageBox.Show_OK_Error("Initialization failed!", "Initialization failed! See log file!");
            }

            ComboBox_Yield.ItemsSource = YieldList.Instance.Yields;
            if( ComboBox_Yield.Items.Count > 0 )
            {
                ComboBox_Yield.SelectedItem = YieldList.Instance.Yields[0];
            }
        }

        private void ComboBox_Yield_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MessageBox.Show(ComboBox_Yield.SelectedItem.ToString());
        }

        private void button_CreateEvents_Click(object sender, RoutedEventArgs e)
        {

        }

        private void button_LoadTemplates_Click(object sender, RoutedEventArgs e)
        {
            if( false == AtLeastOneButtonChecked() )
            {
                CommonMessageBox.Show_OK_Warning("No Harbour seleceted!", "At least one harbour must be selected!");
                return;
            }
            PrepareTemplates();
        }

        private bool AtLeastOneButtonChecked()
        {
            HarbourList.Instance.Clear();

            if (true == checkBox_Europe.IsChecked)
            {
                HarbourList.Instance.Harbours.Add(HarbourList.EUROPE);
            }

            if (true == checkBox_Afrika.IsChecked)
            {
                HarbourList.Instance.Harbours.Add(HarbourList.AFRICA);
            }

            if (true == checkBox_PortRoyal.IsChecked)
            {
                HarbourList.Instance.Harbours.Add(HarbourList.PORTROYAL);
            }

            return HarbourList.Instance.Harbours.Count > 0;
        }

        private void PrepareTemplates()
        {

        }
    }
}
