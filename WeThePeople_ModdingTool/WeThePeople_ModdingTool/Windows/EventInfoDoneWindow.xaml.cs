using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using WeThePeople_ModdingTool.DataSets;
using WeThePeople_ModdingTool.Validators;

namespace WeThePeople_ModdingTool.Windows
{
    public partial class EventInfoDoneWindow : Window
    {
        private DataSetEventInfoDone dataSetEventInfoDone = new DataSetEventInfoDone();
        public DataSetEventInfoDone DataSetEventInfoDone
        {
            get
            {
                GetFromGUI();
                return dataSetEventInfoDone;
            }
            set
            {
                SetDataToGUI();
                dataSetEventInfoDone = value;
            }
        }

        public EventInfoDoneWindow()
        {
            InitializeComponent();
            InitGUIElements();
            SetDataToGUI();
        }

        private void InitGUIElements()
        {
            comboBox_UnitClass.ItemsSource = UnitClassRepository.Instance.UnitClasses;

            for( int i=0;i<100;i++ )
            {
                comboBox_UnitCount.Items.Add(i.ToString());
            }

            for( int i=-9;i<10;i++)
            {
                comboBox_RelationKing.Items.Add(i.ToString());
                comboBox_YieldPrice.Items.Add(i.ToString());
            }
        }

        private void button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void button_Ok_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
        private void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void SetDataToGUI()
        {
            textBox_Gold.Text = dataSetEventInfoDone.GetGold();
            comboBox_UnitClass.SelectedItem = dataSetEventInfoDone.GetUnitClass();
            comboBox_UnitCount.SelectedItem = dataSetEventInfoDone.GetUnitCount();
            textBox_UnitExperiance.Text = dataSetEventInfoDone.GetUnitExperience();
            comboBox_RelationKing.SelectedItem = dataSetEventInfoDone.GetKingRelation();
            comboBox_YieldPrice.SelectedItem = dataSetEventInfoDone.GetYieldPrice();
        }

        private void GetFromGUI()
        {
            dataSetEventInfoDone.SetGold(textBox_Gold.Text);
            if( -1 != comboBox_UnitClass.SelectedIndex )
            {
                dataSetEventInfoDone.SetUnitClass(comboBox_UnitClass.SelectedItem.ToString());
            }
            else            
            {
                dataSetEventInfoDone.SetUnitClass("");
            }
            dataSetEventInfoDone.SetUnitCount(comboBox_UnitCount.SelectedItem.ToString());
            dataSetEventInfoDone.SetUnitExperience(textBox_UnitExperiance.Text);
            dataSetEventInfoDone.SetKingRelation(comboBox_RelationKing.SelectedItem.ToString());
            dataSetEventInfoDone.SetYieldPrice(comboBox_YieldPrice.SelectedItem.ToString());
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
    }
}
