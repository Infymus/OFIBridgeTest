using WindowsAccessBridgeInterop;

namespace OFIBridgeTest.Tests.NodeExtensions
{
    public static partial class NodeExtensions
    {
        /// <summary>
        /// Looks for a Node by a Start and End Index and if found in that array returns it
        /// Also allows a CheckParse so if you are looking for a Double/Int field, set checkParse TRUE and it will make sure the
        /// return can be converted to a Double. Good for order numbers and things.
        /// NOTE: This always goes in REVERESE as most of the nodes are either -1 or -2 from what AccessBridgeExplorer says they are.
        /// </summary>
        /// <param name="findNodeName"></param>
        /// <param name="nodeContains"></param>
        /// <param name="startIdx"></param>
        /// <param name="endIdx"></param>
        /// <param name="checkParse"></param>
        /// <param name="parent"></param>
        /// <param name="role"></param>
        /// <param name="states"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static string CheckAppStateReadTextByArrayIndex(string findNodeName, string nodeContains, int startIdx, int endIdx, bool checkParse,
            AccessibleNode? parent, Role role, State[]? states = null, int index = 0)
        {
            DebugOutput($"CheckAppStateReadTextByArrayIndex: '{findNodeName}' | Contains = '{nodeContains}' | Index Start/End = '{startIdx}/{endIdx}'");
            for (int countIdx = startIdx; countIdx >= endIdx; countIdx--)
            {
                if (CheckAppStateByArray(findNodeName, nodeContains, countIdx, parent, Role.Text))
                {
                    string checkVar = ReadTextByArray(findNodeName, parent, Role.Text, countIdx, "Sentence");
                    if (checkParse)
                    {
                        if (double.TryParse(checkVar, out _))
                        {
                            DebugOutput($"| Found '{findNodeName}' @ Index '{countIdx}' | Value = '{checkVar}'");
                            return checkVar;
                        }
                    }
                    else
                    {
                        DebugOutput($"| Found '{findNodeName}' @ Index '{countIdx}' | Value = '{checkVar}'");
                        return checkVar;
                    }
                }
            }
            return "";
        }
    }
}