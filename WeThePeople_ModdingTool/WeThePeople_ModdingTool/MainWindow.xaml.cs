﻿using System;
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
            MessageBox.Show(ComboBox_Yield.SelectedItem.ToString());
        }

        private void button_CreateEvents_Click(object sender, RoutedEventArgs e)
        {
            PrepareTemplates();
        }

        private void button_LoadTemplates_Click(object sender, RoutedEventArgs e)
        {
            if( false == AtLeastOneButtonChecked() )
            {
                CommonMessageBox.Show_OK_Warning("No Harbour seleceted!", "At least one harbour must be selected!");
                return;
            }
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
            string CvRandomEventInterface_Start_Processed = ProcessTemplate(MainSettingsLoader.Instance.CvRandomEventInterface_Start_Template, HarbourList.Instance.Harbours[0]);
            if( false == SaveFile(CreatePathFileYield(MainSettingsLoader.Instance.CvRandomEventInterface_Start_Concrete), CvRandomEventInterface_Start_Processed) )
            {
                CommonMessageBox.Show_OK_Warning("Failed saving file!", "Unable to save file! " +"");
            }

            textBlock_PythonStart.Text = CvRandomEventInterface_Start_Processed;

            string CvRandomEventInterface_Done_Processed = ProcessTemplate(MainSettingsLoader.Instance.CvRandomEventInterface_Done_Template, HarbourList.Instance.Harbours[0]);
            if (false == SaveFile(CreatePathFileYield(MainSettingsLoader.Instance.CvRandomEventInterface_Done_Concrete), CvRandomEventInterface_Done_Processed))
            {
                CommonMessageBox.Show_OK_Warning("Failed saving file!", "Unable to save file! " + "");
            }

            textBox_PythonDone.Text = CvRandomEventInterface_Done_Processed;
        }

        private string CreatePathFileYield( string baseFile )
        {
            string absoluteProgramPath = AppDomain.CurrentDomain.BaseDirectory;
            string relativeAssetPath = System.IO.Path.Combine(absoluteProgramPath, MainSettingsLoader.Instance.assetPathRelative);
            
            string appendix = baseFile;
            appendix += ComboBox_Yield.SelectedItem.ToString();
            appendix += ".py";

            return System.IO.Path.Combine(relativeAssetPath, appendix);
        }

        private string ProcessTemplate( string template, string harbour )
        {
            PythonItemReplacer replacer = new PythonItemReplacer();
            replacer.ReplaceItems.Add(ReplaceItems.HARBOUR_NORMAL, harbour);
            replacer.ReplaceItems.Add(ReplaceItems.HARBOUR_UPPERCASE, harbour.ToUpper());
            replacer.ReplaceItems.Add(ReplaceItems.YIELD, ComboBox_Yield.SelectedItem.ToString());

            if (false == replacer.Replace(template))
            {
                return "";
            }

            return replacer.ReplacedString;
        }

        private bool SaveFile(string fileName, string pythonFile )
        {
            TextFileUtility textFileLoader = new TextFileUtility();
            return textFileLoader.Save(fileName,pythonFile);
        }

        private XmlDocument ProcessTemplate(XmlDocument template, string harbour)
        {
            XMLItemReplacer replacer = new XMLItemReplacer();
            replacer.ReplaceItems.Add(ReplaceItems.HARBOUR_NORMAL, harbour);
            replacer.ReplaceItems.Add(ReplaceItems.HARBOUR_UPPERCASE, harbour.ToUpper());
            replacer.ReplaceItems.Add(ReplaceItems.YIELD, ComboBox_Yield.SelectedItem.ToString());
            replacer.ReplaceItems.Add(ReplaceItems.TRIGGER_START_VALUE, "100");
            replacer.ReplaceItems.Add(ReplaceItems.TRIGGER_DONE_VALUE, "1000");

            if (false == replacer.Replace(template))
            {
                return null;
            }

            return replacer.ReplacedContent;
        }

    }
}
