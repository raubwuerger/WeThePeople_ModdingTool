using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;
using WeThePeople_ModdingTool.DataSets;
using WeThePeople_ModdingTool.Factories;
using WeThePeople_ModdingTool.FileUtilities;

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

        public bool Init()
        {
            return LoadTemplates();
        }
        //TODO: Sollte in List überführt werden. Wenn die Anzahl an templates wächst muss nur ein weiterer Eintrag in die List eingefügt werden.
        public bool LoadTemplates()
        {
            bool loadingTamplatesOk = true;

            DataSetFactory dataSetFactory = new DataSetFactory();

            TemplateRepository.Instance.RegisterTemplate(dataSetFactory.CreateEventInfos_Start() );
            TemplateRepository.Instance.RegisterTemplate(dataSetFactory.CreateEventTriggerInfos_Start());
            TemplateRepository.Instance.RegisterTemplate(dataSetFactory.CreateEventTriggerInfos_Done());
            TemplateRepository.Instance.RegisterTemplate(dataSetFactory.CreateEventGameText());
            TemplateRepository.Instance.RegisterTemplate(dataSetFactory.CreateRandomEventStart());
            TemplateRepository.Instance.RegisterTemplate(dataSetFactory.CreateRandomEventDone());

            if ( false == InitYieldList() )
            {
                loadingTamplatesOk = false;
            }

            if( false == InitUnitClasses() )
            {
                loadingTamplatesOk = false;
            }

            if( false == InitHarbours() )
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

            YieldTypeRepository.Instance.YieldTypes = yieldTypes;
            return YieldTypeRepository.Instance.YieldTypes.Count > 0;
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

            UnitClassRepository.Instance.UnitClasses = unitClasses;
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
