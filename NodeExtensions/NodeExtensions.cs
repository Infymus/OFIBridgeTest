using System.Diagnostics;
using System.Runtime.InteropServices;
using Tests.Utilities;
using WindowsAccessBridgeInterop;

//
// This class is where all of the Oracle Form Extensions go. This should give you an idea how to
// click a button, scroll a scroll bar, close a form, open a menu, read text, wait for oracle to respond
// and a whole lot more. Build on this.
//

namespace OFIBridgeTest.Tests.NodeExtensions
{
    public enum ClickPoint
    {
        TopLeft,
        TopCenter,
        TopRight,
        CenterLeft,
        Center,
        CenterRight,
        BottomLeft,
        BottomCenter,
        BottomRight,
        Minimize,
        Maximize,
        Close,
        ThreeDots,
        ThreeDotsMinimum,
        Offset,
        ScrollBarRightFull,
        ScrollBarLeftFull,
        ScrollBarRight,
        ScrollBarLeft,
        ScrollBarDown,
        ScrollBarUp,
        TextEnd
    }

    public enum State
    {
        [StringValue("horizontal")]
        Horizontal,

        [StringValue("vertical")]
        Vertical,

        [StringValue("visible")]
        Visible,

        [StringValue("showing")]
        Showing,

        [StringValue("enabled")]
        Enabled,

        [StringValue("editable")]
        Editable,

        [StringValue("focusable")]
        Focusable,

        [StringValue("single line")]
        SingleLine,

        [StringValue("not found")]
        NotFound
    }

    public class TextCellHelper
    {
        public static bool IsEditableTextNode(AccessibleContextNode node)
        {
            var info = node.GetInfo();
            return info.accessibleInterfaces.HasFlag(AccessibleInterfaces.cAccessibleTextInterface) && info.states.Contains("editable");
        }
    }

    public class Rect
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public Rect(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }
    }

    public static class AccessibleContextInfoExtensions
    {
        /// <summary>
        /// Gets the scaled dimensions based on the current screen scaling factor.
        /// </summary>
        /// <param name="contextInfo">The accessible context information.</param>
        /// <returns>An instance of ScaledDimensions with adjusted x, y, width, and height.</returns>
        public static AccessibleContextInfo? GetScaledInfo(this AccessibleContextNode contextNode)
        {
            var originalContextInfo = contextNode.GetInfo();

            // Create a copy of the contextInfo
            var scaledContextInfo = new AccessibleContextInfo
            {
                x = originalContextInfo.x,
                y = originalContextInfo.y,
                width = originalContextInfo.width,
                height = originalContextInfo.height
            };

            // Get the scaling factor
            var scalingFactor = GetScalingFactor();

            // Adjust the coordinates and size based on the scaling factor
            scaledContextInfo.x = (int)(scaledContextInfo.x * scalingFactor);
            scaledContextInfo.y = (int)(scaledContextInfo.y * scalingFactor);
            scaledContextInfo.width = (int)(scaledContextInfo.width * scalingFactor);
            scaledContextInfo.height = (int)(scaledContextInfo.height * scalingFactor);

            return scaledContextInfo;
        }

        /// <summary>
        /// Retrieves the current screen scaling factor.
        /// </summary>
        /// <returns>The scaling factor as a float.</returns>
        private static float GetScalingFactor()
        {
            try
            {
                var hMonitor = MonitorFromPoint(new Point(0, 0), MONITOR_DEFAULTTONEAREST);
                if (GetScaleFactorForMonitor(hMonitor, out var scaleFactor) == 0)
                {
                    return scaleFactor / 100f;
                }
                else
                {
                    throw new InvalidOperationException("Unable to retrieve scaling factor.");
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to get scaling factor.", ex);
            }
        }

        [DllImport("user32.dll")]
        private static extern nint MonitorFromPoint(Point pt, uint dwFlags);

        [DllImport("Shcore.dll")]
        private static extern int GetScaleFactorForMonitor(nint hMonitor, out int pScale);

        const uint MONITOR_DEFAULTTONEAREST = 2;
    }

    public class SerializableAccessibleWindow
    {
        public string Title { get; set; }
        public SerializableAccessibleContext RootContext { get; set; }

        public SerializableAccessibleWindow(string title, SerializableAccessibleContext rootContext)
        {
            Title = title;
            RootContext = rootContext;
        }
    }

    public class SerializableAccessibleContext
    {
        public string Name { get; set; }
        public string Role { get; set; }
        public List<SerializableAccessibleContext> Children { get; set; } = new List<SerializableAccessibleContext>();

        public SerializableAccessibleContext(string name, string role)
        {
            Name = name;
            Role = role;
        }
    }

    public static partial class NodeExtensions
    {
        public static AccessibleContextNode? GetParentPane(JavaObjectHandle childObject, WindowsAccessBridgeInterop.AccessBridge? accessBridge)
        {
            // Retrieve the parent object handle
            var parentObject = accessBridge?.Functions.GetParentWithRole(childObject.JvmId, childObject, "pane");

            // Check if the parent object is found
            if (parentObject == null) return null;
            // Retrieve role information of the parent object
            AccessibleContextInfo? roleInfo = null;
            var infoSuccess = accessBridge?.Functions.GetAccessibleContextInfo(parentObject.JvmId, parentObject, out roleInfo);
            return roleInfo?.role == "pane" ? new AccessibleContextNode(accessBridge, parentObject) : null;
        }

        public enum AccessibleTextEnums
        {
            [System.ComponentModel.Description("Text attributes at caret")]
            AtCaret,
            [System.ComponentModel.Description("Text attributes at point (0, 0)")]
            AtPoint,
            [System.ComponentModel.Description("Contents")]
            AtContents
        }

        private static void DebugOutput(string inDebugData)
        {
            DateTime dateTime = DateTime.Now;
            string formattedDate = dateTime.ToString("MM-dd-yyyy @ hh:mm:ss tt");
            Debug.WriteLine($"{formattedDate} : {inDebugData}");
        }

        public static void EnsureNodeHasFocus(this AccessibleContextNode? node, AccessBridge accessBridge)
        {
            if (node == null) return;

            var actions = new AccessibleActionsToDo
            {
                actionsCount = 1,
                actions = new AccessibleActionInfo[] { new AccessibleActionInfo { name = "focus" } }
            };
            var success = accessBridge.Functions.DoAccessibleActions(node.JvmId, node.AccessibleContextHandle, ref actions, out _);
            if (!success)
            {
                Debug.WriteLine("Failed to set focus on the node.");
            }
        }

        public static Rect? GetNodeRect(this AccessibleContextNode? node)
        {
            var info = node?.GetInfo();
            return info != null ? new Rect(info.x, info.y, info.width, info.height) : null;
        }

        public static void PrintNodeInfo(this AccessibleContextNode? node)
        {
            if (node == null) return;

            var info = node?.GetInfo();
            Debug.WriteLine($"Name: {info.name}");
            Debug.WriteLine($"Role: {info.role}");
            Debug.WriteLine($"States: {info.states}");
            Debug.WriteLine($"Is Editable: {info.states.Contains("editable")}");
            Debug.WriteLine($"Supports AccessibleText: {info.accessibleInterfaces.HasFlag(AccessibleInterfaces.cAccessibleTextInterface)}");
        }

    }
}

