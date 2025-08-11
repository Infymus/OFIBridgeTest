using OFIBridgeTest.Tests;
using System.Runtime.InteropServices;

namespace TestHelpers.KeyBoardMouse
{
    public enum clickDirs
    {
        Up, Down, Left, Right
    }

    public class KeyboardHelper
    {
        [DllImport("user32.dll")]
        private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, nuint dwExtraInfo);

        private const int KEYEVENTF_KEYUP = 0x0002;

        private static void DebugOutput(string inDebugData)
        {
            DateTime dateTime = DateTime.Now;
            string formattedDate = dateTime.ToString("MM-dd-yyyy @ hh:mm:ss tt");
            System.Diagnostics.Debug.WriteLine($"{formattedDate} : {inDebugData}");
        }

        /// <summary>
        /// Type text into an EditBox/Field
        /// </summary>
        /// <param name="text"></param>
        public static void TypeText(string text)
        {
            DebugOutput($"TypeText('{text}')");
            foreach (char c in text)
            {
                // Handle special characters
                string sendKey = c switch
                {
                    '(' => "{(}",
                    ')' => "{)}",
                    '+' => "{+}",
                    '^' => "{^}",
                    '%' => "{%}",
                    '~' => "{~}",
                    '{' => "{{}",
                    '}' => "{}}",
                    '[' => "{[}",
                    ']' => "{]}",
                    _ => c.ToString()  // Normal characters
                };
                SendKeys.SendWait(sendKey);
                Thread.Sleep(Globals.MinTypeTime);
            }
        }

        /// <summary>
        /// Sends a TAB key to the Oracle Forms.
        /// </summary>
        public static void PressTAB(int inTimes, int inSleep = 0)
        {
            DebugOutput($"PressTAB(inTimes = {inTimes} | inSleep = {inSleep})");
            for (int count = 0; count < inTimes; count++)
            {
                SendKeys.SendWait("{TAB}");
                if (inSleep > 0)
                    Thread.Sleep(inSleep);
            }
        }

        /// <summary>
        /// Presses an ESCAPE key
        /// </summary>
        public static void PressESC()
        {
            // If you are going to use InputSimpulator, uncomment this:
            // var simu = new InputSimulator();
            //simu.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.ESCAPE);
            SendKeys.Send("{ESC}");
        }

        /// <summary>
        /// Clears any Oracle field of characters
        /// </summary>
        /// <param name="numChars"></param>
        public static void ClearInputText(int numChars)
        {
            DebugOutput($"ClearInputText({numChars})");
            for (int i = 0; i < numChars; i++)
            {
                SendKeys.SendWait("{BS}");
                Thread.Sleep(Globals.MinTypeTime); // Small delay to simulate real typing
            }
        }

        public static void ClickDirection(clickDirs inDirection, int numTimes = 1)
        {
            DebugOutput($"ClickDirection({inDirection} {numTimes} time(s))");
            for (int i = 0; i < numTimes; i++)
            {
                switch (inDirection)
                {
                    case clickDirs.Up:
                        SendKeys.Send("{UP}");
                        break;
                    case clickDirs.Down:
                        SendKeys.Send("{DOWN}");
                        break;
                    case clickDirs.Left:
                        SendKeys.Send("{LEFT}");
                        break;
                    case clickDirs.Right:
                        SendKeys.Send("{RIGHT}");
                        break;
                }
                Thread.Sleep(50);
            }
        }

        public static void PressEnter()
        {
            DebugOutput($"PressEnter()");
            SendKeys.Send("{ENTER}");
        }

        public static void SendWaitWithDelay(string keys, int delay)
        {
            SendKeys.SendWait(keys);
            Thread.Sleep(delay);
        }

        public static string CaptureText()
        {
            DebugOutput($"CaptureText()");
            SendKeys.Send("^A");
            Thread.Sleep(500);
            SendKeys.Send("^C");
            Thread.Sleep(500);
            return Clipboard.GetText();
        }

    }
}