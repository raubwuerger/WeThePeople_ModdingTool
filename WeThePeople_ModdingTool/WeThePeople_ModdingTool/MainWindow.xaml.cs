using System;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using WeThePeople_ModdingTool.FileUtilities;
using System.Xml;
using WeThePeople_ModdingTool.Windows;
using WeThePeople_ModdingTool.Factories;
using WeThePeople_ModdingTool.DataSets;
using ICSharpCode.AvalonEdit;
using Serilog;
using System.IO;
using WeThePeople_ModdingTool.Creators;
using WeThePeople_ModdingTool.Validators;
using System.Linq;
using System.Text;

namespace WeThePeople_ModdingTool
{
    public partial class MainWindow : Window
    {
        public TabItem tabItemToDelete;

        private IDictionary<CheckBox, TextEditor> CheckBoxTextEditor_Enable_Mapping = new Dictionary<CheckBox, TextEditor>();
        private IDictionary<Button, TextEditor> ButtonTextEditor_Validation_Mapping = new Dictionary<Button, TextEditor>();
        private IDictionary<Button, TabItem> ButtonTextEditor_Delete_Mapping = new Dictionary<Button, TabItem>();
        private IDictionary<DataSetXML, TextEditor> DataSetXML_TextEditor_Mapping = new Dictionary<DataSetXML, TextEditor>();
        private IDictionary<DataSetPython, TextEditor> DataSetPython_TextEditor_Mapping = new Dictionary<DataSetPython, TextEditor>();

        public object StringUtility { get; private set; }

        public MainWindow()
        {
            InitializeComponent();
            string currentPath = System.IO.Directory.GetCurrentDirectory();
            LogFrameworkInitialzer.Init(this);
            CommandLineArgsParser.Parse();
            if (false == MainSettingsLoader.Instance.Init())
            {
                CommonMessageBox.Show_OK_Error("Initialization failed!", "Initialization failed! See log file!");
            }
            InitMapping();

            ComboBox_Yield.ItemsSource = YieldTypeRepository.Instance.YieldTypeNames;
            if (ComboBox_Yield.Items.Count > 0)
            {
                ComboBox_Yield.SelectedItem = YieldTypeRepository.Instance.YieldTypeNames[0];
            }

            comboBox_Harbours.ItemsSource = HarbourRepository.Instance.Harbours;
            if (comboBox_Harbours.Items.Count > 0)
            {
                comboBox_Harbours.SelectedItem = HarbourRepository.Instance.Harbours[0];
            }
            SetItemVisibility();
            InitReplaceStrings();
        }

