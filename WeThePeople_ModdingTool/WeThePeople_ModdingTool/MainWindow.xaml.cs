using System;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using WeThePeople_ModdingTool.FileUtilities;
using System.Xml;
using WeThePeople_ModdingTool.Windows;
using WeThePeople_ModdingTool.Factories;
using WeThePeople_ModdingTool.DataSets;
using WeThePeople_ModdingTool.Processors;
using WeThePeople_ModdingTool.Validators;
using System.Diagnostics;

namespace WeThePeople_ModdingTool
{
    public partial class MainWindow : Window
    {
        private string selectedYieldType;
        private string selectedHarbour;
        EventProcessor eventProcessor = new EventProcessor();

        private IDictionary<CheckBox, TextBox> CheckBoxTextBox_Enable_Mapping = new Dictionary<CheckBox, TextBox>();
        private IDictionary<Button, TextBox> ButtonTextBox_Validation_Mapping = new Dictionary<Button, TextBox>();
        private IDictionary<Button, TextBox> ButtonTextBox_Delete_Mapping = new Dictionary<Button, TextBox>();

        public object StringUtility { get; private set; }

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
            CheckBoxTextBox_Enable_Mapping.Add(checkBox_EventInfoStart_Editable, textBox_EventInfoStart);
            CheckBoxTextBox_Enable_Mapping.Add(checkBox_CIV4GameText, textBox_CIV4GameText);
            CheckBoxTextBox_Enable_Mapping.Add(checkBox_EventInfoDone_1_Editable, textBox_EventInfoDone_1);
            CheckBoxTextBox_Enable_Mapping.Add(checkBox_EventInfoDone_2_Editable, textBox_EventInfoDone_2);
            CheckBoxTextBox_Enable_Mapping.Add(checkBox_EventInfoDone_3_Editable, textBox_EventInfoDone_3);
            CheckBoxTextBox_Enable_Mapping.Add(checkBox_EventInfoDone_4_Editable, textBox_EventInfoDone_4);
            CheckBoxTextBox_Enable_Mapping.Add(checkBox_EventInfoDone_5_Editable, textBox_EventInfoDone_5);

            ButtonTextBox_Validation_Mapping.Add(TriggerInfoStart_Validation_Button, TriggerInfoStart_TextBox);
            ButtonTextBox_Validation_Mapping.Add(TriggerInfoDone_Validation_Button, TriggerInfoDone_TextBox);
            ButtonTextBox_Validation_Mapping.Add(button_EventInfoStart_validate, textBox_EventInfoStart);
            ButtonTextBox_Validation_Mapping.Add(button_CIV4GameText_validate, textBox_CIV4GameText);
            ButtonTextBox_Validation_Mapping.Add(button_EventInfoDone_1_validate, textBox_EventInfoDone_1);
            ButtonTextBox_Validation_Mapping.Add(button_EventInfoDone_2_validate, textBox_EventInfoDone_2);
            ButtonTextBox_Validation_Mapping.Add(button_EventInfoDone_3_validate, textBox_EventInfoDone_3);
            ButtonTextBox_Validation_Mapping.Add(button_EventInfoDone_4_validate, textBox_EventInfoDone_4);
            ButtonTextBox_Validation_Mapping.Add(button_EventInfoDone_5_validate, textBox_EventInfoDone_5);

