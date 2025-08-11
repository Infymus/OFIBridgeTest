using Tests.Utilities;
using WindowsAccessBridgeInterop;

namespace OFIBridgeTest.Tests.NodeExtensions
{
    public static partial class NodeExtensions
    {
        /// <summary>
        /// Reads the Text of an Oracle Field by Node Name and Value
        /// </summary>
        /// <param name="findNodeName"></param>
        /// <param name="parent"></param>
        /// <param name="findNodeValue"></param>
        /// <param name="inAccessibleText"></param>
        /// <returns></returns>
        public static string ReadText(string findNodeName, AccessibleNode? parent, string findNodeValue = "Sentence",
            AccessibleTextEnums inAccessibleText = AccessibleTextEnums.AtCaret, State[]? states = null, int index = 0)
        {
            DebugOutput($"ReadText: '{findNodeName}' | value = '{findNodeValue}'");
            string returnText = null;
            // Usually we want the Text Attributes at Caret - But we can override it
            string findGroupNameText = OracleUtilities.GetEnumDescription(inAccessibleText);

            var ReadOracleEditBox = FindNodeByRole(findNodeName, Role.Text, parent, states, index);
            if (ReadOracleEditBox == null) return "";

            // Pull the Property Group List, Property Group and Children
            PropertyList pl = ReadOracleEditBox.GetProperties(PropertyOptions.AccessibleText);
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
                            .Where(p => p.Name == findNodeValue)
                            .Select(p => p.Value)
                            .FirstOrDefault();

                    if (value != null)
                    {
                        DebugOutput($"| Found '{value.ToString()}'");
                        returnText = value.ToString();
                    }
                }
            }
            else
            {
                var value = propGroupChildren
                        .Where(p => p.Name == findNodeValue)
                        .Select(p => p.Value)
                        .FirstOrDefault();

                if (value != null)
                {
                    DebugOutput($"| Found '{value.ToString()}'");

                    returnText = value.ToString();
                }
            }

            return returnText;
        }
    }
}