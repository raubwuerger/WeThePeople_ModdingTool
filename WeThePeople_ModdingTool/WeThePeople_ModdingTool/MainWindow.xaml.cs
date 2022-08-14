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
using System.Xml;
using Serilog;

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
            InitLoggingFramework();
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
        private void InitLoggingFramework()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File(GenerateLogFileName(), rollingInterval: RollingInterval.Day)
                .CreateLogger();
            CreateInitialLogMessage();
        }

        private string GenerateLogFileName()
        {
            DateTime dateTime = DateTime.UtcNow.Date;
            string logFileName = "logs/";
            logFileName += dateTime.ToString("yyyy-MM-dd");
            logFileName += "_";
            logFileName += System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
            logFileName += ".log";
            return logFileName;
        }

        private void CreateInitialLogMessage()
        {
            string initialLogMessage = ">>>>>>>>>> Started logging application: ";
            initialLogMessage += System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
            initialLogMessage += ":";
            initialLogMessage += System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            initialLogMessage += " <<<<<<<<<<";
            Log.Information(initialLogMessage);
        }


        private void ComboBox_Yield_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void button_CreateEvents_Click(object sender, RoutedEventArgs e)
        {
            CreateEvents();
        }

        private void button_LoadTemplates_Click(object sender, RoutedEventArgs e)
        {
            if( false == AtLeastOneButtonChecked() )
            {
                CommonMessageBox.Show_OK_Warning("No Harbour seleceted!", "At least one harbour must be selected!");
                return;
            }

            string CvRandomEventInterface_Start_Processed = ProcessTemplate(MainSettingsLoader.Instance.CvRandomEventInterface_Start_Template);
            textBox_PythonStart.Text = CvRandomEventInterface_Start_Processed;

            string CvRandomEventInterface_Done_Processed = ProcessTemplate(MainSettingsLoader.Instance.CvRandomEventInterface_Done_Template);
            textBox_PythonDone.Text = CvRandomEventInterface_Done_Processed;

            XmlDocument CIV4EventInfos_Start_Template_Processed = ProcessTemplate(MainSettingsLoader.Instance.CIV4EventInfos_Start_Template);
            textBox_EventInfoStart.Text = CreateText(CIV4EventInfos_Start_Template_Processed);

            XmlDocument CIV4EventInfos_Done_Template_Processed = ProcessTemplate(MainSettingsLoader.Instance.CIV4EventInfos_Done_Template);
            textBox_EventInfoDone.Text = CreateText(CIV4EventInfos_Done_Template_Processed);

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

        private void CreateEvents()
        {
            if( false == SaveFile(CreatePathFileYield(MainSettingsLoader.Instance.CvRandomEventInterface_Start_Concrete, ".py"), textBox_PythonStart.Text))
            {
                CommonMessageBox.Show_OK_Warning("Failed saving file!", "Unable to save file! " +"");
            }

            if (false == SaveFile(CreatePathFileYield(MainSettingsLoader.Instance.CvRandomEventInterface_Done_Concrete, ".py"), textBox_PythonDone.Text))
            {
                CommonMessageBox.Show_OK_Warning("Failed saving file!", "Unable to save file! " + "");
            }

            XmlDocument CIV4EventInfos_Start_Template_Processed = LoadXMLFromTextbox(textBox_EventInfoStart);
            if (false == SaveFile(CreatePathFileYield(MainSettingsLoader.Instance.CIV4EventInfos_Start_Concrete, ".xml"), CIV4EventInfos_Start_Template_Processed))
            {
                CommonMessageBox.Show_OK_Warning("Failed saving file!", "Unable to save file! " + "");
            }

            XmlDocument CIV4EventInfos_Done_Template_Processed = ProcessTemplate(MainSettingsLoader.Instance.CIV4EventInfos_Done_Template);
            if (false == SaveFile(CreatePathFileYield(MainSettingsLoader.Instance.CIV4EventInfos_Done_Concrete, ".xml"), CIV4EventInfos_Done_Template_Processed))
            {
                CommonMessageBox.Show_OK_Warning("Failed saving file!", "Unable to save file! " + "");
            }
            textBox_EventInfoDone.Text = CreateText(CIV4EventInfos_Done_Template_Processed);
        }

        private XmlDocument LoadXMLFromTextbox( TextBox textBox )
        {
            XmlDocument xmlDocument = new XmlDocument();
            try
            {
                xmlDocument.LoadXml(textBox.Text);
                return xmlDocument;
            }
            catch(Exception ex)
            {
                Log.Error(CommonVariables.MESSAGE_BOX_EXCEPTION);
                return xmlDocument;
            }
        }

        private string CreatePathFileYield( string baseFile, string fileExtension )
        {
            string absoluteProgramPath = AppDomain.CurrentDomain.BaseDirectory;
            string relativeAssetPath = System.IO.Path.Combine(absoluteProgramPath, MainSettingsLoader.Instance.assetPathRelative);
            
            string appendix = baseFile;
            appendix += ComboBox_Yield.SelectedItem.ToString();
            appendix += fileExtension;

            return System.IO.Path.Combine(relativeAssetPath, appendix);
        }

        private string ProcessTemplate( string template )
        {
            PythonItemReplacer replacer = new PythonItemReplacer();
            replacer.ReplaceItems.Add(ReplaceItems.HARBOUR_NORMAL, HarbourList.Instance.Harbours[0]);
            replacer.ReplaceItems.Add(ReplaceItems.HARBOUR_UPPERCASE, HarbourList.Instance.Harbours[0].ToUpper());
            replacer.ReplaceItems.Add(ReplaceItems.YIELD, ComboBox_Yield.SelectedItem.ToString());

            if (false == replacer.Replace(template))
            {
                return "";
            }

            return replacer.ReplacedString;
        }

        private bool SaveFile(string fileName, string pythonFile )
        {
            TextFileUtility textFileUtility = new TextFileUtility();
            return textFileUtility.Save(fileName,pythonFile);
        }

        private XmlDocument ProcessTemplate(XmlDocument template)
        {
            XMLItemReplacer replacer = new XMLItemReplacer();
            replacer.ReplaceItems.Add(ReplaceItems.HARBOUR_NORMAL, HarbourList.Instance.Harbours[0]);
            replacer.ReplaceItems.Add(ReplaceItems.HARBOUR_UPPERCASE, HarbourList.Instance.Harbours[0].ToUpper());
            replacer.ReplaceItems.Add(ReplaceItems.YIELD, ComboBox_Yield.SelectedItem.ToString());
            replacer.ReplaceItems.Add(ReplaceItems.TRIGGER_START_VALUE, "100");
            replacer.ReplaceItems.Add(ReplaceItems.TRIGGER_DONE_VALUE, "1000");

            if (false == replacer.Replace(template))
            {
                return null;
            }

            return replacer.ReplacedContent;
        }

        private bool SaveFile(string fileName, XmlDocument xmlDocument)
        {
            XMLFileUtility xmlFileUtility = new XMLFileUtility();
            return xmlFileUtility.Save(fileName, xmlDocument);
        }

        private string CreateText(XmlDocument xmlDocument)
        {
            XmlNodeList nodes = xmlDocument.DocumentElement.SelectNodes("/EventInfo");

            string formatedXML = String.Empty;
            foreach (XmlNode node in nodes)
            {
                formatedXML += FormatXml(node);
            }
            return formatedXML;
        }
        private string FormatXml(XmlNode xmlNode)
        {
            StringBuilder bob = new StringBuilder();

            // We will use stringWriter to push the formated xml into our StringBuilder bob.
            using (StringWriter stringWriter = new StringWriter(bob))
            {
                // We will use the Formatting of our xmlTextWriter to provide our indentation.
                using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
                {
                    xmlTextWriter.Formatting = Formatting.Indented;
                    xmlNode.WriteTo(xmlTextWriter);
                }
            }

            return bob.ToString();
        }

        private void checkBox_PythonStart_Editable_Checked(object sender, RoutedEventArgs e)
        {
            textBox_PythonStart.IsEnabled = true;
        }

        private void checkBox_PythonStart_Editable_Unchecked(object sender, RoutedEventArgs e)
        {
            textBox_PythonStart.IsEnabled = false;
        }

        private void checkBox_PythonDone_Editable_Checked(object sender, RoutedEventArgs e)
        {
            textBox_PythonDone.IsEnabled = true;
        }

        private void checkBox_PythonDone_Editable_Unchecked(object sender, RoutedEventArgs e)
        {
            textBox_PythonDone.IsEnabled = false;
        }

        private void checkBox_EventInfoStart_Editable_Checked(object sender, RoutedEventArgs e)
        {
            textBox_EventInfoStart.IsEnabled = true;
        }

        private void checkBox_EventInfoStart_Editable_Unchecked(object sender, RoutedEventArgs e)
        {
            textBox_EventInfoStart.IsEnabled = false;
        }

        private void checkBox_EventInfoDone_Editable_Checked(object sender, RoutedEventArgs e)
        {
            textBox_EventInfoDone.IsEnabled = true;
        }

        private void checkBox_EventInfoDone_Editable_Unchecked(object sender, RoutedEventArgs e)
        {
            textBox_EventInfoDone.IsEnabled = false;
        }

        private void button_EventInfoStart_validate_Click(object sender, RoutedEventArgs e)
        {
            XmlDocument xmlValid = LoadXMLFromTextbox(textBox_EventInfoStart);
        }

    }
}
