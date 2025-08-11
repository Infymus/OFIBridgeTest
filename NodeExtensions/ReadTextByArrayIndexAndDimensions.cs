using Tests.Utilities;
using WindowsAccessBridgeInterop;

namespace OFIBridgeTest.Tests.NodeExtensions
{
    public static partial class NodeExtensions
    {
        public static string ReadTextByArrayIndexAndDimensions(string elementName, string elementContains, AccessibleNode? parent, Role role, int indexInParent,
            int nHeight, int nWidth, string findChildNodelName = "Sentence", AccessibleTextEnums inAccessibleText = AccessibleTextEnums.AtCaret,
            State[]? states = null, int index = 0, int timeoutMilliseconds = Globals.MaxWaitTime, int pollingIntervalMilliseconds = Globals.MinPollingTime)
        {
            DebugOutput($"ReadTextByArrayIndexAndDimensions: '{elementName}' | index = '{indexInParent}' | role = '{role}' | Height/Width = '{nHeight}/{nWidth}'");
            string returnText = null;
            string findGroupNameText = OracleUtilities.GetEnumDescription(inAccessibleText);

            var nodeFound = FindNodeByArrayIndexAndDimensions(elementName, elementContains, indexInParent, nHeight, nWidth, role, parent);

            if (nodeFound == null) return null;

            PropertyList pl = nodeFound.GetProperties(PropertyOptions.AccessibleText);
            PropertyGroup propertyGroup = pl[0] as PropertyGroup;
            var propGroupChildren = propertyGroup.Children;

            var desiredPropertyGroup = propGroupChildren
                .OfType<PropertyGroup>()
                .FirstOrDefault(pg => pg.Name == findGroupNameText);

            if (desiredPropertyGroup != null)
            {
                var childNodeGroup = desiredPropertyGroup.Children;
                if (childNodeGroup.Count != 0)
                {
                    var value = childNodeGroup
                            .Where(p => p.Name.ToUpper() == findChildNodelName.ToUpper())
                            .Select(p => p.Value)
                            .FirstOrDefault();

                    if (value != null)
                    {
                        returnText = value.ToString();
                        DebugOutput($"| ReadTextByArray Found : '{returnText}' | index = '{indexInParent}'");
                    }
                }
            }
            else
            {
                var value = propGroupChildren
                        .Where(p => p.Name.ToUpper() == findChildNodelName.ToUpper())
                        .Select(p => p.Value)
                        .FirstOrDefault();

                if (value != null)
                {
                    returnText = value.ToString();
                    DebugOutput($"| Found : '{returnText}' | index = '{indexInParent}'");
                }
            }
            return returnText;
        }
    }
}