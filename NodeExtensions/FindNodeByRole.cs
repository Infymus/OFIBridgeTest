using System.Diagnostics;
using Tests.Utilities;
using WindowsAccessBridgeInterop;

namespace OFIBridgeTest.Tests.NodeExtensions
{
    public static partial class NodeExtensions
    {
        /// <summary>
        /// Finds a Node by a Role and returns the node
        /// </summary>
        /// <param name="elementName"></param>
        /// <param name="role"></param>
        /// <param name="parent"></param>
        /// <param name="states"></param>
        /// <param name="index"></param>
        /// <param name="timeoutMilliseconds"></param>
        /// <param name="pollingIntervalMilliseconds"></param>
        /// <returns></returns>
        /// <exception cref="NodeNotFoundException"></exception>
        public static AccessibleContextNode FindNodeByRole(string elementName, Role role, AccessibleNode? parent,
            State[]? states = null, int index = 0, int timeoutMilliseconds = Globals.MaxWaitTime, int pollingIntervalMilliseconds = Globals.MinPollingTime)
        {
            DebugOutput($"| FindNodeByRole '{elementName}'");
            parent?.Refresh();
            var stopwatch = Stopwatch.StartNew();
            AccessibleContextNode? node = null;

            while (stopwatch.ElapsedMilliseconds < timeoutMilliseconds)
            {
                var matchCount = 0;
                var depth = 0;
                node = FindNodeByRoleRecursive(elementName, role, parent, states, index, ref matchCount, depth);

                if (node != null)
                {
                    return node;
                }

                Thread.Sleep(pollingIntervalMilliseconds);
            }

            throw new NodeNotFoundException($"Node with role '{role}' and name starting with '{elementName}' at index {index} not found within {timeoutMilliseconds} milliseconds.");
        }
    }
}