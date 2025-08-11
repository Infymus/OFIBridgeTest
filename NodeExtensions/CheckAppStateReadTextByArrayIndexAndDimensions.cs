using WindowsAccessBridgeInterop;

namespace OFIBridgeTest.Tests.NodeExtensions
{
    public static partial class NodeExtensions
    {
        /// <summary>
        /// This one adds extra parameters for Width and Height
        /// </summary>
        /// <param name="findNodeName"></param>
        /// <param name="nodeContains"></param>
        /// <param name="startIdx"></param>
        /// <param name="endIdx"></param>
        /// <param name="nWidth"></param>
        /// <param name="nHeight"></param>
        /// <param name="checkParse"></param>
        /// <param name="parent"></param>
        /// <param name="role"></param>
        /// <param name="states"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static string CheckAppStateReadTextByArrayIndexAndDimensions(string findNodeName, string nodeContains, int startIdx, int endIdx,
            int nHeight, int nWidth, bool checkParse, AccessibleNode? parent, Role role, State[]? states = null, int index = 0)
        {
            DebugOutput(@$"CheckAppStateReadTextByArrayIndexAndDimensions: '{findNodeName}' | Contains = '{nodeContains}' | Index Start/End = '{startIdx}/{endIdx}'
                | Height/Width = '{nHeight}/{nWidth}'");
            for (int countIdx = startIdx; countIdx >= endIdx; countIdx--)
            {
                if (CheckAppStateByArrayIndexAndDimensions(findNodeName, nodeContains, countIdx, nHeight, nWidth, parent, Role.Text))
                {
                    string checkVar = ReadTextByArrayIndexAndDimensions(findNodeName, nodeContains, parent, role, countIdx, nHeight, nWidth);
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

