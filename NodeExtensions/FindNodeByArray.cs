using System.Diagnostics;
using Tests.Utilities;
using WindowsAccessBridgeInterop;

namespace OFIBridgeTest.Tests.NodeExtensions
{
    public static partial class NodeExtensions
    {
        public static AccessibleContextNode FindNodeByArray(string elementName, AccessibleNode? parent, Role role, int indexInParent, State[]? states = null, int index = 0,
                    int timeoutMilliseconds = Globals.MaxWaitTime, int pollingIntervalMilliseconds = Globals.MinPollingTime)
        {
            DebugOutput($"| FindNodeByArray '{elementName}'");
            parent?.Refresh();
            var stopwatch = Stopwatch.StartNew();
            AccessibleContextNode? nodeFound = null;

            while (stopwatch.ElapsedMilliseconds < 15000)
            {
                var matchCount = 0;
                var depth = 0;
                nodeFound = FindNodeByArrayRecursive(elementName, parent, role, indexInParent, 0, ref matchCount, depth);

                if (nodeFound != null)
                {
                    DebugOutput($"| Found '{elementName}'");
                    return nodeFound;
                }

                Thread.Sleep(pollingIntervalMilliseconds);
            }
            throw new NodeNotFoundException($"FindNodeByArray - Node with role '{role}' and name containing '{elementName}' at indexInParent {indexInParent} not found within {timeoutMilliseconds} milliseconds.");
        }
    }
}