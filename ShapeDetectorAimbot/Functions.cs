using AForge;
using AForge.Imaging.Filters;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace PixelAimbots
{
    internal class Functions
    {
        public static class Mouse
        {
            public static void SetLocation(System.Drawing.Point Loc)
            {
                Cursor.Position = new System.Drawing.Point(Loc.X, Loc.Y);
            }
        }

        public static class GameWindow
        {
            public static Bitmap CaptureImage(int width, int height, int sourcex, int sourcey)
            {
                Bitmap _BitmapCapture = new Bitmap(width, height);
                Graphics _Graphics = Graphics.FromImage(_BitmapCapture);
                _Graphics.CopyFromScreen(sourcex, sourcey, 0, 0, _BitmapCapture.Size, CopyPixelOperation.SourceCopy);
                return _BitmapCapture;
            }
        }

        public static class Image
        {
            public static Bitmap ApplyEdgeDetectorFilter(Bitmap image)
            {
                image = Grayscale.CommonAlgorithms.RMY.Apply(image);
                return new CannyEdgeDetector().Apply(image);
            }
        }

        public static class Math
        {
            public static System.Drawing.Point[] ToPointsArray(List<IntPoint> points)
            {
                System.Drawing.Point[] _Array = new System.Drawing.Point[points.Count];
                for (int i = 0, n = points.Count; i < n; i++)
                {
                    _Array[i] = new System.Drawing.Point(points[i].X, points[i].Y);
                }
                return _Array;
            }
        }
    }
}
