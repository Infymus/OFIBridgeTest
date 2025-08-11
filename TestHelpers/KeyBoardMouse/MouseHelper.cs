using OFIBridgeTest.Tests.NodeExtensions;
using System.Runtime.InteropServices;
using TestHelpers.Enums;

namespace TestHelpers.KeyBoardMouse
{
    public class MouseHelper
    {
        [DllImport("user32.dll")]
        private static extern bool SetCursorPos(int X, int Y);

        [DllImport("user32.dll")]
        private static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, int dwExtraInfo);

        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;

        private static void DebugOutput(string inDebugData)
        {
            DateTime dateTime = DateTime.Now;
            string formattedDate = dateTime.ToString("MM-dd-yyyy @ hh:mm:ss tt");
            System.Diagnostics.Debug.WriteLine($"{formattedDate} : {inDebugData}");
        }

        public static void Click(Rect rect, ClickPoint clickPoint, bool silent = false)
        {
            var (clickX, clickY) = CalculateClickPoint(rect.X, rect.Y, rect.Width, rect.Height, clickPoint);
            if (!silent)
                DebugOutput($"| Click | X = {clickX} | Y = {clickY} | ClickPoint = {clickPoint}");
            SetCursorPos(clickX, clickY);
            mouse_event(MOUSEEVENTF_LEFTDOWN, (uint)clickX, (uint)clickY, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTUP, (uint)clickX, (uint)clickY, 0, 0);
        }

        private static (int, int) CalculateClickPoint(int x, int y, int width, int height, ClickPoint clickPoint)
        {
            var clickX = x;
            var clickY = y;

            switch (clickPoint)
            {
                case ClickPoint.ScrollBarRightFull:
                    clickX = x + width - 20;
                    clickY = y + 5;
                    break;
                case ClickPoint.ScrollBarRight:
                    clickX = x + width - 6;
                    clickY = y + 5;
                    break;
                case ClickPoint.ScrollBarLeft:
                    clickX = x + 5;
                    clickY = y + 5;
                    break;
                case ClickPoint.ScrollBarLeftFull:
                    clickX = x + 20;
                    clickY = y + 5;
                    break;
                case ClickPoint.ScrollBarDown:
                    clickX = x + 5;
                    clickY = y + height - 6;
                    break;
                case ClickPoint.ScrollBarUp:
                    clickX = x + 5;
                    clickY = y + height + 5;
                    break;
                case ClickPoint.Offset:
                    clickX = x;
                    clickY = y;
                    break;
                case ClickPoint.ThreeDotsMinimum:
                    clickX = x + width - 2;
                    clickY = y + 2;
                    break;
                case ClickPoint.ThreeDots:
                    clickX = x + width - 8;
                    clickY = y + 8;
                    break;
                case ClickPoint.Close:
                    clickX = x + width - 5;
                    clickY = y + 3;
                    break;
                case ClickPoint.Minimize:
                    clickX = x + width - 12;
                    clickY = y + 3;
                    break;
                case ClickPoint.Maximize:
                    clickX = x + width - 6;
                    clickY = y + 3;
                    break;
                case ClickPoint.TopLeft:
                    clickX = x;
                    clickY = y;
                    break;
                case ClickPoint.TopCenter:
                    clickX = x + width / 2;
                    clickY = y;
                    break;
                case ClickPoint.TopRight:
                    clickX = x + width;
                    clickY = y;
                    break;
                case ClickPoint.CenterLeft:
                    clickX = x;
                    clickY = y + height / 2;
                    break;
                case ClickPoint.Center:
                    clickX = x + width / 2;
                    clickY = y + height / 2;
                    break;
                case ClickPoint.CenterRight:
                    clickX = x + width;
                    clickY = y + height / 2;
                    break;
                case ClickPoint.BottomLeft:
                    clickX = x;
                    clickY = y + height;
                    break;
                case ClickPoint.BottomCenter:
                    clickX = x + width / 2;
                    clickY = y + height;
                    break;
                case ClickPoint.BottomRight:
                    clickX = x + width;
                    clickY = y + height;
                    break;
                case ClickPoint.TextEnd:
                    clickX = x + width - 10;
                    clickY = y + 5;
                    break;
                default:
                    clickX = x + width - 5;
                    clickY = y + height / 2;
                    break;
            }

            return (clickX, clickY);
        }
    }
}
