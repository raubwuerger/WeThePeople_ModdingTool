using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
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
        private readonly string Civ4UnitInfos_Domain = "Domain";

        private readonly string PREFIX_UNIT = "UNIT_";
        private readonly string PREFIX_DOMAIN = "DOMAIN_";
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
            sheet[0, 4] = "Domain";
        }
        private void FillData()
        {
            var sheet = grid.CurrentWorksheet;

            List<string> units = new List<string>();
            IDictionary<string, int> costEuropeAll = new System.Collections.Generic.Dictionary<string, int>();
            IDictionary<string, int> costAfricaAll = new System.Collections.Generic.Dictionary<string, int>();
            IDictionary<string, int> costPortRoyalAll = new System.Collections.Generic.Dictionary<string, int>();
            List<string> domainAll = new List<string>();

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
                if (animal.Equals("1"))
                {
                    continue;
                }

                string cargo = GetSubnodeValue(xmlNode, Civ4UnitInfos_Special);
                if (cargo.Equals(SPECIALUNIT_YIELD_CARGO))
                {
                    continue;
                }

                int costEurope = 0;
                StringValidator.GetNaturalNumber(GetSubnodeValue(xmlNode, Civ4UnitInfos_iEuropeCost), out costEurope);
                if (costEurope <= 0)
                {
                    costEurope = 0;
                }

                int costAfrica = 0;
                StringValidator.GetNaturalNumber(GetSubnodeValue(xmlNode, Civ4UnitInfos_iAfricaCost), out costAfrica);
                if (costAfrica <= 0)
                {
                    costAfrica = 0;
                }

                int costPortRoyal = 0;
                StringValidator.GetNaturalNumber(GetSubnodeValue(xmlNode, Civ4UnitInfos_iPortRoyalCost), out costPortRoyal);
                if (costPortRoyal <= 0)
                {
                    costPortRoyal = 0;
                }

                string domain = GetSubnodeValue(xmlNode, Civ4UnitInfos_Domain);

                units.Add(unitName);
                costEuropeAll.Add(unitName, costEurope);
                costAfricaAll.Add(unitName, costAfrica);
                costPortRoyalAll.Add(unitName, costPortRoyal);
                domainAll.Add(domain);
            }

            int rowStartIndex = 1;
            int currentRowIndex = rowStartIndex;
            int columnStartIndex = 0;
            int currentColumnIndex = columnStartIndex;
            int index = 0;

            sheet.RowCount = (units.Count + 1);

            foreach (string unit in units)
            {
                currentColumnIndex = columnStartIndex;
                sheet[currentRowIndex, currentColumnIndex++] = RemoveFront(PREFIX_UNIT, units[index]);
                sheet[currentRowIndex, currentColumnIndex++] = costEuropeAll[units[index]];
                sheet[currentRowIndex, currentColumnIndex++] = costAfricaAll[units[index]];
                sheet[currentRowIndex, currentColumnIndex++] = costPortRoyalAll[units[index]];
                sheet[currentRowIndex, currentColumnIndex++] = RemoveFront(PREFIX_DOMAIN, domainAll[index]);

                if (costEuropeAll[units[index]] == 0 && costAfricaAll[units[index]] == 0 && costPortRoyalAll[units[index]] == 0)
                {
                    int europeCost = costEuropeAll[units[index]];
                    int africaCost = costAfricaAll[units[index]];
                    int portRoyalCost = costPortRoyalAll[units[index]];

                    var range = sheet.Ranges[currentRowIndex, columnStartIndex, 1, currentColumnIndex];
                    range.Style.BackColor = Color.FromRgb(211, 211, 211);
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

        private string RemoveFront(string removeString, string sourceString)
        {
            int startIndex = 0;
            int endIndex = removeString.Length;
            return sourceString.Remove(startIndex, endIndex);
        }

    }
}
