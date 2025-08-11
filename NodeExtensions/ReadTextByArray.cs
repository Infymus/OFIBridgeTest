using Tests.Utilities;
using WindowsAccessBridgeInterop;

namespace OFIBridgeTest.Tests.NodeExtensions
{
    public static partial class NodeExtensions
    {
        /// <summary>
        /// Reads text in an array at a node by name, role, index, Group Name and Value
        /// </summary>
        /// <param name="elementName"></param>
        /// <param name="parent"></param>
        /// <param name="role"></param>
        /// <param name="indexInParent"></param>
        /// <param name="findChildNodelName"></param>
        /// <param name="inAccessibleText"></param>
        /// <param name="states"></param>
        /// <param name="index"></param>
        /// <param name="timeoutMilliseconds"></param>
        /// <param name="pollingIntervalMilliseconds"></param>
        /// <returns></returns>
        public static string ReadTextByArray(string elementName, AccessibleNode? parent, Role role, int indexInParent, string findChildNodelName = "Sentence",
            AccessibleTextEnums inAccessibleText = AccessibleTextEnums.AtCaret, State[]? states = null, int index = 0,
            int timeoutMilliseconds = Globals.MaxWaitTime, int pollingIntervalMilliseconds = Globals.MinPollingTime)
        {
            DebugOutput($"ReadTextByArray: '{elementName}' | index = '{indexInParent}' | role = '{role}'");
            string returnText = null;
            string findGroupNameText = OracleUtilities.GetEnumDescription(inAccessibleText);

            var nodeFound = FindNodeByArray(elementName, parent, role, indexInParent);
            if (nodeFound == null) return null;

            PropertyList pl = nodeFound.GetProperties(PropertyOptions.AccessibleText);
            PropertyGroup propertyGroup = pl[0] as PropertyGroup;
            var propGroupChildren = propertyGroup.Children;

            var desiredPropertyGroup = propGroupChildren
                .OfType<PropertyGroup>() // Ensure the elements are of type PropertyGroup
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