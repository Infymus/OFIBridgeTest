using TestHelpers.KeyBoardMouse;
using WindowsAccessBridgeInterop;

namespace OFIBridgeTest.Tests.NodeExtensions
{
    public static partial class NodeExtensions
    {
        /// <summary>
        /// Clicks a Scroll Bar. Only works if there is ONE scroll bar on the form. 
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="role"></param>
        /// <param name="inState"></param>
        /// <param name="NumClicks"></param>
        /// <param name="offSet"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="index"></param>
        /// <param name="timeoutMilliseconds"></param>
        /// <param name="pollingIntervalMilliseconds"></param>
        /// <returns></returns>
        public static bool ClickScrollBar(AccessibleNode? parent, Role role, State inState, int NumClicks, ClickPoint offSet, int x = 0, int y = 0,
             int index = 0, int timeoutMilliseconds = Globals.MaxWaitTime, int pollingIntervalMilliseconds = Globals.MinPollingTime)
        {
            DebugOutput($"ClickScrollBar: NumClicks = '{NumClicks}' | ScrollBarType = '{inState}'");
            // Add More states we need beyond Vertical and Horizontal
            State[] states = new[] { State.Enabled, State.Showing, State.Visible, inState };
            var scrollBar = FindScrollBar("", Role.ScrollBar, parent, x, y, states);
            if (scrollBar == null) return false;
            DebugOutput($"| Found 'ScrollBar'");
            var rect = GetNodeRect(scrollBar);
            if (rect == null) return false;
            DebugOutput($"| Scroll Click @ X = {rect.X} | Y = {rect.Y} | ClickPoint = {offSet}");
            for (int i = 0; i < NumClicks; i++)
            {
                MouseHelper.Click(rect, offSet, true);
            }
            return true;
        }
    }
}