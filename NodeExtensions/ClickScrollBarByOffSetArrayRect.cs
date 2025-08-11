using TestHelpers.KeyBoardMouse;
using WindowsAccessBridgeInterop;

namespace OFIBridgeTest.Tests.NodeExtensions
{
    public static partial class NodeExtensions
    {
        /// <summary>
        /// This allows you to click a scroll bar by an offset RECT array.
        /// You must include the X and the Y location to distinguish between multiple scroll bars.
        /// NOTE: Scroll bar locations change EVERY TIME you open an Oracle Form. So to get around this
        ///       you have to find a button or something around the scroll bar, and then use this method to 
        ///       find that RECT in a +/- X and a +/- Y (EG: Button1 is at 30,40 but Scrollbar LEFT is at 20,10 so 
        ///       you minus off from 30,40 to get there)
        /// </summary>
        /// <param name="elementName"></param>
        /// <param name="parent"></param>
        /// <param name="role"></param>
        /// <param name="indexInParent"></param>
        /// <param name="offSet"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="states"></param>
        /// <param name="index"></param>
        /// <param name="timeoutMilliseconds"></param>
        /// <param name="pollingIntervalMilliseconds"></param>
        /// <returns></returns>
        public static bool ClickScrollBarByOffSetArrayRect(string elementName, AccessibleNode? parent, Role role, int indexInParent, ClickPoint offSet,
         int NumClicks, int x, int y, int width, int height, State[]? states = null, int index = 0, int timeoutMilliseconds = Globals.MaxWaitTime,
         int pollingIntervalMilliseconds = Globals.MinPollingTime)
        {
            DebugOutput($"ClickScrollBarByOffSetArrayRect: '{elementName}' | index = '{indexInParent}' | offset = '{offSet}'");
            var nodeFound = FindNodeByArray(elementName, parent, role, indexInParent);
            if (nodeFound == null)
                nodeFound = FindNodeByRoleContains(elementName, "", role, parent, states, index, timeoutMilliseconds, pollingIntervalMilliseconds);
            if (nodeFound == null) return false;
            DebugOutput($"| Found : '{elementName}' | index = '{indexInParent}'");
            var rect = GetNodeRect(nodeFound);
            if (rect == null) return false;
            rect.X += x;
            rect.Y += y;
            rect.Height += height;
            rect.Width += width;
            for (int i = 0; i < NumClicks; i++)
            {
                MouseHelper.Click(rect, offSet, true);
            }

            return true;
        }
    }
}