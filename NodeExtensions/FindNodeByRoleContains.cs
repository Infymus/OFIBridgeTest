using System.Diagnostics;
using WindowsAccessBridgeInterop;

namespace OFIBridgeTest.Tests.NodeExtensions
{
    public static partial class NodeExtensions
    {
        /// <summary>
        /// Finds a Node by Element Name and if it Contains Text
        /// Does NOT error out if it is not found. 
        /// </summary>
        /// <param name="elementName"></param>
        /// <param name="elementContains"></param>
        /// <param name="role"></param>
        /// <param name="parent"></param>
        /// <param name="states"></param>
        /// <param name="index"></param>
        /// <param name="timeoutMilliseconds"></param>
        /// <param name="pollingIntervalMilliseconds"></param>
        /// <returns></returns>
        public static AccessibleContextNode FindNodeByRoleContains(string elementName, string elementContains, Role role,
            AccessibleNode? parent, State[]? states = null, int index = 0, int timeoutMilliseconds = Globals.MaxWaitTime, int pollingIntervalMilliseconds = Globals.MinPollingTime)
        {
            DebugOutput($"| FindNodeByRoleContains '{elementName}' | Contains '{elementContains}'");

            parent?.Refresh();
            var stopwatch = Stopwatch.StartNew();
            AccessibleContextNode? node = null;

            while (stopwatch.ElapsedMilliseconds < timeoutMilliseconds)
            {
                var matchCount = 0;
                var depth = 0;
                node = FindNodeByRoleRecursiveContains(elementName, elementContains, role, parent, states, index, ref matchCount, depth);

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