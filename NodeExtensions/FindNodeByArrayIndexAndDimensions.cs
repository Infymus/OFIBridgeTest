using System.Diagnostics;
using WindowsAccessBridgeInterop;

namespace OFIBridgeTest.Tests.NodeExtensions
{
    public static partial class NodeExtensions
    {
        /// <summary>
        /// Finds a node by index, contains, and by height and width.
        /// Since there are many forms that have the same name, this allows finding it specifically by height and width.
        /// </summary>
        /// <param name="elementName"></param>
        /// <param name="elementContains"></param>
        /// <param name="indexInParent"></param>
        /// <param name="nWidth"></param>
        /// <param name="nHeight"></param>
        /// <param name="role"></param>
        /// <param name="parent"></param>
        /// <param name="states"></param>
        /// <param name="index"></param>
        /// <param name="timeoutMilliseconds"></param>
        /// <param name="pollingIntervalMilliseconds"></param>
        /// <returns></returns>
        public static AccessibleContextNode FindNodeByArrayIndexAndDimensions(string elementName, string elementContains, int indexInParent,
            int nHeight, int nWidth, Role role, AccessibleNode? parent, State[]? states = null, int index = 0,
            int timeoutMilliseconds = Globals.MaxWaitTime, int pollingIntervalMilliseconds = Globals.MinPollingTime)
        {
            DebugOutput($"| FindNodeByArrayIndexAndDimensions '{elementName}' | Contains '{elementContains}' | Index = '{indexInParent}' | Height/Width = '{nHeight}/'{nWidth}'");
            parent?.Refresh();
            var stopwatch = Stopwatch.StartNew();
            AccessibleContextNode? node = null;

            while (stopwatch.ElapsedMilliseconds < timeoutMilliseconds)
            {
                var matchCount = 0;
                var depth = 0;
                node = FindNodeByArrayRecursiveByIndexAndDimensions(elementName, parent, role, indexInParent, nHeight, nWidth, index, ref matchCount, depth);

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