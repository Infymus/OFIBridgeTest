using TestHelpers.KeyBoardMouse;
using WindowsAccessBridgeInterop;

namespace OFIBridgeTest.Tests.NodeExtensions
{
    public static partial class NodeExtensions
    {
        /// <summary>
        /// Clicks a node Center
        /// </summary>
        /// <param name="findNodeName"></param>
        /// <param name="parent"></param>
        /// <param name="role"></param>
        /// <param name="findGroupName"></param>
        /// <param name="states"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static bool Click(string findNodeName, AccessibleNode? parent, Role role, string findGroupName = null,
            State[]? states = null, int index = 0)
        {
            DebugOutput($"Click: '{findNodeName}'");
            var OracleClickArea = FindNodeByRole(findNodeName, role, parent, states, index);
            if (OracleClickArea == null) return false;
            DebugOutput($"| Found '{findNodeName}'");
            // Get coordinates
            var rect = GetNodeRect(OracleClickArea);
            if (rect == null) return false;

            // Click the coordinates
            MouseHelper.Click(rect, ClickPoint.Center);

            return true;
        }
    }
}