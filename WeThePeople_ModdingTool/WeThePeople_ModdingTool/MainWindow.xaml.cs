using System;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using WeThePeople_ModdingTool.FileUtilities;
using System.Xml;
using WeThePeople_ModdingTool.Windows;
using WeThePeople_ModdingTool.Factories;
using WeThePeople_ModdingTool.DataSets;

namespace WeThePeople_ModdingTool
{
    public partial class MainWindow : Window
    {
        private string selectedYieldType;
        private string selectedHarbour;

        private IDictionary<CheckBox, TextBox> CheckBoxTextBox_Enable_Mapping = new Dictionary<CheckBox, TextBox>();
        private IDictionary<Button, TextBox> ButtonTextBox_Validation_Mapping = new Dictionary<Button, TextBox>();
//        private IDictionary<Button, TextBox> ButtonTextBox_Validation_Mapping = new Dictionary<Button, TextBox>();
        public MainWindow()
        {
            InitializeComponent();
            LogFrameworkInitialzer.Init();
            CommandLineArgsParser.Parse();
            InitMapping();
            if( false == MainSettingsLoader.Instance.Init() )
            {
                CommonMessageBox.Show_OK_Error("Initialization failed!", "Initialization failed! See log file!");
            }

            ComboBox_Yield.ItemsSource = YieldTypeRepository.Instance.YieldTypes;
            if( ComboBox_Yield.Items.Count > 0 )
            {
                ComboBox_Yield.SelectedItem = YieldTypeRepository.Instance.YieldTypes[0];
            }

            comboBox_Harbours.ItemsSource = HarbourRepository.Instance.Harbours;
            if( comboBox_Harbours.Items.Count > 0 )
            {
                comboBox_Harbours.SelectedItem = HarbourRepository.Instance.Harbours[0];
            }
        }

