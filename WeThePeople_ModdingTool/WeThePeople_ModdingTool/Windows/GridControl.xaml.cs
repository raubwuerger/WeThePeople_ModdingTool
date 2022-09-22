using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml;
using unvell.ReoGrid;
using WeThePeople_ModdingTool.FileUtilities;
using WeThePeople_ModdingTool.Validators;

namespace WeThePeople_ModdingTool.Windows
{
    /// <summary>
    /// Interaction logic for GridControl.xaml
    /// </summary>
    public partial class GridControl : Window
    {
        private readonly string Civ4UnitInfos_Type = "Type";
        private readonly string Civ4UnitInfos_UnitInfo = "UnitInfo";
        private readonly string Civ4UnitInfos_iEuropeCost = "iEuropeCost";
        private readonly string Civ4UnitInfos_iAfricaCost = "iAfricaCost";
        private readonly string Civ4UnitInfos_iPortRoyalCost = "iPortRoyalCost";
        private readonly string Civ4UnitInfos_bAnimal = "bAnimal";
        private readonly string Civ4UnitInfos_Special = "Special";

        private readonly string PREFIX_UNIT = "UNIT_";
        private readonly string NODE_NOT_FOUND = "NODE_NOT_FOUND";
        private readonly string SPECIALUNIT_YIELD_CARGO = "SPECIALUNIT_YIELD_CARGO";

        private XmlDocument xmlDocument;
        public System.Xml.XmlDocument XmlDocument
        {
            set { xmlDocument = value; }
        }
        public GridControl()
        {
            InitializeComponent();
        }

        public void Init()
        {
            Initialize();
            CreateHeader();
            FillData();
        }

        private void Initialize()
        {
            var sheet = grid.CurrentWorksheet;
            sheet.FreezeToCell(1, 0);
            var worksheet = grid.CurrentWorksheet;
            worksheet.SetSettings(WorksheetSettings.Edit_Readonly, true);
        }

        private void CreateHeader()
        {
            var sheet = grid.CurrentWorksheet;
            sheet[0, 0] = "UnitType";
            sheet[0, 1] = "Europe";
            sheet[0, 2] = "Africa";
            sheet[0, 3] = "PortRoyal";
        }
        private void FillData()
        {
            var sheet = grid.CurrentWorksheet;

            List<string> units = new List<string>();
            IDictionary<string, int> unitTypeEurope = new System.Collections.Generic.Dictionary<string, int>();
            IDictionary<string, int> unitTypeAfrica = new System.Collections.Generic.Dictionary<string, int>();
            IDictionary<string, int> unitTypePortRoyal = new System.Collections.Generic.Dictionary<string, int>();

            XmlNodeList xmlNodeList = xmlDocument.DocumentElement.GetElementsByTagName(Civ4UnitInfos_UnitInfo);
            foreach (XmlNode xmlNode in xmlNodeList)
            {
                XmlNode xmlNodeType = XMLHelper.FindNodeByName(xmlNode.ChildNodes, Civ4UnitInfos_Type);
                if (null == xmlNodeType)
                {
                    continue;
                }

                string unitName = xmlNodeType.InnerText;

                string animal = GetSubnodeValue(xmlNode, Civ4UnitInfos_bAnimal);
                if ( animal.Equals("1") )
                {
                    continue;
                }

                string cargo = GetSubnodeValue(xmlNode, Civ4UnitInfos_Special);
                if( cargo.Equals(SPECIALUNIT_YIELD_CARGO) )
                {
                    continue;
                }

                int costEurope = 0;
                StringValidator.GetNaturalNumber(GetSubnodeValue(xmlNode, Civ4UnitInfos_iEuropeCost), out costEurope);
                if( costEurope <= 0 )
                {
                    costEurope = 0;
                }

                int costAfrica = 0;
                StringValidator.GetNaturalNumber(GetSubnodeValue(xmlNode, Civ4UnitInfos_iAfricaCost), out costAfrica);
                if(costAfrica <= 0)
                {
                    costAfrica = 0;
                }

                int costPortRoyal = 0;
                StringValidator.GetNaturalNumber(GetSubnodeValue(xmlNode, Civ4UnitInfos_iPortRoyalCost), out costPortRoyal);
                if(costPortRoyal <= 0)
                {
                    costPortRoyal = 0;
                }

                units.Add(unitName);
                unitTypeEurope.Add(unitName, costEurope);
                unitTypeAfrica.Add(unitName, costAfrica);
                unitTypePortRoyal.Add(unitName, costPortRoyal);
            }

            int rowStartIndex = 1;
            int currentRowIndex = rowStartIndex;
            int columnStartIndex = 0;
            int currentColumnIndex = columnStartIndex;
            int index = 0;

            sheet.RowCount = (units.Count + 1);

            foreach (string unit in units )
            {
                currentColumnIndex = columnStartIndex;
                sheet[currentRowIndex, currentColumnIndex++] = RemoveFront(PREFIX_UNIT, units[index]);
                sheet[currentRowIndex, currentColumnIndex++] = unitTypeEurope[units[index]];
                sheet[currentRowIndex, currentColumnIndex++] = unitTypeAfrica[units[index]];
                sheet[currentRowIndex, currentColumnIndex++] = unitTypePortRoyal[units[index]];

                if (unitTypeEurope[units[index]] == 0 && unitTypeAfrica[units[index]] == 0 && unitTypePortRoyal[units[index]] == 0)
                {
                    int europeCost = unitTypeEurope[units[index]];
                    int africaCost = unitTypeAfrica[units[index]];
                    int portRoyalCost = unitTypePortRoyal[units[index]];

                    var range = sheet.Ranges[currentRowIndex, columnStartIndex, 1, currentColumnIndex];
                    range.Style.BackColor = Color.FromRgb(211,211,211);
                }

                currentRowIndex++;
                index++;
            }

            sheet.AutoFitColumnWidth(0, false);
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
            return NODE_NOT_FOUND;
        }

        private string RemoveFront(  string removeString, string sourceString )
        {
            int startIndex = 0;
            int endIndex = removeString.Length;
            return sourceString.Remove(startIndex, endIndex);
        }

    }
}
