using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using WeThePeople_ModdingTool.DataSets;
using WeThePeople_ModdingTool.FileUtilities;
using WeThePeople_ModdingTool.Helper;
using WeThePeople_ModdingTool.Validators;

namespace WeThePeople_ModdingTool.Windows
{
    public partial class EventInfoDoneWindow : Window
    {
        private string yield;
        public string Yield
        {
            set { yield = value; }
        }

        private string harbour;
        public string Harbour
        {
            set { harbour = value; }
        }

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
        }

        public void Init()
        {
            InitGUIElements();
            SetDataToGUI();
        }

        private void InitGUIElements()
        {
            label_EventInfoDone.Content = "Create EventInfoDone: " + yield + " - " + harbour;

            comboBox_UnitClass.ItemsSource = UnitClassRepository.Instance.UnitClassNames;

            for (int i = 0; i <= 100; i++)
            {
                comboBox_UnitCount.Items.Add(i.ToString());
            }

            for (int i = -9; i < 10; i++)
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
            if (false == IsAtLeastOneInputValid())
            {
                if (MessageBoxResult.No == CommonMessageBox.Show_YesNo("Input not valid!", "No changes made!\r\nDo you want to proceed?"))
                {
                    return;
                }
            }
            else
            {
                if (false == IsUnitCreationValid())
                {
                    if (MessageBoxResult.No == CommonMessageBox.Show_YesNo("Input not valid!", "Unit creation invalid!\r\nDo you want to proceed?"))
                    {
                        return;
                    }
                }
            }

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
            comboBox_UnitClass.SelectedItem = UnitClassRepository.Instance.GetKeyFromValue(dataSetEventInfoDone.GetUnitClass());
            comboBox_UnitCount.SelectedItem = dataSetEventInfoDone.GetUnitCount();
            textBox_UnitExperiance.Text = dataSetEventInfoDone.GetUnitExperience();
            comboBox_RelationKing.SelectedItem = dataSetEventInfoDone.GetKingRelation();
            comboBox_YieldPrice.SelectedItem = dataSetEventInfoDone.GetYieldPrice();
        }

        private void GetFromGUI()
        {
            dataSetEventInfoDone.SetGold(GetGold());
            if (-1 != comboBox_UnitClass.SelectedIndex)
            {
                dataSetEventInfoDone.SetUnitClass(UnitClassRepository.Instance.GetValueFromName(comboBox_UnitClass.SelectedItem.ToString()));
            }
            else
            {
                dataSetEventInfoDone.SetUnitClass("NONE");
            }
            dataSetEventInfoDone.SetUnitCount(comboBox_UnitCount.SelectedItem.ToString());
            dataSetEventInfoDone.SetUnitExperience(textBox_UnitExperiance.Text);
            dataSetEventInfoDone.SetKingRelation(comboBox_RelationKing.SelectedItem.ToString());
            dataSetEventInfoDone.SetYieldPrice(comboBox_YieldPrice.SelectedItem.ToString());
        }

        private String GetGold()
        {
            int number;
            bool isNumeric = int.TryParse(textBox_Gold.Text, out number);
            if (false == isNumeric)
            {
                return "0";
            }
            return textBox_Gold.Text;
        }

        private bool IsAtLeastOneInputValid()
        {
            if (true == IsGoldValid())
            {
                return true;
            }

            if (true == IsUnitClassValid())
            {
                return true;
            }

            if (true == IsUnitCountValid())
            {
                return true;
            }

            if (true == IsUnitExperienceValid())
            {
                return true;
            }

            if (true == IsKingRelationValid())
            {
                return true;
            }

            if (true == IsYieldPriceValid())
            {
                return true;
            }

            return false;
        }

        private bool IsUnitCreationValid()
        {
            if (false == IsUnitClassValid() && false == IsUnitCountValid() && false == IsUnitExperienceValid())
            {
                return true;
            }

            if (IsUnitClassValid() && IsUnitCountValid())
            {
                return true;
            }

            return false;
        }

        private bool IsUnitClassValid()
        {
            if (comboBox_UnitClass.SelectedItem == null)
            {
                return false;
            }

            return true;
        }

        private bool IsUnitCountValid()
        {
            if (comboBox_UnitCount.SelectedItem == null)
            {
                return false;
            }

            if (StringValidator.IsNullOrWhiteSpace(comboBox_UnitCount.SelectedItem.ToString()))
            {
                return false;
            }

            return StringHelper.IsNumberGreaterZero(comboBox_UnitCount.SelectedItem.ToString());
        }

        private bool IsUnitExperienceValid()
        {
            if (true == StringValidator.IsNullOrWhiteSpace(textBox_UnitExperiance.Text))
            {
                return false;
            }

            return StringHelper.IsNumberGreaterZero(textBox_UnitExperiance.Text);
        }

        private bool IsGoldValid()
        {
            if (true == StringValidator.IsNullOrWhiteSpace(textBox_Gold.Text))
            {
                return false;
            }

            return StringHelper.IsNumberGreaterZero(textBox_Gold.Text);
        }

        private bool IsKingRelationValid()
        {
            return false == "0".Equals(comboBox_RelationKing.SelectedItem.ToString());
        }

        private bool IsYieldPriceValid()
        {
            return false == "0".Equals(comboBox_YieldPrice.SelectedItem.ToString());
        }

        private void button_Gold_Clear_Click(object sender, RoutedEventArgs e)
        {
            textBox_Gold.Text = "0";
        }

        private void button_UnitClass_Clear_Click(object sender, RoutedEventArgs e)
        {
            comboBox_UnitClass.SelectedIndex = -1;
        }

        private void button_UnitCount_Clear_Click(object sender, RoutedEventArgs e)
        {
            comboBox_UnitCount.SelectedIndex = -1;
        }

        private void button_UnitExperience_Clear_Click(object sender, RoutedEventArgs e)
        {
            textBox_UnitExperiance.Text = "0";
        }

        private void button_RelationKing_Clear_Click(object sender, RoutedEventArgs e)
        {
            comboBox_RelationKing.SelectedIndex = -1;
        }

        private void button_YieldPrice_Clear_Click(object sender, RoutedEventArgs e)
        {
            comboBox_YieldPrice.SelectedIndex = -1;
        }
    }
}
