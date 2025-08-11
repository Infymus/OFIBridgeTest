using TestHelpers.KeyBoardMouse;
using WindowsAccessBridgeInterop;

namespace OFIBridgeTest.Tests.NodeExtensions
{
    public static partial class NodeExtensions
    {
        /// <summary>
        /// Clicks an array at a node by name, role and index
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
        public static bool ClickArray(string elementName, AccessibleNode? parent, Role role, int indexInParent, State[]? states = null,
            int index = 0, int timeoutMilliseconds = Globals.MaxWaitTime, int pollingIntervalMilliseconds = Globals.MinPollingTime)
        {
            DebugOutput($"ClickArray: '{elementName}' | index = '{indexInParent}'");
            var nodeFound = FindNodeByArray(elementName, parent, role, indexInParent);
            if (nodeFound == null) return false;

            // Get coordinates
            var rect = GetNodeRect(nodeFound);
            if (rect == null) return false;

            // Click the coordinates
            MouseHelper.Click(rect, ClickPoint.Center);

            return true;
        }
    }
}