using System.Diagnostics;
using WindowsAccessBridgeInterop;

namespace OFIBridgeTest.Tests.NodeExtensions
{
    public static partial class NodeExtensions
    {
        /// <summary>
        /// This one finds a role contains with array
        /// </summary>
        /// <param name="elementName"></param>
        /// <param name="elementContains"></param>
        /// <param name="indexInParent"></param>
        /// <param name="role"></param>
        /// <param name="parent"></param>
        /// <param name="states"></param>
        /// <param name="index"></param>
        /// <param name="timeoutMilliseconds"></param>
        /// <param name="pollingIntervalMilliseconds"></param>
        /// <returns></returns>
        public static AccessibleContextNode FindNodeByRoleContainsArray(string elementName, string elementContains, int indexInParent,
            Role role, AccessibleNode? parent, State[]? states = null, int index = 0, int timeoutMilliseconds = Globals.MaxWaitTime,
            int pollingIntervalMilliseconds = Globals.MinPollingTime)
        {
            DebugOutput($"| FindNodeByRoleContainsArray '{elementName}' | Contains '{elementContains}'");
            parent?.Refresh();
            var stopwatch = Stopwatch.StartNew();
            AccessibleContextNode? node = null;

            while (stopwatch.ElapsedMilliseconds < timeoutMilliseconds)
            {
                var matchCount = 0;
                var depth = 0;
                node = FindNodeByArrayRecursive(elementName, parent, role, indexInParent, index, ref matchCount, depth);

                if (node != null)
                {
                    return node;
                }

                Thread.Sleep(pollingIntervalMilliseconds);
            }

            return null;
        }
    }
}