            ButtonTextBox_Delete_Mapping.Add(button_EventInfoDone_2_delete, textBox_EventInfoDone_2);
            ButtonTextBox_Delete_Mapping.Add(button_EventInfoDone_3_delete, textBox_EventInfoDone_3);
            ButtonTextBox_Delete_Mapping.Add(button_EventInfoDone_4_delete, textBox_EventInfoDone_4);
            ButtonTextBox_Delete_Mapping.Add(button_EventInfoDone_5_delete, textBox_EventInfoDone_5);
        }
        private void ComboBox_Yield_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedYieldType = ComboBox_Yield.SelectedItem.ToString();
            eventProcessor.YieldType = selectedYieldType;
        }
        private void comboBox_Harbours_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedHarbour = comboBox_Harbours.SelectedItem.ToString();
            eventProcessor.Harbour = selectedHarbour;
        }
        private void button_CreateEvents_Click(object sender, RoutedEventArgs e)
        {
            SaveCreatedEventFiles();
        }

        private void button_LoadTemplates_Click(object sender, RoutedEventArgs e)
        {
            textBox_PythonStart.Text = eventProcessor.Process(TemplateRepository.Instance.FindByNamePython(DataSetFactory.RandomEvent_Start));
            textBox_PythonDone.Text = eventProcessor.Process(TemplateRepository.Instance.FindByNamePython(DataSetFactory.RandomEvent_Done));

            DataSetXML dataSetXMLTriggerInfos_Start = TemplateRepository.Instance.FindByNameXML(DataSetFactory.EventTriggerInfos_Start);
            if( true == eventProcessor.ProcessAndSet(dataSetXMLTriggerInfos_Start) )
            {
                TriggerInfoStart_TextBox.Text = XMLHelper.FormatKeepIndention(XMLHelper.GetRootNodeListProcessedXML(dataSetXMLTriggerInfos_Start));
            }

            DataSetXML dataSetXMLTriggerInfos_Done = TemplateRepository.Instance.FindByNameXML(DataSetFactory.EventTriggerInfos_Done);
            if (true == eventProcessor.ProcessAndSet(dataSetXMLTriggerInfos_Done))
            {
                TriggerInfoDone_TextBox.Text = XMLHelper.FormatKeepIndention(XMLHelper.GetRootNodeListProcessedXML(dataSetXMLTriggerInfos_Done));
            }

            DataSetXML dataSetGameText = TemplateRepository.Instance.FindByNameXML(DataSetFactory.EventGameText);
            if( true == eventProcessor.ProcessAndSet(dataSetGameText) )
            {
                textBox_CIV4GameText.Text = XMLHelper.FormatKeepIndention(XMLHelper.GetRootNodeListProcessedXML(dataSetGameText));
            }

            ComboBox_Yield.IsEnabled = false;
            comboBox_Harbours.IsEnabled = false;
            button_CreateEventInfoStartXML.IsEnabled = true;
        }

        private void button_RestTemplates_Click(object sender, RoutedEventArgs e)
        {
            TemplateRepository.Instance.Reset();
            ClearTextBoxes();
            ComboBox_Yield.IsEnabled = true;
            comboBox_Harbours.IsEnabled = true;
            button_CreateEventInfoStartXML.IsEnabled = false;
            button_AddEventInfoDone.IsEnabled = false;
            CIV4EventInfos_Start.Visibility = Visibility.Hidden;
            CIV4EventInfos_Done_1.Visibility = Visibility.Hidden;
            CIV4EventInfos_Done_2.Visibility = Visibility.Hidden;
            CIV4EventInfos_Done_3.Visibility = Visibility.Hidden;
            CIV4EventInfos_Done_4.Visibility = Visibility.Hidden;
            CIV4EventInfos_Done_5.Visibility = Visibility.Hidden;
        }

        private void ClearTextBoxes()
        {
            foreach(KeyValuePair<CheckBox, TextBox> entry in CheckBoxTextBox_Enable_Mapping)
            {
                entry.Value.Text = String.Empty;
            }
        }

        private void SaveCreatedEventFiles()
        {
            if ( false == SaveFile(CreatePathFileConcrete( TemplateRepository.Instance.FindByNamePython(DataSetFactory.RandomEvent_Start)), textBox_PythonStart.Text))
            {
                CommonMessageBox.Show_OK_Warning(CommonVariables.FAILED_SAVING_FILE, CommonVariables.UNABLE_TO_SAVE_FILE +"");
            }

            if (false == SaveFile(CreatePathFileConcrete( TemplateRepository.Instance.FindByNamePython(DataSetFactory.RandomEvent_Done)), textBox_PythonDone.Text))
            {
                CommonMessageBox.Show_OK_Warning(CommonVariables.FAILED_SAVING_FILE, CommonVariables.UNABLE_TO_SAVE_FILE + "");
            }

            if (false == SaveFile(CreatePathFileConcrete(TemplateRepository.Instance.FindByNameXML(DataSetFactory.EventTriggerInfos_Start)), XMLHelper.CreateFromString(TriggerInfoStart_TextBox.Text)))
            {
                CommonMessageBox.Show_OK_Warning(CommonVariables.FAILED_SAVING_FILE, CommonVariables.UNABLE_TO_SAVE_FILE + "");
            }

            if (false == SaveFile(CreatePathFileConcrete(TemplateRepository.Instance.FindByNameXML(DataSetFactory.EventTriggerInfos_Done)), XMLHelper.CreateFromString(TriggerInfoDone_TextBox.Text)))
            {
                CommonMessageBox.Show_OK_Warning(CommonVariables.FAILED_SAVING_FILE, CommonVariables.UNABLE_TO_SAVE_FILE + "");
            }

            if (false == SaveFile(CreatePathFileConcrete(TemplateRepository.Instance.FindByNameXML(DataSetFactory.EventInfos_Start)), XMLHelper.CreateFromString(textBox_EventInfoStart.Text)))
            {
                CommonMessageBox.Show_OK_Warning(CommonVariables.FAILED_SAVING_FILE, CommonVariables.UNABLE_TO_SAVE_FILE + "");
            }

            if (false == SaveFile(CreatePathFileConcrete(TemplateRepository.Instance.FindByNameXML(DataSetFactory.EventInfos_Done)), XMLHelper.CreateFromString(textBox_EventInfoDone_1.Text)))
            {
                CommonMessageBox.Show_OK_Warning(CommonVariables.FAILED_SAVING_FILE, CommonVariables.UNABLE_TO_SAVE_FILE + "");
            }

            if (false == SaveFile(CreatePathFileConcrete(TemplateRepository.Instance.FindByNameXML(DataSetFactory.EventGameText)), XMLHelper.CreateFromString(textBox_CIV4GameText.Text)))
            {
                CommonMessageBox.Show_OK_Warning(CommonVariables.FAILED_SAVING_FILE, CommonVariables.UNABLE_TO_SAVE_FILE + "");
            }

        }

        private string CreatePathFileConcrete(DataSetBase dataSetBase)
        {
            string concreteFileName = selectedYieldType;
            concreteFileName += "_";
            concreteFileName += selectedHarbour.ToUpper();
            concreteFileName += dataSetBase.TemplateFileExtension;
            return dataSetBase.TemplateFileNameConcrete + concreteFileName;
        }

        private bool SaveFile(string fileName, string pythonFile )
        {
            TextFileUtility textFileUtility = new TextFileUtility();
            return textFileUtility.Save(fileName,pythonFile);
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
            textBox.IsReadOnly = false;
        }

        private void checkBox_Unchecked(object sender, RoutedEventArgs e)
        {
            TextBox textBox = GetTextBox(e.Source as CheckBox);
            if (null == textBox)
            {
                return;
            }
            textBox.IsReadOnly = true;
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

            if( true == WeThePeople_ModdingTool.Validators.StringValidator.IsNullOrWhiteSpace(textBox.Text) )
            {
                return;
            }
            XMLHelper.IsXMLShapelyShowMessageBox(textBox.Text);
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
            if ( false == eventInfoStartWindow.ShowDialog() )
            {
                return;
            }

            DataSetEventInfoStart dataSetEventInfo = eventInfoStartWindow.DataSetEventInfoStart;
            DataSetXML dataSetEventInfos_Start = TemplateRepository.Instance.FindByNameXML(DataSetFactory.EventInfos_Start);
            dataSetEventInfos_Start.TemplateReplaceItems[ReplaceItems.TRIGGER_VALUE_START] = dataSetEventInfo.GetTriggerValueStart();
            dataSetEventInfos_Start.TemplateReplaceItems[ReplaceItems.TRIGGER_VALUE_DONE] = dataSetEventInfo.GetTriggerValueDone();

            dataSetEventInfos_Start.XmlDocumentProcessed = eventProcessor.Process(dataSetEventInfos_Start);
            textBox_EventInfoStart.Text = XMLHelper.FormatKeepIndention( XMLHelper.GetRootNodeListProcessedXML(dataSetEventInfos_Start) );
            CIV4EventInfos_Start.Visibility = Visibility.Visible;
            tabControl_templates.SelectedItem = CIV4EventInfos_Start;
            button_AddEventInfoDone.IsEnabled = true;
        }

        private void button_AddEventInfoDone_Click(object sender, RoutedEventArgs e)
        {
            EventInfoDoneWindow eventInfoDoneWindow = new EventInfoDoneWindow();
            if( false == eventInfoDoneWindow.ShowDialog() )
            {
                return;
            }

            DataSetEventInfoDone dataSetEventInfoDone = eventInfoDoneWindow.DataSetEventInfoDone;
            DataSetXML dataSetEventInfos_Done = TemplateRepository.Instance.FindByNameXML(DataSetFactory.EventInfos_Done +"_1");
            dataSetEventInfos_Done.TemplateReplaceItems[ReplaceItems.GOLD] = dataSetEventInfoDone.GetGold();
            dataSetEventInfos_Done.TemplateReplaceItems[ReplaceItems.UNIT_CLASS] = dataSetEventInfoDone.GetUnitClass();
            dataSetEventInfos_Done.TemplateReplaceItems[ReplaceItems.UNIT_COUNT] = dataSetEventInfoDone.GetUnitCount();
            dataSetEventInfos_Done.TemplateReplaceItems[ReplaceItems.UNIT_EXPERIENCE] = dataSetEventInfoDone.GetUnitExperience();
            dataSetEventInfos_Done.TemplateReplaceItems[ReplaceItems.KING_RELATION] = dataSetEventInfoDone.GetKingRelation();
            dataSetEventInfos_Done.TemplateReplaceItems[ReplaceItems.YIELD_PRICE] = dataSetEventInfoDone.GetYieldPrice();

            if( false == eventProcessor.ProcessAndSet(dataSetEventInfos_Done) )
            {
                return;
            }
            textBox_EventInfoDone_1.Text = XMLHelper.FormatKeepIndention(XMLHelper.GetRootNodeListProcessedXML(dataSetEventInfos_Done));
            CIV4EventInfos_Done_1.Visibility = Visibility.Visible;
            tabControl_templates.SelectedItem = CIV4EventInfos_Done_1;

            UpdateEventTriggerInfoDone();
        }

        private void button_EventInfoDone_delete(object sender, RoutedEventArgs e)
        {

        }

        static string NODE_EVENTS = "Events";
        static string NODE_EVENT = "Event";
        static string NODE_TYPE = "Type";
        private bool UpdateEventTriggerInfoDone()
        {
            DataSetXML dataSetEventTriggerInfos_Done = TemplateRepository.Instance.FindByNameXML(DataSetFactory.EventTriggerInfos_Done);
            
            XmlNode nodeEvents = XMLHelper.FindNodeByName(XMLHelper.GetFirstChildRootNodeList(dataSetEventTriggerInfos_Done), NODE_EVENTS);
            if( null == nodeEvents )
            {
                return false;
            }

            DataSetXML eventInfoDone = TemplateRepository.Instance.FindByNameXML(DataSetFactory.EventInfos_Done + "_" + "1");
            XmlNode xmlNodeEvent = dataSetEventTriggerInfos_Done.XmlDocumentProcessed.CreateNode(XmlNodeType.Element, NODE_EVENT, null);
            xmlNodeEvent.InnerText = GetEventInfoDoneTypeName(eventInfoDone);

            if( true == XMLHelper.ContainsInnerNode(nodeEvents, xmlNodeEvent.InnerText) )
            {
                return true;
            }

            XmlNode nodeAdded = nodeEvents.AppendChild(xmlNodeEvent);
            TriggerInfoDone_TextBox.Text = XMLHelper.FormatKeepIndention( XMLHelper.GetRootNodeListProcessedXML(dataSetEventTriggerInfos_Done) );

            return true;
        }

        private string GetEventInfoDoneTypeName( DataSetXML dataSetXML )
        {
            XmlNodeList rootNodeList = XMLHelper.GetRootNodeListProcessedXML(dataSetXML);
            if( rootNodeList.Count != 1 )
            {
                return String.Empty;
            }

            XmlNode xmlNodeType = GetEventInfoDoneType(rootNodeList[0].ChildNodes );
            return xmlNodeType.InnerText;
        }

        private XmlNode GetEventInfoDoneType( XmlNodeList xmlNodeList )
        {
            return XMLHelper.FindNodeByName(xmlNodeList,NODE_TYPE);
        }
    }
}
