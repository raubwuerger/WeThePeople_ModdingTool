using System;
using System.Collections.Generic;
using System.Text;
using WeThePeople_ModdingTool.Creators;
using WeThePeople_ModdingTool.DataSets;
using WeThePeople_ModdingTool.Validators;

namespace WeThePeople_ModdingTool.Helper
{
    public class EventCreatorHelper
    {
        public static bool IsValid( EventCreatorBase eventCreator )
        {
            if (true == StringValidator.IsNullOrWhiteSpace(eventCreator.Harbour))
            {
                return false;
            }

            if (true == StringValidator.IsNullOrWhiteSpace(eventCreator.YieldType))
            {
                return false;
            }

            if (true == StringValidator.IsNullOrWhiteSpace(eventCreator.SavePath))
            {
                return false;
            }

            return true;
        }
    }
}
