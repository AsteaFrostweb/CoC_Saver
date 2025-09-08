using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIA;

namespace CoCSaver
{
    public static class ImageProcessing
    {
        public static Bitmap PreprocessForOCR(Bitmap src, byte brightnessThreshold)
        {
            Bitmap bmp = new Bitmap(src.Width, src.Height);
            using (Graphics g = Graphics.FromImage(bmp))
                g.DrawImage(src, 0, 0, src.Width, src.Height);

            int newWidth = bmp.Width * 4;
            int newHeight = bmp.Height * 4;
            Bitmap scaledBmp = new Bitmap(newWidth, newHeight);
            using (Graphics g = Graphics.FromImage(scaledBmp))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                g.DrawImage(bmp, 0, 0, newWidth, newHeight);
            }

            //scaledBmp = AdjustContrast(scaledBmp, 2, 1f);
            scaledBmp = ConvertToBlackAndWhite(scaledBmp, brightnessThreshold);

            return scaledBmp;
        }

        public static Bitmap ConvertToBlackAndWhite(Bitmap src, byte threshold = 128)
        {
            Bitmap bw = new Bitmap(src.Width, src.Height);
            for (int y = 0; y < src.Height; y++)
            {
                for (int x = 0; x < src.Width; x++)
                {
                    Color c = src.GetPixel(x, y);
                    int gray = (int)(0.299 * c.R + 0.587 * c.G + 0.114 * c.B);
                    bw.SetPixel(x, y, gray < threshold ? Color.Black : Color.White);
                }
            }
            return bw;
        }

        public static Bitmap AdjustContrast(Bitmap src, float contrast = 1.2f, float brightness = 1.0f)
        {
            // contrast >1 increases contrast, <1 decreases
            // brightness >1 makes brighter, <1 darker
            Bitmap bmp = new Bitmap(src.Width, src.Height);
            for (int y = 0; y < src.Height; y++)
            {
                for (int x = 0; x < src.Width; x++)
                {
                    Color c = src.GetPixel(x, y);

                    // Adjust contrast
                    float r = (((c.R / 255.0f - 0.5f) * contrast + 0.5f) * brightness) * 255f;
                    float g = (((c.G / 255.0f - 0.5f) * contrast + 0.5f) * brightness) * 255f;
                    float b = (((c.B / 255.0f - 0.5f) * contrast + 0.5f) * brightness) * 255f;

                    // Clamp to 0-255
                    r = Math.Max(0, Math.Min(255, r));
                    g = Math.Max(0, Math.Min(255, g));
                    b = Math.Max(0, Math.Min(255, b));

                    bmp.SetPixel(x, y, Color.FromArgb((int)r, (int)g, (int)b));
                }
            }
            return bmp;
        }

        public static bool IsRectValid(Rectangle rect)
        {
            if (rect == Rectangle.Empty) return false;
            if (rect.Width == 0 || rect.Height == 0) return false;

            return true;
        }

        public static Bitmap WiaImageFileToBitmap(ImageFile imageFile)
        {
            var bytes = (byte[])imageFile.FileData.get_BinaryData();
            using (var ms = new System.IO.MemoryStream(bytes))
            {
                return new Bitmap(ms);
            }
        }
    }
}
