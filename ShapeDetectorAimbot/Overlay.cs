using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace PixelAimbots
{
    public partial class Overlay : Form
    {
        private Process GameProcess;
        private Thread OverlayThread => new Thread(HandleWindow);

        public Overlay()
        {
            InitializeComponent();
        }

        public void HandleWindow()
        {
            GameProcess = Process.GetProcesses().Where(x => x.ProcessName.Equals("Discord")).FirstOrDefault();

            while (GameProcess != null && !GameProcess.HasExited)
            {
                if (Globals.User32.GetWindowRect(GameProcess.MainWindowHandle, ref Globals.GameWindow.WindowRect))
                {
                    if (Globals.User32.GetClientRect(GameProcess.MainWindowHandle, ref Globals.GameWindow.ClientRectangle))
                    {
                        Invoke(new Action(() => UpdateWindow()));
                    }
                } 
                Thread.Sleep(1);
            }

            MessageBox.Show("Process is not running or has been closed");
            Invoke(new Action(() => Close()));
        }

        private void UpdateWindow()
        {
            if (Globals.GameWindow.IsMinimized) { Hide(); } Show();

            Globals.GameWindow.ClientToScreenPoint = new Point();
            if (Globals.User32.ClientToScreen(GameProcess.MainWindowHandle, ref Globals.GameWindow.ClientToScreenPoint))
            {
                Globals.GameWindow.ClientToScreenRectangle = new Rectangle(Globals.GameWindow.ClientToScreenPoint, Globals.GameWindow.ClientRectangle.Size);
                Location = Globals.GameWindow.ClientToScreenRectangle.Location;
                Size = Globals.GameWindow.ClientToScreenRectangle.Size;
                Invalidate();
            }
        }

        private void Overlay_Paint(object sender, PaintEventArgs e)
        {
            Graphics _Graphics = e.Graphics;

            Rectangle _Rectangle = new Rectangle(Globals.Drawings.RectangleCentered, Globals.Settings.ImageFOV);
            _Graphics.DrawRectangle(new Pen(Color.OrangeRed, 1), _Rectangle);

            var _Image = Functions.GameWindow.CaptureImage(Globals.Settings.ImageFOV.Width, Globals.Settings.ImageFOV.Height, Globals.GameWindow.WindowRect.left + (Globals.GameWindow.ClientRectangle.Width / 2) - (Globals.Settings.ImageFOV.Width / 2), Globals.GameWindow.WindowRect.top + (Globals.GameWindow.ClientRectangle.Height / 2) - (Globals.Settings.ImageFOV.Height / 2));
            _Graphics.DrawImage(Functions.Image.ApplyEdgeDetectorFilter(_Image), new Point(0, 180));
        }

        private void Overlay_Load(object sender, EventArgs e)
        {
            OverlayThread.Start();
        }

        private void Overlay_FormClosing(object sender, FormClosingEventArgs e)
        {
            OverlayThread.Abort();
        }
    }
}