        private void InitMapping()
        {
            CheckBoxTextEditor_Enable_Mapping.Add(checkBox_PythonStart_Editable, TextBox_Python_Start);
            CheckBoxTextEditor_Enable_Mapping.Add(checkBox_PythonDone_Editable, TextBox_Python_Done);

            CheckBoxTextEditor_Enable_Mapping.Add(TriggerInfoStart_Editable_CheckBox, TextBox_TriggerInfo_Start);
            CheckBoxTextEditor_Enable_Mapping.Add(TriggerInfoDone_Editable_CheckBox, TextBox_TriggerInfo_Done);
            CheckBoxTextEditor_Enable_Mapping.Add(checkBox_EventInfoStart_Editable, TextBox_EventInfo_Start);
            CheckBoxTextEditor_Enable_Mapping.Add(checkBox_CIV4GameText, TextBox_EventGameText);
            CheckBoxTextEditor_Enable_Mapping.Add(checkBox_EventInfoDone_1_Editable, TextBox_EventInfo_Done_1);
            CheckBoxTextEditor_Enable_Mapping.Add(checkBox_EventInfoDone_2_Editable, TextBox_EventInfo_Done_2);
            CheckBoxTextEditor_Enable_Mapping.Add(checkBox_EventInfoDone_3_Editable, TextBox_EventInfo_Done_3);
            CheckBoxTextEditor_Enable_Mapping.Add(checkBox_EventInfoDone_4_Editable, TextBox_EventInfo_Done_4);
            CheckBoxTextEditor_Enable_Mapping.Add(checkBox_EventInfoDone_5_Editable, TextBox_EventInfo_Done_5);
            CheckBoxTextEditor_Enable_Mapping.Add(checkBox_EventInfoDone_6_Editable, TextBox_EventInfo_Done_6);

            ButtonTextEditor_Validation_Mapping.Add(TriggerInfoStart_Validation_Button, TextBox_TriggerInfo_Start);
            ButtonTextEditor_Validation_Mapping.Add(TriggerInfoDone_Validation_Button, TextBox_TriggerInfo_Done);
            ButtonTextEditor_Validation_Mapping.Add(button_EventInfoStart_validate, TextBox_EventInfo_Start);
            ButtonTextEditor_Validation_Mapping.Add(button_CIV4GameText_validate, TextBox_EventGameText);
            ButtonTextEditor_Validation_Mapping.Add(button_EventInfoDone_1_validate, TextBox_EventInfo_Done_1);
            ButtonTextEditor_Validation_Mapping.Add(button_EventInfoDone_2_validate, TextBox_EventInfo_Done_2);
            ButtonTextEditor_Validation_Mapping.Add(button_EventInfoDone_3_validate, TextBox_EventInfo_Done_3);
            ButtonTextEditor_Validation_Mapping.Add(button_EventInfoDone_4_validate, TextBox_EventInfo_Done_4);
            ButtonTextEditor_Validation_Mapping.Add(button_EventInfoDone_5_validate, TextBox_EventInfo_Done_5);
            ButtonTextEditor_Validation_Mapping.Add(button_EventInfoDone_6_validate, TextBox_EventInfo_Done_6);

            ButtonTextEditor_Delete_Mapping.Add(button_EventInfoDone_1_delete, TabItem_EventInfo_Done_1);
            ButtonTextEditor_Delete_Mapping.Add(button_EventInfoDone_2_delete, TabItem_EventInfo_Done_2);
            ButtonTextEditor_Delete_Mapping.Add(button_EventInfoDone_3_delete, TabItem_EventInfo_Done_3);
            ButtonTextEditor_Delete_Mapping.Add(button_EventInfoDone_4_delete, TabItem_EventInfo_Done_4);
            ButtonTextEditor_Delete_Mapping.Add(button_EventInfoDone_5_delete, TabItem_EventInfo_Done_5);
            ButtonTextEditor_Delete_Mapping.Add(button_EventInfoDone_6_delete, TabItem_EventInfo_Done_6);

            DataSetXML_TextEditor_Mapping.Add(TemplateRepository.Instance.FindByNameXML(DataSetFactory.CIV4EventInfos_Start), TextBox_EventInfo_Start);
            DataSetXML_TextEditor_Mapping.Add(TemplateRepository.Instance.FindByNameXML(DataSetFactory.CIV4GameText_Colonization_Events_utf8_Start), TextBox_EventGameText);
            DataSetXML_TextEditor_Mapping.Add(TemplateRepository.Instance.FindByNameXML(DataSetFactory.CIV4EventTriggerInfos_Start), TextBox_TriggerInfo_Start);
            DataSetXML_TextEditor_Mapping.Add(TemplateRepository.Instance.FindByNameXML(DataSetFactory.CIV4EventTriggerInfos_Done), TextBox_TriggerInfo_Done);

            DataSetPython_TextEditor_Mapping.Add(TemplateRepository.Instance.FindByNamePython(DataSetFactory.CvRandomEventInterface_Start), TextBox_Python_Start);
            DataSetPython_TextEditor_Mapping.Add(TemplateRepository.Instance.FindByNamePython(DataSetFactory.CvRandomEventInterface_Done), TextBox_Python_Done);
        }

        private void SetItemVisibility()
        {
            button_CreateEvents.IsEnabled = false;
        }
        private void button_CreateEvents_Click(object sender, RoutedEventArgs e)
        {
            UpdateDataSets();
            SaveCreatedEventFiles();
        }

        private void button_LoadTemplates_Click(object sender, RoutedEventArgs e)
        {
            EventCreatorFactory eventCreatorFactory = new EventCreatorFactory();
            EventCreatorBase eventCreatorBaseEvents = eventCreatorFactory.CreateEventCreatorBaseEvents(this);

            if (false == eventCreatorBaseEvents.Create())
            {
                CommonMessageBox.Show_OK_Error("Operation failed!", "Creating events failed!\r\nSee log file.");
            }
        }

