using Tests.Utilities;
using WindowsAccessBridgeInterop;

namespace OFIBridgeTest.Tests.NodeExtensions
{
    public static partial class NodeExtensions
    {
        /// <summary>
        /// Returns the State[] of any Node
        /// </summary>
        /// <param name="findNodeName"></param>
        /// <param name="nodeContains"></param>
        /// <param name="indexInParent"></param>
        /// <param name="parent"></param>
        /// <param name="role"></param>
        /// <param name="states"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static State[] ReturnStateByArray(string findNodeName, string nodeContains, int indexInParent,
            AccessibleNode? parent, Role role, State[]? states = null, int index = 0)
        {
            List<State> result = new List<State>();
            DebugOutput($"ReturnStateByArray: '{findNodeName}' | Contains = '{nodeContains}' | Index = '{indexInParent}'");
            var foundNode = FindNodeByRoleContainsArray(findNodeName, nodeContains, indexInParent, role, parent);
            if (foundNode != null)
            {
                var parentInfo = foundNode.GetInfo();
                string[] statesArray = parentInfo.states.Split(',');
                return parentInfo.states
                    .Split(',')
                    .Select(stateString => Enum.GetValues(typeof(State))
                        .Cast<State>()
                        .FirstOrDefault(state => state.GetStringValue() == stateString))
                    .Where(state => state != null)
                    .ToArray();
            }
            else
            {
                result.Add(State.NotFound);
                return result.ToArray();
            }
        }
    }
}