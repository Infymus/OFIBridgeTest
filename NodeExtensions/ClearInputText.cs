using TestHelpers.KeyBoardMouse;
using Tests.Utilities;
using WindowsAccessBridgeInterop;

namespace OFIBridgeTest.Tests.NodeExtensions
{
    public static partial class NodeExtensions
    {
        /// <summary>
        /// Clears Input text out of an edit box
        /// </summary>
        /// <param name="elementName"></param>
        /// <param name="parent"></param>
        /// <param name="role"></param>
        /// <param name="indexInParent"></param>
        /// <param name="states"></param>
        /// <param name="index"></param>
        /// <param name="timeoutMilliseconds"></param>
        /// <param name="pollingIntervalMilliseconds"></param>
        /// <returns></returns>
        public static bool ClearInputText(string findNodeName, string nodeContains, int indexInParent,
            AccessibleNode? parent, Role role, State[]? states = null, int index = 0)
        {
            DebugOutput($"ClearInputText: '{findNodeName}' | Contains = '{nodeContains}' | Index = '{indexInParent}'");
            var nodeFound = FindNodeByRoleContainsArray(findNodeName, nodeContains, indexInParent, role, parent);
            if (nodeFound == null) return false;

            DebugOutput($"| Found : '{findNodeName}' | index = '{indexInParent}'");

            // Get coordinates
            var rect = GetNodeRect(nodeFound);
            if (rect == null) return false;

            // Click the coordinates
            MouseHelper.Click(rect, ClickPoint.Center);
            OracleUtilities.Sleep(50);

            // Click the coordinates
            KeyboardHelper.ClearInputText(10);

            return true;
        }
    }
}