        private void button_RestTemplates_Click(object sender, RoutedEventArgs e)
        {
            TemplateRepository.Instance.Reset();
            ClearTextEditors();
            ComboBox_Yield.IsEnabled = true;
            comboBox_Harbours.IsEnabled = true;
            button_CreateEventInfoStartXML.IsEnabled = false;
            button_AddEventInfoDone.IsEnabled = false;
            button_LoadTemplates.IsEnabled = true;
            button_CreateEvents.IsEnabled = false;
            CIV4EventInfos_Start.Visibility = Visibility.Hidden;
            TabItem_EventInfo_Done_1.Visibility = Visibility.Hidden;
            TabItem_EventInfo_Done_2.Visibility = Visibility.Hidden;
            TabItem_EventInfo_Done_3.Visibility = Visibility.Hidden;
            TabItem_EventInfo_Done_4.Visibility = Visibility.Hidden;
            TabItem_EventInfo_Done_5.Visibility = Visibility.Hidden;
            TabItem_EventInfo_Done_6.Visibility = Visibility.Hidden;
        }

        private void ClearTextEditors()
        {
            foreach (KeyValuePair<CheckBox, TextEditor> entry in CheckBoxTextEditor_Enable_Mapping)
            {
                entry.Value.Text = String.Empty;
            }
        }

        private void SaveCreatedEventFiles()
        {
            EventCreatorFactory eventCreatorFactory = new EventCreatorFactory();
            EventCreatorBase eventCreatorBase = eventCreatorFactory.CreateEventCreatorFilesPutTogether(this);

            if (false == eventCreatorBase.Create())
            {
                CommonMessageBox.Show_OK_Error("Operation failed!", "Creating events failed!\r\nSee log file.");
            }
            else
            {
                CommonMessageBox.Show_Info("Operation succeeded!", "Creating events was successful!");
            }
        }

        private void UpdateDataSets()
        {
            foreach (KeyValuePair<DataSetXML, TextEditor> entry in DataSetXML_TextEditor_Mapping)
            {
                if (true == XMLHelper.IsXMLShapely(entry.Value.Text))
                {
                    entry.Key.XmlDocumentProcessed.LoadXml(entry.Value.Text);
                }
            }

            foreach (KeyValuePair<DataSetPython, TextEditor> entry in DataSetPython_TextEditor_Mapping)
            {
                entry.Key.PythonContentProcessed = entry.Value.Text;
            }

        }

        private void checkBox_Checked(object sender, RoutedEventArgs e)
        {
            TextEditor textEditor = GetTextEditorValidation(e.Source as CheckBox);
            if (null == textEditor)
            {
                return;
            }
            textEditor.IsReadOnly = false;
        }

        private void checkBox_Unchecked(object sender, RoutedEventArgs e)
        {
            TextEditor textEditor = GetTextEditorValidation(e.Source as CheckBox);
            if (null == textEditor)
            {
                return;
            }
            textEditor.IsReadOnly = true;
        }

        private TextEditor GetTextEditorValidation(CheckBox checkBox)
        {
            if (null == checkBox)
            {
                return null;
            }

            TextEditor textEditor;
            if (false == CheckBoxTextEditor_Enable_Mapping.TryGetValue(checkBox, out textEditor))
            {
                return null;
            }

            return textEditor;
        }

        private void button_Validation(object sender, RoutedEventArgs e)
        {
            TextEditor textEditor = GetTextEditorValidation(e.Source as Button);
            if (null == textEditor)
            {
                return;
            }
            ValidateXMLFile(textEditor.Text);
        }

        private void ValidateXMLFile(string xmlString)
        {
            if (true == StringValidator.IsNullOrWhiteSpace(xmlString))
            {
                return;
            }
            XMLHelper.IsXMLShapelyShowMessageBox(xmlString);
        }

        private TextEditor GetTextEditorValidation(Button button)
        {
            if (null == button)
            {
                return null;
            }

            TextEditor textEditor;
            if (false == ButtonTextEditor_Validation_Mapping.TryGetValue(button, out textEditor))
            {
                return null;
            }

            return textEditor;
        }

