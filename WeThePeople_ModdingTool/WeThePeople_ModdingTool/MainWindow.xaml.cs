﻿using System;
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
using WeThePeople_ModdingTool.Creators;
using WeThePeople_ModdingTool.Helper;

namespace WeThePeople_ModdingTool
{
    public partial class MainWindow : Window
    {
        public TabItem tabItemToDelete;

        private IDictionary<CheckBox, TextBox> CheckBoxTextBox_Enable_Mapping = new Dictionary<CheckBox, TextBox>();
        private IDictionary<Button, TextBox> ButtonTextBox_Validation_Mapping = new Dictionary<Button, TextBox>();
        private IDictionary<Button, TabItem> ButtonTextBox_Delete_Mapping = new Dictionary<Button, TabItem>();
        private IDictionary<DataSetXML, TextBox> DataSetXML_TextBox_Mapping = new Dictionary<DataSetXML, TextBox>();
        private IDictionary<DataSetPython, TextBox> DataSetPython_TextBox_Mapping = new Dictionary<DataSetPython, TextBox>();

        public object StringUtility { get; private set; }

        public MainWindow()
        {
            InitializeComponent();
            LogFrameworkInitialzer.Init();
            CommandLineArgsParser.Parse();
            if( false == MainSettingsLoader.Instance.Init() )
            {
                CommonMessageBox.Show_OK_Error("Initialization failed!", "Initialization failed! See log file!");
            }
            InitMapping();

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
            SetItemVisibility();
        }

        private void InitMapping()
        {
            CheckBoxTextBox_Enable_Mapping.Add(checkBox_PythonStart_Editable, TextBox_Python_Start);
            CheckBoxTextBox_Enable_Mapping.Add(checkBox_PythonDone_Editable, TextBox_Python_Done);
            CheckBoxTextBox_Enable_Mapping.Add(TriggerInfoStart_Editable_CheckBox, TextBox_TriggerInfo_Start);
            CheckBoxTextBox_Enable_Mapping.Add(TriggerInfoDone_Editable_CheckBox, TextBox_TriggerInfo_Done);
            CheckBoxTextBox_Enable_Mapping.Add(checkBox_EventInfoStart_Editable, TextBox_EventInfo_Start);
            CheckBoxTextBox_Enable_Mapping.Add(checkBox_CIV4GameText, TextBox_EventGameText);
            CheckBoxTextBox_Enable_Mapping.Add(checkBox_EventInfoDone_1_Editable, TextBox_EventInfo_Done_1);
            CheckBoxTextBox_Enable_Mapping.Add(checkBox_EventInfoDone_2_Editable, TextBox_EventInfo_Done_2);
            CheckBoxTextBox_Enable_Mapping.Add(checkBox_EventInfoDone_3_Editable, TextBox_EventInfo_Done_3);
            CheckBoxTextBox_Enable_Mapping.Add(checkBox_EventInfoDone_4_Editable, TextBox_EventInfo_Done_4);
            CheckBoxTextBox_Enable_Mapping.Add(checkBox_EventInfoDone_5_Editable, TextBox_EventInfo_Done_5);

            ButtonTextBox_Validation_Mapping.Add(TriggerInfoStart_Validation_Button, TextBox_TriggerInfo_Start);
            ButtonTextBox_Validation_Mapping.Add(TriggerInfoDone_Validation_Button, TextBox_TriggerInfo_Done);
            ButtonTextBox_Validation_Mapping.Add(button_EventInfoStart_validate, TextBox_EventInfo_Start);
            ButtonTextBox_Validation_Mapping.Add(button_CIV4GameText_validate, TextBox_EventGameText);
            ButtonTextBox_Validation_Mapping.Add(button_EventInfoDone_1_validate, TextBox_EventInfo_Done_1);
            ButtonTextBox_Validation_Mapping.Add(button_EventInfoDone_2_validate, TextBox_EventInfo_Done_2);
            ButtonTextBox_Validation_Mapping.Add(button_EventInfoDone_3_validate, TextBox_EventInfo_Done_3);
            ButtonTextBox_Validation_Mapping.Add(button_EventInfoDone_4_validate, TextBox_EventInfo_Done_4);
            ButtonTextBox_Validation_Mapping.Add(button_EventInfoDone_5_validate, TextBox_EventInfo_Done_5);

            ButtonTextBox_Delete_Mapping.Add(button_EventInfoDone_1_delete, TabItem_EventInfo_Done_1);
            ButtonTextBox_Delete_Mapping.Add(button_EventInfoDone_2_delete, TabItem_EventInfo_Done_2);
            ButtonTextBox_Delete_Mapping.Add(button_EventInfoDone_3_delete, TabItem_EventInfo_Done_3);
            ButtonTextBox_Delete_Mapping.Add(button_EventInfoDone_4_delete, TabItem_EventInfo_Done_4);
            ButtonTextBox_Delete_Mapping.Add(button_EventInfoDone_5_delete, TabItem_EventInfo_Done_5);

            DataSetXML_TextBox_Mapping.Add(TemplateRepository.Instance.FindByNameXML(DataSetFactory.EventInfos_Start), TextBox_EventInfo_Start);
            DataSetXML_TextBox_Mapping.Add(TemplateRepository.Instance.FindByNameXML(DataSetFactory.EventGameText), TextBox_EventGameText);
            DataSetXML_TextBox_Mapping.Add(TemplateRepository.Instance.FindByNameXML(DataSetFactory.EventTriggerInfos_Start), TextBox_TriggerInfo_Start);
            DataSetXML_TextBox_Mapping.Add(TemplateRepository.Instance.FindByNameXML(DataSetFactory.EventTriggerInfos_Done), TextBox_TriggerInfo_Done);

            DataSetPython_TextBox_Mapping.Add(TemplateRepository.Instance.FindByNamePython(DataSetFactory.RandomEvent_Start), TextBox_Python_Start);
            DataSetPython_TextBox_Mapping.Add(TemplateRepository.Instance.FindByNamePython(DataSetFactory.RandomEvent_Done), TextBox_Python_Done);
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
            ClearTextBoxes();
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
            EventCreatorFactory eventCreatorFactory = new EventCreatorFactory();
            EventCreatorBase eventCreatorBase = eventCreatorFactory.CreateEventCreatorFilesPutTogether(this);

            if( false == eventCreatorBase.Create() )
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
            foreach(KeyValuePair<DataSetXML, TextBox> entry in DataSetXML_TextBox_Mapping )
            {
                if( true == XMLHelper.IsXMLShapely(entry.Value.Text) )
                {
                    entry.Key.XmlDocumentProcessed.LoadXml(entry.Value.Text);
                }
            }

            foreach (KeyValuePair<DataSetPython, TextBox> entry in DataSetPython_TextBox_Mapping)
            {
                entry.Key.PythonContentProcessed = entry.Value.Text;
            }
        }

