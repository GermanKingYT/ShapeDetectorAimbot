using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace PixelAimbots
{
    internal class Globals
    {
        public static class Settings
        {
            public static Size ImageFOV => new Size(350, 350);
        }
        
        public static class Drawings
        {
            public static Point RectangleCentered => new Point((GameWindow.ClientRectangle.Width / 2) - (Settings.ImageFOV.Width / 2), (GameWindow.ClientRectangle.Height / 2) - (Settings.ImageFOV.Height / 2));
        }

        public static class GameWindow
        {
            public static bool IsMinimized => ClientRectangle.Size.IsEmpty;
            public static User32.Rect WindowRect;
            public static Rectangle ClientRectangle;
            public static Point ClientToScreenPoint;
            public static Rectangle ClientToScreenRectangle;
        }
        
        public static class User32
        {
            [StructLayout(LayoutKind.Sequential)]
            public struct Rect
            {
                public int left;
                public int top;
                public int right;
                public int bottom;
            }

            [DllImport("user32.dll")]
            public static extern bool GetWindowRect(IntPtr hWnd, ref Rect rect); 

            [DllImport("user32.dll", SetLastError = true)]
            public static extern bool GetClientRect(IntPtr hWnd, ref Rectangle lpRect); 

            [DllImport("user32.dll", SetLastError = true)]
            public static extern bool ClientToScreen(IntPtr hWnd, ref Point lpPoint); 
        }
    }
}
