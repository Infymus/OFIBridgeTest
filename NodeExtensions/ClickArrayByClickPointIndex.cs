using TestHelpers.KeyBoardMouse;
using Tests.Utilities;
using WindowsAccessBridgeInterop;

namespace OFIBridgeTest.Tests.NodeExtensions
{
    public static partial class NodeExtensions
    {
        /// <summary>
        /// Allows clicking at a specific element, parent, role, index and ClickPoint offset
        /// </summary>
        /// <param name="elementName"></param>
        /// <param name="parent"></param>
        /// <param name="role"></param>
        /// <param name="indexInParent"></param>
        /// <param name="clickPoint"></param>
        /// <param name="states"></param>
        /// <param name="index"></param>
        /// <param name="timeoutMilliseconds"></param>
        /// <param name="pollingIntervalMilliseconds"></param>
        /// <returns></returns>
        public static bool ClickArrayByClickPointIndex(string elementName, AccessibleNode? parent, Role role, int indexInParent, ClickPoint clickPoint,
           State[]? states = null, int index = 0, int timeoutMilliseconds = Globals.MaxWaitTime, int pollingIntervalMilliseconds = Globals.MinPollingTime)
        {

            DebugOutput($"ClickArrayByClickPointIndex: '{elementName}' | index = '{indexInParent}'");
            var nodeFound = FindNodeByNameAndIndex(elementName, indexInParent, role, parent);

            if (nodeFound == null) return false;

            var info = nodeFound.GetInfo();
            DebugOutput($"| Found : '{elementName}' | index = '{indexInParent}' | x = {info.x} / y = {info.y} / w = {info.width} / h = {info.height}");

            // Get coordinates
            var rect = GetNodeRect(nodeFound);
            if (rect == null) return false;

            // Click the coordinates
            MouseHelper.Click(rect, ClickPoint.Center);
            OracleUtilities.Sleep(250);
            MouseHelper.Click(rect, clickPoint);

            return false;
        }
    }
}