        private void checkBox_Checked(object sender, RoutedEventArgs e)
        {
            TextBox textBox = GetTextBoxValidation(e.Source as CheckBox);
            if (null == textBox)
            {
                return;
            }
            textBox.IsReadOnly = false;
        }

        private void checkBox_Unchecked(object sender, RoutedEventArgs e)
        {
            TextBox textBox = GetTextBoxValidation(e.Source as CheckBox);
            if (null == textBox)
            {
                return;
            }
            textBox.IsReadOnly = true;
        }

        private TextBox GetTextBoxValidation( CheckBox checkBox )
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
            TextBox textBox = GetTextBoxValidation(e.Source as Button);
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

        private TextBox GetTextBoxValidation(Button button)
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
            EventCreatorFactory eventCreatorFactory = new EventCreatorFactory();
            EventCreatorBase eventCreator = eventCreatorFactory.CreateEventInfoStart(this);
            if( false == eventCreator.Create() )
            {
                CommonMessageBox.Show_OK_Error("Operation failed!", "Creating event EventInfoStart failed!\r\nSee log file.");
            }
        }

        private void button_AddEventInfoDone_Click(object sender, RoutedEventArgs e)
        {
            EventCreatorFactory eventCreatorFactory = new EventCreatorFactory();
            EventCreatorBase eventCreator = eventCreatorFactory.CreateEventInfoDone(this);
            if( false == eventCreator.Create() )
            {
                CommonMessageBox.Show_OK_Error("Operation failed!", "Creating event EventInfoDone failed!\r\nSee log file.");
            }
        }

        private void button_EventInfoDone_delete(object sender, RoutedEventArgs e)
        {
            tabItemToDelete = GetTextBoxDelete(e.Source as Button);
            if( null == tabItemToDelete)
            {
                return;
            }

            EventCreatorFactory eventCreatorFactory = new EventCreatorFactory();
            EventCreatorBase eventCreator = eventCreatorFactory.CreateEventCreatorRemoveEventTriggerInfoDone(this);
            if (false == eventCreator.Create())
            {
                CommonMessageBox.Show_OK_Error("Operation failed!", "Removing EventInfoDone_X from EventInfoTriggerDone failed!\r\nSee log file.");
            }
        }

        public TabItem GetTextBoxDelete( Button button )
        {
            if (null == button)
            {
                return null;
            }

            TabItem tabItem;
            if (false == ButtonTextBox_Delete_Mapping.TryGetValue(button, out tabItem))
            {
                return null;
            }

            return tabItem;
        }
    }
}
