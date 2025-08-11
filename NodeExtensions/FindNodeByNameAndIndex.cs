using System.Diagnostics;
using Tests.Utilities;
using WindowsAccessBridgeInterop;

namespace OFIBridgeTest.Tests.NodeExtensions
{
    public static partial class NodeExtensions
    {
        public static AccessibleContextNode FindNodeByNameAndIndex(string elementName, int indexInParent, Role role, AccessibleNode? parent,
              State[]? states = null, int index = 0, int timeoutMilliseconds = Globals.MaxWaitTime, int pollingIntervalMilliseconds = Globals.MinPollingTime)
        {
            DebugOutput($"| FindNodeByNameAndIndex '{elementName}' | Index = {indexInParent}");

            parent?.Refresh();
            var stopwatch = Stopwatch.StartNew();
            AccessibleContextNode? node = null;

            while (stopwatch.ElapsedMilliseconds < timeoutMilliseconds)
            {
                var matchCount = 0;
                var depth = 0;
                node = FindNodeByNameAndIndexRecursive(elementName, parent, role, indexInParent, index, ref matchCount, depth);

                if (node != null)
                {
                    return node;
                }

                Thread.Sleep(pollingIntervalMilliseconds);
            }
               throw new NodeNotFoundException($"FindNodeByArray - Node with role '{role}' and name containing '{elementName}' at indexInParent {indexInParent} not found within {timeoutMilliseconds} milliseconds.");

        }
    }
    

}