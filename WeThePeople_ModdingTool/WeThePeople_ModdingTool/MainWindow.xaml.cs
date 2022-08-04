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
            IO.XMLFileParser parser = new IO.XMLFileParser();
            String currentDir = Directory.GetCurrentDirectory();
            currentDir += "..\\..\\..\\templates\\Assets\\XML\\Events\\CIV4EventInfos_template.xml";
            Console.WriteLine(Directory.GetCurrentDirectory());
            parser.LoadFile("D:\\C#\\WeThePeople_ModdingTool\\WeThePeople_ModdingTool\\WeThePeople_ModdingTool\\templates\\Assets\\XML\\Events\\CIV4EventInfos_template.xml");
        }
    }
}
