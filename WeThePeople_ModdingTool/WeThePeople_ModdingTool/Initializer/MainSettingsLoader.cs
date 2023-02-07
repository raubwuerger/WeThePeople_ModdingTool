using Serilog;
using System.Collections.Generic;
using System.Xml;
using WeThePeople_ModdingTool.Factories;
using WeThePeople_ModdingTool.FileUtilities;
using WeThePeople_ModdingTool.Helper;

namespace WeThePeople_ModdingTool
{
    public sealed class MainSettingsLoader
    {
        private static readonly MainSettingsLoader instance = new MainSettingsLoader();

        static MainSettingsLoader()
        {
        }

        public static MainSettingsLoader Instance
        {
            get
            {
                return instance;
            }
        }

        private string YieldTypesPath = @"templates\Civ4YieldInfos_OnlyTypes.xml";

        private string UnitClassesPath = @"templates\CIV4UnitInfos_OnlyClasses.xml";

        private string HarboursDocumentPath = @"templates\Harbours.xml";

        private string ApplicationConfigBasePath = @".\Configuration\";

        private string MAIN_CONFIG_FILE = "WeThePeople_ModdingTool.xml";
        public bool Init()
        {
            if (false == InitConfig())
            {
                return false;
            }
            return LoadTemplates();
        }

        private bool InitConfig()
        {
            XmlDocument xmlDocument = XMLFileUtility.Load(ApplicationConfigBasePath + MAIN_CONFIG_FILE);
            if (null == xmlDocument)
            {
                Log.Debug("Unable to load main config: " + MAIN_CONFIG_FILE);
                return false;
            }

            XmlNodeList modSourcePath = xmlDocument.DocumentElement.GetElementsByTagName(WeThePeople_ModdingTool_Config.Instance.MOD_SOURCE_PATH_PATH);
            if (modSourcePath.Count != 1)
            {
                return false;
            }

            WeThePeople_ModdingTool_Config.Instance.Mod_path = modSourcePath[0].InnerText;

            return true;
        }

        //TODO: Sollte in List überführt werden. Wenn die Anzahl an templates wächst muss nur ein weiterer Eintrag in die List eingefügt werden.
        public bool LoadTemplates()
        {
            bool loadingTamplatesOk = true;

            DataSetFactory dataSetFactory = new DataSetFactory();

            TemplateRepository.Instance.RegisterTemplate(dataSetFactory.CreateEventInfos_Start());
            TemplateRepository.Instance.RegisterTemplate(dataSetFactory.CreateEventTriggerInfos_Start());
            TemplateRepository.Instance.RegisterTemplate(dataSetFactory.CreateEventTriggerInfos_Done());
            TemplateRepository.Instance.RegisterTemplate(dataSetFactory.CreateEventGameText());
            TemplateRepository.Instance.RegisterTemplate(dataSetFactory.CreateRandomEventStart());
            TemplateRepository.Instance.RegisterTemplate(dataSetFactory.CreateRandomEventDone());

            if (false == InitYieldList())
            {
                loadingTamplatesOk = false;
            }

            if (false == InitUnitClasses())
            {
                loadingTamplatesOk = false;
            }

            if (false == InitHarbours())
            {
                loadingTamplatesOk = false;
            }

            return loadingTamplatesOk;
        }

        private bool InitYieldList()
        {
            XmlDocument yieldTypesDocument = XMLFileUtility.Load(PathHelper.GetBasePathCombine(YieldTypesPath));
            if (null == yieldTypesDocument)
            {
                return false;
            }

            List<string> yieldTypes = new List<string>();
            foreach (XmlNode node in yieldTypesDocument.DocumentElement.ChildNodes)
            {
                yieldTypes.Add(node.InnerText);
            }

            yieldTypes.Sort((x, y) => x.ToString().CompareTo(y.ToString()));

            int length_PREFIX_YIELD = CommonVariables.PREFIX_YIELD.Length;
            YieldTypeRepository.Instance.YieldTypes = CreatedDictionary(yieldTypes, length_PREFIX_YIELD);
            YieldTypeRepository.Instance.YieldTypeNames = DictionaryHelper.GetKeys(YieldTypeRepository.Instance.YieldTypes);
            return YieldTypeRepository.Instance.YieldTypes.Count > 0;
        }

        private IDictionary<string, string> CreatedDictionary(List<string> list, int substringIndex)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            foreach (string item in list)
            {
                string name = item.Substring(substringIndex);
                dictionary.TryAdd(name, item);
            }
            return dictionary;
        }

        private bool InitUnitClasses()
        {
            XmlDocument unitClassesDocument = XMLFileUtility.Load(PathHelper.GetBasePathCombine(UnitClassesPath));
            if (null == unitClassesDocument)
            {
                return false;
            }

            List<string> unitClasses = new List<string>();
            foreach (XmlNode node in unitClassesDocument.DocumentElement.ChildNodes)
            {
                unitClasses.Add(node.InnerText);
            }

            unitClasses.Sort((x, y) => x.ToString().CompareTo(y.ToString()));

            int length_PREFIX_UNITCLASS = CommonVariables.PREFIX_UNITCLASS.Length;
            UnitClassRepository.Instance.UnitClasses = CreatedDictionary(unitClasses, length_PREFIX_UNITCLASS);
            UnitClassRepository.Instance.UnitClassNames = DictionaryHelper.GetKeys(UnitClassRepository.Instance.UnitClasses);
            return UnitClassRepository.Instance.UnitClasses.Count > 0;
        }

        private bool InitHarbours()
        {
            XmlDocument harboursDocument = XMLFileUtility.Load(PathHelper.GetBasePathCombine(HarboursDocumentPath));
            if (null == harboursDocument)
            {
                return false;
            }

            List<string> harbours = new List<string>();
            foreach (XmlNode node in harboursDocument.DocumentElement.ChildNodes)
            {
                harbours.Add(node.InnerText);
            }
            HarbourRepository.Instance.Harbours = harbours;
            return HarbourRepository.Instance.Harbours.Count > 0;
        }
    }
}