        private void button_CreateEventInfoStartXML_Click(object sender, RoutedEventArgs e)
        {
            EventCreatorFactory eventCreatorFactory = new EventCreatorFactory();
            EventCreatorBase eventCreator = eventCreatorFactory.CreateEventInfoStart(this);
            if (false == eventCreator.Create())
            {
                CommonMessageBox.Show_OK_Error("Operation failed!", "Creating event EventInfoStart failed!\r\nSee log file.");
            }
        }

        private void button_AddEventInfoDone_Click(object sender, RoutedEventArgs e)
        {
            EventCreatorFactory eventCreatorFactory = new EventCreatorFactory();
            EventCreatorBase eventCreator = eventCreatorFactory.CreateEventInfoDone(this);
            if (false == eventCreator.Create())
            {
                CommonMessageBox.Show_OK_Error("Operation failed!", "Creating event EventInfoDone failed!\r\nSee log file.");
            }
            CheckEventInfoDone();
        }

        private void CheckEventInfoDone()
        {
            button_AddEventInfoDone.IsEnabled = TemplateRepository.Instance.XmlDocumentEventDone.Count < 6;
        }

        private void button_EventInfoDone_delete(object sender, RoutedEventArgs e)
        {
            tabItemToDelete = GetTextEditorDelete(e.Source as Button);
            if (null == tabItemToDelete)
            {
                return;
            }

            EventCreatorFactory eventCreatorFactory = new EventCreatorFactory();
            EventCreatorBase eventCreator = eventCreatorFactory.CreateEventCreatorRemoveEventTriggerInfoDone(this);
            if (false == eventCreator.Create())
            {
                CommonMessageBox.Show_OK_Error("Operation failed!", "Removing EventInfoDone_X from EventInfoTriggerDone failed!\r\nSee log file.");
            }
            CheckEventInfoDone();
        }

        public TabItem GetTextEditorDelete(Button button)
        {
            if (null == button)
            {
                return null;
            }

            TabItem tabItem;
            if (false == ButtonTextEditor_Delete_Mapping.TryGetValue(button, out tabItem))
            {
                return null;
            }

            return tabItem;
        }

        private void DeactivateEventCreation()
        {

        }


        private void button_LoadXML_Click(object sender, RoutedEventArgs e)
        {
            DeactivateEventCreation();
            tabControl_templates.SelectedItem = TabItem_LoadXMLFile;
            TabItem_LoadXMLFile.Visibility = Visibility.Visible;
            System.Windows.Forms.OpenFileDialog dialog = new System.Windows.Forms.OpenFileDialog();
            dialog.Filter = "XML Files|*.xml";
            if (System.Windows.Forms.DialogResult.OK != dialog.ShowDialog())
            {
                return;
            }

            string xmlFile = TextFileUtility.Load(dialog.FileName);
            TextBox_LoadXMLFile.Text = xmlFile;
        }

        private void button_LoadXMLFile_hide_Click(object sender, RoutedEventArgs e)
        {
            TabItem_LoadXMLFile.Visibility = Visibility.Hidden;
            tabControl_templates.SelectedIndex = 0;
        }

        private void button_LoadXMLFile_validate_Click(object sender, RoutedEventArgs e)
        {
            ValidateXMLFile(TextBox_LoadXMLFile.Text);
        }

        private string GetSubnodeValue(XmlNode parentNode, string subnode)
        {
            foreach (XmlNode xmlNode in parentNode.ChildNodes)
            {
                if (xmlNode.Name.Equals(subnode))
                {
                    return xmlNode.InnerText;
                }
            }
            return "";
        }

        private void button_LoadXML_Civ4UnitInfos_Click(object sender, RoutedEventArgs e)
        {
            XmlDocument xmlDocument = XMLFileUtility.Load(WeThePeople_ModdingTool_Config.Instance.GetFullPathCIV4UnitInfos());
            if (null == xmlDocument)
            {
                return;
            }

            GridControl eventInfoStartWindow = new GridControl();
            eventInfoStartWindow.WindowState = WindowState.Maximized;
            eventInfoStartWindow.XmlDocument = xmlDocument;
            eventInfoStartWindow.Init();
            if (false == eventInfoStartWindow.ShowDialog())
            {
                return;
            }
        }

