using WindowsAccessBridgeInterop;

namespace OFIBridgeTest.Tests.NodeExtensions
{
    public static partial class NodeExtensions
    {
        /// <summary>
        /// Allows you to pass a node you want to click on with a range index 
        /// 
        /// NOTE: starts from TOP and goes to BOTTOM.
        /// </summary>
        /// <param name="elementName"></param>
        /// <param name="parent"></param>
        /// <param name="role"></param>
        /// <param name="indexStart"></param>
        /// <param name="indexEnd"></param>
        /// <param name="states"></param>
        /// <param name="index"></param>
        /// <param name="timeoutMilliseconds"></param>
        /// <param name="pollingIntervalMilliseconds"></param>
        /// <returns></returns>
        public static bool ClickArrayMultiIndex(string elementName, AccessibleNode? parent, Role role,
            int indexStart, int indexEnd, State[]? states = null,
            int index = 0, int timeoutMilliseconds = Globals.MaxWaitTime, int pollingIntervalMilliseconds = Globals.MinPollingTime)
        {
            DebugOutput($"ClickArrayMultiIndex: '{elementName}' | start/end index = '{indexStart}/{indexEnd}'");

            for (int countIdx = indexStart; countIdx >= indexEnd; countIdx--)
            {
                var nodeExists = CheckAppStateByArray(elementName, "", countIdx, parent, role);
                if (nodeExists)
                {
                    ClickArray(elementName, parent, role, countIdx);
                    return true;
                }
            }
            DebugOutput($"--Not Found");
            return false;
        }
    }
}