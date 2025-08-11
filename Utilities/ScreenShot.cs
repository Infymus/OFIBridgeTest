namespace Tests.Utilities
{
    public class ScreenShot
    {
        public static Bitmap CaptureScreenshot(int x, int y, int width, int height)
        {
            var bitmap = new Bitmap(width, height);
            using Graphics g = Graphics.FromImage(bitmap);
            g.CopyFromScreen(x, y, 0, 0, new Size(width, height));
            return bitmap;
        }
    }
}
