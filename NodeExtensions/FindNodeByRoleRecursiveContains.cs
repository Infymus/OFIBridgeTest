using Tests.Utilities;
using WindowsAccessBridgeInterop;

namespace OFIBridgeTest.Tests.NodeExtensions
{
    public static partial class NodeExtensions
    {
        /// <summary>
        /// Used for Checking a node appearing but not erroring out. 
        /// Useful for popup windows that have a title name and contains text.
        /// </summary>
        /// <param name="elementName"></param>
        /// <param name="elementContains"></param>
        /// <param name="role"></param>
        /// <param name="parent"></param>
        /// <param name="states"></param>
        /// <param name="index"></param>
        /// <param name="matchCount"></param>
        /// <param name="depth"></param>
        /// <returns></returns>
        private static AccessibleContextNode? FindNodeByRoleRecursiveContains(string elementName, string elementContains, Role role,
            AccessibleNode? parent, State[]? states, int index, ref int matchCount, int depth)
        {
            if (parent == null)
            {
                return null;
            }

            var childNodes = parent.GetChildren();
            foreach (var child in childNodes)
            {
                if (child is not AccessibleContextNode contextNode) continue;
                contextNode.Refresh();
                var info = contextNode.GetInfo();
                if (info.role == role.GetStringValue() && info.name.StartsWith(elementName) && info.name.Contains(elementContains))
                {
                    var containsAll = true;
                    if (states != null || states?.Length > 0)
                    {
                        var parsedStates = info.states.Split(',')
                            .Select(s => s.Trim())
                            .ToArray();

                        var stateStrings = states?.Select(s => s.GetStringValue()).ToArray() ?? Array.Empty<string>();
                        containsAll = stateStrings.All(state => parsedStates.Contains(state));
                    }

                    if (containsAll)
                    {
                        if (matchCount == index)
                        {
                            return contextNode;
                        }

                        matchCount++;
                    }
                }

                // Increment the depth for the recursive call
                var foundNode = FindNodeByRoleRecursiveContains(elementName, elementContains, role, contextNode, states, index, ref matchCount, depth + 1);
                if (foundNode != null)
                {
                    return foundNode;
                }
            }

            return null;
        }
    }
}