        private void InitMapping()
        {
            CheckBoxTextBox_Enable_Mapping.Add(checkBox_PythonStart_Editable, textBox_PythonStart);
            CheckBoxTextBox_Enable_Mapping.Add(checkBox_PythonDone_Editable, textBox_PythonDone);
            CheckBoxTextBox_Enable_Mapping.Add(TriggerInfoStart_Editable_CheckBox, TriggerInfoStart_TextBox);
            CheckBoxTextBox_Enable_Mapping.Add(TriggerInfoDone_Editable_CheckBox, TriggerInfoDone_TextBox);
            CheckBoxTextBox_Enable_Mapping.Add(checkBox_EventInfoDone_Editable, textBox_EventInfoDone);
            CheckBoxTextBox_Enable_Mapping.Add(checkBox_EventInfoStart_Editable, textBox_EventInfoStart);
            CheckBoxTextBox_Enable_Mapping.Add(checkBox_CIV4GameText, textBox_CIV4GameText);

            ButtonTextBox_Validation_Mapping.Add(TriggerInfoStart_Validation_Button, TriggerInfoStart_TextBox);
            ButtonTextBox_Validation_Mapping.Add(TriggerInfoDone_Validation_Button, TriggerInfoDone_TextBox);
            ButtonTextBox_Validation_Mapping.Add(button_EventInfoStart_validate, textBox_EventInfoStart);
            ButtonTextBox_Validation_Mapping.Add(button_EventInfoDone_validate, textBox_EventInfoDone);
            ButtonTextBox_Validation_Mapping.Add(button_CIV4GameText_validate, textBox_CIV4GameText);
        }
        private void ComboBox_Yield_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedYieldType = ComboBox_Yield.SelectedItem.ToString();
        }
        private void comboBox_Harbours_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedHarbour = comboBox_Harbours.SelectedItem.ToString();
        }
        private void button_CreateEvents_Click(object sender, RoutedEventArgs e)
        {
            CreateEvents();
        }

        private void button_LoadTemplates_Click(object sender, RoutedEventArgs e)
        {
            string CvRandomEventInterface_Start_Processed = ProcessTemplate(MainSettingsLoader.Instance.CvRandomEventInterface_Start_Template);
            textBox_PythonStart.Text = CvRandomEventInterface_Start_Processed;

            string CvRandomEventInterface_Done_Processed = ProcessTemplate(MainSettingsLoader.Instance.CvRandomEventInterface_Done_Template);
            textBox_PythonDone.Text = CvRandomEventInterface_Done_Processed;

            XmlDocument CIV4TriggerInfos_Start_Template_Processed = ProcessTemplateCommon(MainSettingsLoader.Instance.CIV4EventTriggerInfos_Start_Template, "/EventTriggerInfo");
            TriggerInfoStart_TextBox.Text = XMLHelper.FormatKeepIndention(CIV4TriggerInfos_Start_Template_Processed.DocumentElement.SelectNodes("/EventTriggerInfo"));

            XmlDocument CIV4TriggerInfos_Done_Template_Processed = ProcessTemplateCommon(MainSettingsLoader.Instance.CIV4EventTriggerInfos_Done_Template, "/EventTriggerInfo");
            TriggerInfoDone_TextBox.Text = XMLHelper.FormatKeepIndention(CIV4TriggerInfos_Done_Template_Processed.DocumentElement.SelectNodes("/EventTriggerInfo"));

            XmlDocument CIV4GameText_Colonization_Events_utf8_Processed = ProcessTemplateCommon(MainSettingsLoader.Instance.CIV4GameText_Colonization_Events_utf8_Template, "/Civ4GameText");
            textBox_CIV4GameText.Text = XMLHelper.FormatKeepIndention(CIV4GameText_Colonization_Events_utf8_Processed.DocumentElement.SelectNodes("/Civ4GameText"));

            button_CreateEventInfoStartXML.IsEnabled = true;
        }

        private void CreateEvents()
        {
            if ( false == SaveFile(CreatePathFileConcrete( TemplateRepository.Instance.FindByNamePython(DataSetFactory.RandomEvent_Start)), textBox_PythonStart.Text))
            {
                CommonMessageBox.Show_OK_Warning("Failed saving file!", "Unable to save file! " +"");
            }

            if (false == SaveFile(CreatePathFileConcrete( TemplateRepository.Instance.FindByNamePython(DataSetFactory.RandomEvent_Done)), textBox_PythonDone.Text))
            {
                CommonMessageBox.Show_OK_Warning("Failed saving file!", "Unable to save file! " + "");
            }

            if (false == SaveFile(CreatePathFileConcrete(TemplateRepository.Instance.FindByNameXML(DataSetFactory.EventTriggerInfos_Start)), XMLHelper.CreateFromString(TriggerInfoStart_TextBox.Text)))
            {
                CommonMessageBox.Show_OK_Warning("Failed saving file!", "Unable to save file! " + "");
            }

            if (false == SaveFile(CreatePathFileConcrete(TemplateRepository.Instance.FindByNameXML(DataSetFactory.EventTriggerInfos_Done)), XMLHelper.CreateFromString(TriggerInfoDone_TextBox.Text)))
            {
                CommonMessageBox.Show_OK_Warning("Failed saving file!", "Unable to save file! " + "");
            }

            if (false == SaveFile(CreatePathFileConcrete(TemplateRepository.Instance.FindByNameXML(DataSetFactory.EventInfos_Start)), XMLHelper.CreateFromString(textBox_EventInfoStart.Text)))
            {
                CommonMessageBox.Show_OK_Warning("Failed saving file!", "Unable to save file! " + "");
            }

            if (false == SaveFile(CreatePathFileConcrete(TemplateRepository.Instance.FindByNameXML(DataSetFactory.EventInfos_Done)), XMLHelper.CreateFromString(textBox_EventInfoDone.Text)))
            {
                CommonMessageBox.Show_OK_Warning("Failed saving file!", "Unable to save file! " + "");
            }

            if (false == SaveFile(CreatePathFileConcrete(TemplateRepository.Instance.FindByNameXML(DataSetFactory.EventGameText)), XMLHelper.CreateFromString(textBox_CIV4GameText.Text)))
            {
                CommonMessageBox.Show_OK_Warning("Failed saving file!", "Unable to save file! " + "");
            }

        }

        private string CreatePathFileConcrete(DataSetBase dataSetBase)
        {
            string concreteFileName = selectedYieldType;
            concreteFileName += dataSetBase.TemplatFileExtension;
            return dataSetBase.TemplateFileNameConcrete + concreteFileName;
        }

        private string ProcessTemplate( string template )
        {
            PythonItemReplacer replacer = new PythonItemReplacer();
            replacer.ReplaceItems.Add(ReplaceItems.HARBOUR_NORMAL, selectedHarbour);
            replacer.ReplaceItems.Add(ReplaceItems.HARBOUR_UPPERCASE, selectedHarbour.ToUpper());
            replacer.ReplaceItems.Add(ReplaceItems.YIELD, selectedYieldType);

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

        private XmlDocument ProcessTemplateCommon(XmlDocument template, string rootNode)
        {
            XMLItemReplacer replacer = new XMLItemReplacer();
            replacer.ReplaceItems.Add(ReplaceItems.HARBOUR_NORMAL, selectedHarbour);
            replacer.ReplaceItems.Add(ReplaceItems.HARBOUR_UPPERCASE, selectedHarbour.ToUpper());
            replacer.ReplaceItems.Add(ReplaceItems.YIELD, selectedYieldType);
            replacer.ReplaceItems.Add(ReplaceItems.TRIGGER_START_VALUE, "100");
            replacer.ReplaceItems.Add(ReplaceItems.TRIGGER_DONE_VALUE, "1000");
            replacer.ReplaceItems.Add(ReplaceItems.GOLD, "1000");
            replacer.ReplaceItems.Add(ReplaceItems.UNIT_CLASS, "");
            replacer.ReplaceItems.Add(ReplaceItems.UNIT_COUNT, "1");
            replacer.ReplaceItems.Add(ReplaceItems.KING_RELATION, "1");
            replacer.ReplaceItems.Add(ReplaceItems.YIELD_PRICE, "1");

            replacer.RootNode = rootNode;

            if (false == replacer.Replace(template))
            {
                return new XmlDocument();
            }

            return replacer.ReplacedContent;
        }

        private XmlDocument ProcessTemplateEventInfoStart(XmlDocument template, string rootNode)
        {
            XMLItemReplacer replacer = new XMLItemReplacer();
            replacer.ReplaceItems.Add(ReplaceItems.HARBOUR_NORMAL, selectedHarbour);
            replacer.ReplaceItems.Add(ReplaceItems.HARBOUR_UPPERCASE, selectedHarbour.ToUpper());
            replacer.ReplaceItems.Add(ReplaceItems.YIELD, selectedYieldType);
            replacer.ReplaceItems.Add(ReplaceItems.TRIGGER_START_VALUE, "100");
            replacer.ReplaceItems.Add(ReplaceItems.TRIGGER_DONE_VALUE, "1000");

            replacer.RootNode = rootNode;

            if (false == replacer.Replace(template))
            {
                return new XmlDocument();
            }

            return replacer.ReplacedContent;
        }

        private XmlDocument ProcessTemplateEventInfoDone(XmlDocument template, string rootNode)
        {
            XMLItemReplacer replacer = new XMLItemReplacer();
            replacer.ReplaceItems.Add(ReplaceItems.HARBOUR_NORMAL, selectedHarbour);
            replacer.ReplaceItems.Add(ReplaceItems.HARBOUR_UPPERCASE, selectedHarbour.ToUpper());
            replacer.ReplaceItems.Add(ReplaceItems.YIELD, selectedYieldType);
            replacer.ReplaceItems.Add(ReplaceItems.GOLD, "1000");
            replacer.ReplaceItems.Add(ReplaceItems.UNIT_CLASS, "");
            replacer.ReplaceItems.Add(ReplaceItems.UNIT_COUNT, "1");
            replacer.ReplaceItems.Add(ReplaceItems.UNIT_EXPERIENCE, "20");
            replacer.ReplaceItems.Add(ReplaceItems.KING_RELATION, "1");
            replacer.ReplaceItems.Add(ReplaceItems.YIELD_PRICE, "1");

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

        private void checkBox_Checked(object sender, RoutedEventArgs e)
        {
            TextBox textBox = GetTextBox(e.Source as CheckBox);
            if (null == textBox)
            {
                return;
            }
            textBox.IsEnabled = true;
        }

        private void checkBox_Unchecked(object sender, RoutedEventArgs e)
        {
            TextBox textBox = GetTextBox(e.Source as CheckBox);
            if (null == textBox)
            {
                return;
            }
            textBox.IsEnabled = false;
        }

        private TextBox GetTextBox( CheckBox checkBox )
        {
            if( null == checkBox )
            {
                return null;
            }

            TextBox textBox;
            if( false == CheckBoxTextBox_Enable_Mapping.TryGetValue(checkBox, out textBox) )
            {
                return null;
            }

            return textBox;
        }

        private void button_Validation(object sender, RoutedEventArgs e)
        {
            TextBox textBox = GetTextBox(e.Source as Button);
            if (null == textBox)
            {
                return;
            }
            XMLHelper.IsXMLShapelyShowException(textBox.Text);
        }

        private TextBox GetTextBox(Button button)
        {
            if (null == button)
            {
                return null;
            }

            TextBox textBox;
            if (false == ButtonTextBox_Validation_Mapping.TryGetValue(button, out textBox))
            {
                return null;
            }

            return textBox;
        }

        private void button_CreateEventInfoStartXML_Click(object sender, RoutedEventArgs e)
        {
            EventInfoStartWindow eventInfoStartWindow = new EventInfoStartWindow();
            if( false == eventInfoStartWindow.ShowDialog() )
            {
                return;
            }

            XmlDocument CIV4EventInfos_Start_Template_Processed = ProcessTemplateEventInfoStart(MainSettingsLoader.Instance.CIV4EventInfos_Start_Template, "/EventInfo");
            textBox_EventInfoStart.Text = XMLHelper.FormatKeepIndention(CIV4EventInfos_Start_Template_Processed.DocumentElement.SelectNodes("/EventInfo"));
            CIV4EventInfos_Start.Visibility = Visibility.Visible;
            button_button_AddEventInfoDone.IsEnabled = true;
        }

        private void button_button_AddEventInfoDone_Click(object sender, RoutedEventArgs e)
        {
            EventInfoDoneWindow eventInfoDoneWindow = new EventInfoDoneWindow();
            if( false == eventInfoDoneWindow.ShowDialog() )
            {
                return;
            }

            XmlDocument CIV4EventInfos_Done_Template_Processed = ProcessTemplateEventInfoDone(MainSettingsLoader.Instance.CIV4EventInfos_Done_Template, "/EventInfo");
            textBox_EventInfoDone.Text = XMLHelper.FormatKeepIndention(CIV4EventInfos_Done_Template_Processed.DocumentElement.SelectNodes("/EventInfo"));

            CIV4EventInfos_Done.Visibility = Visibility.Visible;
        }
    }
}
