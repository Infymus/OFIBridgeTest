using TestHelpers.KeyBoardMouse;
using WindowsAccessBridgeInterop;

namespace OFIBridgeTest.Tests.NodeExtensions
{
    public static partial class NodeExtensions
    {
        /// <summary>
        /// Cleaner method for Click Array Contains.
        /// </summary>
        /// <param name="elementName"></param>
        /// <param name="nodeContains"></param>
        /// <param name="parent"></param>
        /// <param name="role"></param>
        /// <param name="indexInParent"></param>
        /// <param name="states"></param>
        /// <param name="index"></param>
        /// <param name="timeoutMilliseconds"></param>
        /// <param name="pollingIntervalMilliseconds"></param>
        /// <returns></returns>
        public static bool ClickArrayContains(string elementName, string nodeContains, AccessibleNode? parent, Role role, int indexInParent,
            State[]? states = null, int index = 0, int timeoutMilliseconds = Globals.MaxWaitTime, int pollingIntervalMilliseconds = Globals.MinPollingTime)
        {
            DebugOutput($"ClickArrayContains: '{elementName}' | index = '{indexInParent}'");

            var nodeFound = FindNodeByRoleContainsArray(elementName, nodeContains, indexInParent, role, parent);
            if (nodeFound == null) return false;

            DebugOutput($"| Found '{elementName}'");

            // Get coordinates
            var rect = GetNodeRect(nodeFound);
            if (rect == null) return false;

            // Click the coordinates
            MouseHelper.Click(rect, ClickPoint.Center);

            return true;
        }
    }
}