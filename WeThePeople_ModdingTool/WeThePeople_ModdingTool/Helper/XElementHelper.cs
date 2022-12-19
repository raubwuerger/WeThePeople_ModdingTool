using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace WeThePeople_ModdingTool.Helper
{
    class XElementHelper
    {
        public static XElement GetIfOnlyOneExists(XElement xElement, String element)
        {
            if (null == xElement)
            {
                Log.Verbose("Parameter <xElement> is null!");
                return null;
            }
            if (true == String.IsNullOrWhiteSpace(element))
            {
                Log.Verbose("Parameter <element> is null!");
                return null;
            }
            var var = xElement.Elements(element);
            if (var.Count() == 0)
            {
                Log.Verbose("Node <" + element + "> count invalid! => 0");
                return null;
            }
            if (var.Count() > 1)
            {
                Log.Verbose("Node <" + element + "> count invalid! => " + var.Count());
                return null;
            }

            return var.First();
        }

        public static XElement FindByExternalId(IEnumerable<XElement> elements, string externalId)
        {
            if (null == elements)
            {
                Log.Verbose("Parameter <elements> is null!");
                return null;
            }

            if (true == String.IsNullOrWhiteSpace(externalId))
            {
                Log.Verbose("Parameter <externalId> is null!");
                return null;
            }

            foreach (XElement element in elements)
            {
                XAttribute found = element.Attribute(ConstantsXMLAttribute.EXTERNAL_ID);
                if (null == found)
                {
                    continue;
                }

                if (false == externalId.Equals(found.Value.ToString()))
                {
                    continue;
                }

                return element;
            }
            return null;
        }

        public static String FindAttributeValue(XElement xElement, String attributeName)
        {
            if (null == xElement)
            {
                Log.Verbose("Parameter <xElement> is null!");
                return null;
            }

            if (true == String.IsNullOrWhiteSpace(attributeName))
            {
                Log.Verbose("Parameter <attributeName> is invalid!");
                return null;
            }

            XAttribute xAttribute = xElement.Attribute(attributeName);
            if (null == xAttribute)
            {
                Log.Verbose("XML attribute with name <" + attributeName + "> not found!");
                return null;
            }

            return xAttribute.Value;
        }

        public static bool IsEqualByExternalId(XElement xElement_A, XElement xElement_B)
        {
            if (null == xElement_A && null == xElement_B)
            {
                return true;
            }

            if (null == xElement_A || null == xElement_B)
            {
                return false;
            }

            XAttribute externalId_A = xElement_A.Attribute(ConstantsXMLAttribute.EXTERNAL_ID);
            XAttribute externalId_B = xElement_B.Attribute(ConstantsXMLAttribute.EXTERNAL_ID);

            if (null == externalId_A && null == externalId_B)
            {
                IEnumerable<XAttribute> xAttributes_A = xElement_A.Attributes();
                IEnumerable<XAttribute> xAttributes_B = xElement_B.Attributes();
                if (xAttributes_A.Count() != 0 || xAttributes_B.Count() != 0)
                {
                    return false;
                }

                return true;
            }

            if (null == externalId_A || null == externalId_B)
            {
                return false;
            }

            if (externalId_A.Value != externalId_B.Value)
            {
                return false;
            }

            return externalId_A.Value.Equals(externalId_B.Value);
        }
    }
}
