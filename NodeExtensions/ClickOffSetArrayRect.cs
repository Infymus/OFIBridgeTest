using TestHelpers.KeyBoardMouse;
using WindowsAccessBridgeInterop;

namespace OFIBridgeTest.Tests.NodeExtensions
{
    public static partial class NodeExtensions
    {
        /// <summary>
        /// When you can't click a node because it doesn't exist (say it's a vTEXT) - OR - there are multiple
        /// copies of the same node element name, then this is a way to get to a button/text/etc by finding a node
        /// you can get to - but then clicking at +/- X,Y,W,H to get there. 
        /// </summary>
        /// <param name="elementName"></param>
        /// <param name="parent"></param>
        /// <param name="role"></param>
        /// <param name="indexInParent"></param>
        /// <param name="clickPoint"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="states"></param>
        /// <param name="index"></param>
        /// <param name="timeoutMilliseconds"></param>
        /// <param name="pollingIntervalMilliseconds"></param>
        /// <returns></returns>
        public static bool ClickOffSetArrayRect(string elementName, AccessibleNode? parent, Role role, int indexInParent, ClickPoint clickPoint,
            int x, int y, int width, int height, State[]? states = null, int index = 0, int timeoutMilliseconds = Globals.MaxWaitTime,
            int pollingIntervalMilliseconds = Globals.MinPollingTime)
        {
            DebugOutput($"ClickOffSetRectArray: '{elementName}' | index = '{indexInParent}' | offset = '{clickPoint}'");
            var nodeFound = FindNodeByArray(elementName, parent, role, indexInParent);

            if (nodeFound == null) // we can try to find it a different way
                nodeFound = FindNodeByRoleContains(elementName, "", role, parent, states, index, timeoutMilliseconds, pollingIntervalMilliseconds);

            if (nodeFound == null) return false;
            DebugOutput($"| Found : '{elementName}' | index = '{indexInParent}'");

            var rect = GetNodeRect(nodeFound);
            if (rect == null) return false;

            rect.X += x;
            rect.Y += y;
            rect.Height += height;
            rect.Width += width;

            MouseHelper.Click(rect, ClickPoint.Offset);

            return true;
        }
    }
}