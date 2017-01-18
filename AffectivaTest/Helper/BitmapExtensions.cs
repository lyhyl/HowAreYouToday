using Affdex;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace HRUTWeb
{
    public static class BitmapExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bitmap">bitmap *MUST* be 24bppRgb</param>
        /// <returns>Frame</returns>
        public static Frame ConvertToFrame(this Bitmap bitmap)
        {
            if (bitmap == null || bitmap.PixelFormat != PixelFormat.Format24bppRgb)
                throw new ArgumentException(nameof(bitmap));

            Rectangle rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            BitmapData bmpData = bitmap.LockBits(rect, ImageLockMode.ReadWrite, bitmap.PixelFormat);

            IntPtr ptr = bmpData.Scan0;

            int numBytes = bitmap.Width * bitmap.Height * 3;
            byte[] rgbValues = new byte[numBytes];

            int data_x = 0;
            int ptr_x = 0;
            int row_bytes = bitmap.Width * 3;

            for (int y = 0; y < bitmap.Height; y++)
            {
                Marshal.Copy(ptr + ptr_x, rgbValues, data_x, row_bytes);
                data_x += row_bytes;
                ptr_x += bmpData.Stride;
            }

            bitmap.UnlockBits(bmpData);

            return new Frame(bitmap.Width, bitmap.Height, rgbValues, Frame.COLOR_FORMAT.BGR);
        }

        public static Bitmap LoadImageFitSize(string fileName, int mxl = 512)
        {
            Bitmap org = new Bitmap(fileName);
            Size size = org.Size;
            double mxlen = mxl;
            int len = Math.Max(size.Width, size.Height);
            double scale = mxlen / len;
            if (len > mxlen)
            {
                size.Width = (int)(size.Width * scale);
                size.Height = (int)(size.Height * scale);
                Bitmap bmp = new Bitmap(size.Width, size.Height, PixelFormat.Format24bppRgb);
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.DrawImage(org,
                        new RectangleF(0, 0, (float)(org.Width * scale), (float)(org.Height * scale)),
                        new RectangleF(0, 0, org.Width, org.Height),
                        GraphicsUnit.Pixel);
                }
                org.Dispose();
                return bmp;
            }
            else
                return org;
        }
    }
}
