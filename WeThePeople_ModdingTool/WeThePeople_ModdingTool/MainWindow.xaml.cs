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
                string messageBoxText = "Initialization failed! See log file!";
                string caption = "Initialization failed!";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Error;
                MessageBoxResult result;

                result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);
            }
            YieldList yields = new YieldList();

            ComboBox_Yield.ItemsSource = yields.Yields;
        }

        private void ComboBox_Yield_Selected(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(ComboBox_Yield.SelectedItem.ToString());
        }
    }
}
