using System.Diagnostics;
using Tests.Utilities;
using WindowsAccessBridgeInterop;

namespace OFIBridgeTest.Tests.NodeExtensions
{
    public static partial class NodeExtensions
    {
        public static AccessibleContextNode FindInternalFrame(string elementName, AccessibleNode? parent, State[]? states = null, int index = 0,
                    int timeoutMilliseconds = Globals.MaxWaitTime, int pollingIntervalMilliseconds = Globals.MinPollingTime)
        {
            DebugOutput($"| FindInternalFrame '{elementName}'");
            parent?.Refresh();
            var stopwatch = Stopwatch.StartNew();
            AccessibleContextNode? internalFrame = null;

            while (stopwatch.ElapsedMilliseconds < 15000)
            {
                var matchCount = 0;
                var depth = 0;
                internalFrame = FindInternalFrameRecursive(elementName, parent, 0, ref matchCount, depth);

                if (internalFrame != null)
                {
                    return internalFrame;
                }

                Thread.Sleep(pollingIntervalMilliseconds);
            }
            throw new NodeNotFoundException($"FindInternalFrame - Node with role InternalFrame and name starting with '{elementName}' at index {index} not found within {timeoutMilliseconds} milliseconds.");
        }
    }
}