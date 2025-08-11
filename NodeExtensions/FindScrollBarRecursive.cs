using Tests.Utilities;
using WindowsAccessBridgeInterop;

namespace OFIBridgeTest.Tests.NodeExtensions
{
    public static partial class NodeExtensions
    {
        private static AccessibleContextNode? FindScrollBarRecursive(string elementName, Role role, AccessibleNode? parent, int x, int y, State[]? states, int index, ref int matchCount, int depth)
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

                //if (info.role == role.GetStringValue() && info.name.StartsWith(elementName) && info.x == x && info.y == y)
                if (info.role == role.GetStringValue()
                    && info.name.StartsWith(elementName)
                    && (x == 0 && y == 0 || (info.x == x && info.y == y)))
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
                            DebugOutput($"| FindNodeByRoleRecursive() | Found: '{info.name}'");
                            return contextNode;
                        }

                        matchCount++;
                    }
                }

                // Increment the depth for the recursive call
                var foundNode = FindScrollBarRecursive(elementName, role, contextNode, x, y, states, index, ref matchCount, depth + 1);
                if (foundNode != null)
                {
                    return foundNode;
                }
            }

            return null;
        }
    }
}