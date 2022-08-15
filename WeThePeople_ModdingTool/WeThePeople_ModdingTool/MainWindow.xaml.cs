using System;
using System.Windows;
using System.Windows.Controls;
using WeThePeople_ModdingTool.FileUtilities;
using System.Xml;

namespace WeThePeople_ModdingTool
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LogFrameworkInitialzer.Init();
            CommandLineArgsParser.Parse();
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

            XmlDocument CIV4EventInfos_Start_Template_Processed = ProcessTemplate(MainSettingsLoader.Instance.CIV4EventInfos_Start_Template, "/EventInfo");
            textBox_EventInfoStart.Text = XMLHelper.FormatKeepIndention(CIV4EventInfos_Start_Template_Processed.DocumentElement.SelectNodes("/EventInfo"));

            XmlDocument CIV4EventInfos_Done_Template_Processed = ProcessTemplate(MainSettingsLoader.Instance.CIV4EventInfos_Done_Template, "/EventInfo");
            textBox_EventInfoDone.Text = XMLHelper.FormatKeepIndention(CIV4EventInfos_Done_Template_Processed.DocumentElement.SelectNodes("/EventInfo"));

            XmlDocument CIV4GameText_Colonization_Events_utf8_Processed = ProcessTemplate(MainSettingsLoader.Instance.CIV4GameText_Colonization_Events_utf8_Template, "/Civ4GameText");
            textBox_CIV4GameText.Text = XMLHelper.FormatKeepIndention(CIV4GameText_Colonization_Events_utf8_Processed.DocumentElement.SelectNodes("/Civ4GameText"));
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

            XmlDocument CIV4EventInfos_Start_Template_Processed = XMLHelper.CreateFromString(textBox_EventInfoStart.Text);
            if (false == SaveFile(CreatePathFileYield(MainSettingsLoader.Instance.CIV4EventInfos_Start_Concrete, ".xml"), CIV4EventInfos_Start_Template_Processed))
            {
                CommonMessageBox.Show_OK_Warning("Failed saving file!", "Unable to save file! " + "");
            }

            XmlDocument CIV4EventInfos_Done_Template_Processed = XMLHelper.CreateFromString(textBox_EventInfoDone.Text);
            if (false == SaveFile(CreatePathFileYield(MainSettingsLoader.Instance.CIV4EventInfos_Done_Concrete, ".xml"), CIV4EventInfos_Done_Template_Processed))
            {
                CommonMessageBox.Show_OK_Warning("Failed saving file!", "Unable to save file! " + "");
            }

            XmlDocument CIV4GameText_Colonization_Events_utf8_Processed = XMLHelper.CreateFromString(textBox_CIV4GameText.Text);
            if (false == SaveFile(CreatePathFileYield(MainSettingsLoader.Instance.CIV4GameText_Colonization_Events_utf8_Concrete, ".xml"), CIV4GameText_Colonization_Events_utf8_Processed))
            {
                CommonMessageBox.Show_OK_Warning("Failed saving file!", "Unable to save file! " + "");
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
                return String.Empty;
            }

            return replacer.ReplacedString;
        }

        private bool SaveFile(string fileName, string pythonFile )
        {
            TextFileUtility textFileUtility = new TextFileUtility();
            return textFileUtility.Save(fileName,pythonFile);
        }

        private XmlDocument ProcessTemplate(XmlDocument template, string rootNode)
        {
            XMLItemReplacer replacer = new XMLItemReplacer();
            replacer.ReplaceItems.Add(ReplaceItems.HARBOUR_NORMAL, HarbourList.Instance.Harbours[0]);
            replacer.ReplaceItems.Add(ReplaceItems.HARBOUR_UPPERCASE, HarbourList.Instance.Harbours[0].ToUpper());
            replacer.ReplaceItems.Add(ReplaceItems.YIELD, ComboBox_Yield.SelectedItem.ToString());
            replacer.ReplaceItems.Add(ReplaceItems.TRIGGER_START_VALUE, "100");
            replacer.ReplaceItems.Add(ReplaceItems.TRIGGER_DONE_VALUE, "1000");

            replacer.RootNode = rootNode;

            if (false == replacer.Replace(template))
            {
                return new XmlDocument();
            }

            return replacer.ReplacedContent;
        }

        private bool SaveFile(string fileName, XmlDocument xmlDocument)
        {
            XMLFileUtility xmlFileUtility = new XMLFileUtility();
            return xmlFileUtility.Save(fileName, xmlDocument);
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
            XMLHelper.IsXMLShapelyShowException(textBox_EventInfoStart.Text);
        }
        private void button_EventInfoDone_validate_Click(object sender, RoutedEventArgs e)
        {
            XMLHelper.IsXMLShapelyShowException(textBox_EventInfoDone.Text);
        }

        private void checkBox_CIV4GameText_Checked(object sender, RoutedEventArgs e)
        {
            textBox_CIV4GameText.IsEnabled = true;
        }

        private void checkBox_CIV4GameText_Unchecked(object sender, RoutedEventArgs e)
        {
            textBox_CIV4GameText.IsEnabled = false;
        }

        private void button_CIV4GameText_validate_Click(object sender, RoutedEventArgs e)
        {
            XMLHelper.IsXMLShapelyShowException(textBox_CIV4GameText.Text);
        }
    }
}