        public static int counterStart = 0;
        private void Test_logging_Click(object sender, RoutedEventArgs e)
        {
            int counterEnd = counterStart + 10000;
            for (int i = counterStart; i < counterEnd; i++)
            {
                Log.Information("Log entry:" + i.ToString());
            }
        }

        static private Dictionary<string, string> _replaceStrings = new Dictionary<string, string>();

        private void InitReplaceStrings()
        {
            _replaceStrings.Add(" />", "/>");
            _replaceStrings.Add("<LocalInfoText></LocalInfoText>", "<LocalInfoText/>");
            _replaceStrings.Add("<WorldNewsTexts></WorldNewsTexts>", "<WorldNewsTexts/>");
            _replaceStrings.Add("<OtherPlayerPopup></OtherPlayerPopup>", "<OtherPlayerPopup/>");
            _replaceStrings.Add("<UnitPromotion></UnitPromotion>", "<UnitPromotion/>");
            _replaceStrings.Add("<UnitName></UnitName>", "<UnitName/>");
            _replaceStrings.Add("<UnitCombatPromotions></UnitCombatPromotions>", "<UnitCombatPromotions/>");
            _replaceStrings.Add("<UnitClassPromotions></UnitClassPromotions>", "<UnitClassPromotions/>");
            _replaceStrings.Add("<BuildingExtraYields></BuildingExtraYields>", "<BuildingExtraYields/>");
            _replaceStrings.Add("<PlotExtraYields></PlotExtraYields>", "<PlotExtraYields/>");
            _replaceStrings.Add("<AdditionalEvents></AdditionalEvents>", "<AdditionalEvents/>");
            _replaceStrings.Add("<EventTimes></EventTimes>", "<EventTimes/>");
            _replaceStrings.Add("<ClearEvents></ClearEvents>", "<ClearEvents/>");
            _replaceStrings.Add("<PythonCallback></PythonCallback>", "<PythonCallback/>");
            _replaceStrings.Add("<PythonExpireCheck></PythonExpireCheck>", "<PythonExpireCheck/>");
            _replaceStrings.Add("<PythonCanDo></PythonCanDo>", "<PythonCanDo/>");
            _replaceStrings.Add("<QuestFailText></QuestFailText>", "<QuestFailText/>");

            _replaceStrings.Add("<UnitsRequired></UnitsRequired>", "<UnitsRequired/>");
            _replaceStrings.Add("<BuildingsRequired></BuildingsRequired>", "<BuildingsRequired/>");
            _replaceStrings.Add("<FeaturesRequired></FeaturesRequired>", "<FeaturesRequired/>");
            _replaceStrings.Add("<TerrainsRequired></TerrainsRequired>", "<TerrainsRequired/>");
            _replaceStrings.Add("<ImprovementsRequired></ImprovementsRequired>", "<ImprovementsRequired/>");
            _replaceStrings.Add("<RoutesRequired></RoutesRequired>", "<RoutesRequired/>");
            _replaceStrings.Add("<PrereqEvents></PrereqEvents>", "<PrereqEvents/>");
            _replaceStrings.Add("<PythonCanDoUnit></PythonCanDoUnit>", "<PythonCanDoUnit/>");
        }
        private void button_FormatXML_Click(object sender, RoutedEventArgs e)
        {
            DeactivateEventCreation();
            System.Windows.Forms.OpenFileDialog dialog = new System.Windows.Forms.OpenFileDialog();
            dialog.Filter = "XML Files|*.xml";
            if (System.Windows.Forms.DialogResult.OK != dialog.ShowDialog())
            {
                return;
            }

            List<string> linesFormated = new List<string>();
            string[] keys = _replaceStrings.Keys.ToArray();

            foreach (string line in File.ReadLines(dialog.FileName))
            {
                linesFormated.Add(DoReplace(line, keys));
            }

            StringBuilder builder = new StringBuilder();
            foreach (string line in linesFormated)
            {
                // Append string to StringBuilder.
                builder.Append(line).Append(Environment.NewLine);
            }
            TextFileUtility.Save(dialog.FileName + ".fmt", builder.ToString());
        }

        private string DoReplace(string original, string[] replaces)
        {
            foreach (string key in replaces)
            {
                if (false == original.Contains(key))
                {
                    continue;
                }
                return original.Replace(key, _replaceStrings[key]);
            }
            return original;
        }
    }
}
