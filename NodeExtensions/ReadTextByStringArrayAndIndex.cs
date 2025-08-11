using NUnit.Framework;
using WindowsAccessBridgeInterop;

namespace OFIBridgeTest.Tests.NodeExtensions
{
    public static partial class NodeExtensions
    {
        /// <summary>
        /// This allows you to pass an array of strings plus an index of where they may be on the nodes.
        /// Some nodes move around in index, plus the name of the node changes. Instead of writing multiple loops everywhere,
        /// this method lets you pass all those possible strings and the index they may be in. Goes from END to START.
        /// </summary>
        /// <param name="startIdx"></param>
        /// <param name="endIdx"></param>
        /// <param name="parent"></param>
        /// <param name="role"></param>
        /// <param name="strings"></param>
        /// <returns></returns>
        public static string ReadTextByStringArrayAndIndex(int startIdx, int endIdx, AccessibleNode? parent, Role role, params string[] strings)
        {
            DebugOutput($"ReadTextByStringArrayAndIndex | Multi-String | Index Start/End = '{startIdx}/{endIdx}'");
            for (int countIdx = startIdx; countIdx >= endIdx; countIdx--)
            {
                foreach (var findStr in strings)
                {
                    DebugOutput($"| Index '{countIdx}/{endIdx}' | Find '{findStr}'");
                    if (CheckAppStateByArray(findStr, "", countIdx, parent, Role.Text))
                    {
                        string returnStr = ReadTextByArray(findStr, parent, Role.Text, countIdx);
                        DebugOutput($"| Found : '{returnStr}' | index = '{countIdx}'");
                        return returnStr;
                    }
                }
            }
            Assert.Fail($"Node Not Found for Array[{strings}].");
            return "";
        }
    }
}