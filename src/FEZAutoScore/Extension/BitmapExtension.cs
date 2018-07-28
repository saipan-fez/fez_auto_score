using System.Collections;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Linq;

namespace FEZAutoScore.Extension
{
    public static class BitmapExtension
    {
        public static BitArray GenerateDifferenceHash(this Bitmap bitmap)
        {
            /*
             * dHash (Difference Hash)
             * @see http://www.hackerfactor.com/blog/index.php?/archives/529-Kind-of-Like-That.html
             * 1. リサイズ & グレースケール
             * 2, 左側のピクセルと自身のピクセルの輝度を比較する。
             */
            using (Bitmap target = new Bitmap(12, 11))
            {
                using (ImageAttributes ia = new ImageAttributes())
                {
                    // @see https://qiita.com/yoya/items/96c36b069e74398796f3#ntsc-%E4%BF%82%E6%95%B0-
                    ia.SetColorMatrix(new ColorMatrix(
                        new float[][]{
                    new float[]{ 0.298839f, 0.298839f, 0.298839f, 0 ,0},
                    new float[]{ 0.586811f, 0.586811f, 0.586811f, 0, 0},
                    new float[]{ 0.114350f, 0.114350f, 0.114350f, 0, 0},
                    new float[]{ 0, 0, 0, 1, 0},
                    new float[]{ 0, 0, 0, 0, 1}
                    }));
                    using (Graphics g = Graphics.FromImage(target))
                    {
                        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                        g.DrawImage(bitmap, new Rectangle(0, 0, target.Width, target.Height),
                            0, 0, bitmap.Width, bitmap.Height,
                            GraphicsUnit.Pixel, ia);
                    }
                }
                var bits = new BitArray(target.Width * target.Height);
                int index = 0;
                for (int y = 0; y < target.Height; y++)
                {
                    // ToDo: GetPixelを2回しているので直したい。
                    Color left = target.GetPixel(0, y);
                    for (int x = 1; x < target.Width; x++)
                    {
                        Color right = target.GetPixel(x, y);
                        // グレースケールはRGB値が同じなため。
                        if (left.R > right.R)
                        {
                            bits.Set(index, true);
                        }
                        index++;
                    }
                }
                return bits;
            }
        }

        public static byte[] GenerateHashFromBitmapData(this Bitmap bitmap)
        {
            byte[] ret;

            var bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, bitmap.PixelFormat);
            int bsize = bitmapData.Stride * bitmap.Height;
            byte[] array = new byte[bsize];
            Marshal.Copy(bitmapData.Scan0, array, 0, bsize);
            bitmap.UnlockBits(bitmapData);

            using (var provider = MD5.Create())
            {
                ret = provider.ComputeHash(array);
            }

            return ret;
        }

        public static unsafe void ToThresholding(this Bitmap bitmap, bool reverse = false)
        {
            BitmapData bitmapData = null;

            try
            {
                bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

                ToThresholding(bitmapData, reverse);
            }
            finally
            {
                try
                {
                    if (bitmapData != null)
                    {
                        bitmap.UnlockBits(bitmapData);
                        bitmapData = null;
                    }
                }
                catch { }
            }
        }

        private static unsafe void ToThresholding(BitmapData bitmapData, bool reverse)
        {
            int bpp = 3;
            int stride = bitmapData.Stride;
            byte* startPtr = (byte*)bitmapData.Scan0;
            uint color;
            uint com_color = reverse ? (uint)0x000000 : (uint)0xFFFFFF;
            byte set_color = reverse ? (byte)0xFF : (byte)0x00;

            for (int y = 0; y < bitmapData.Height; y++)
            {
                byte* lptr = startPtr + stride * y;
                byte* rptr = lptr + bitmapData.Width * bpp;

                for (byte* ptr = lptr; ptr < rptr; ptr += bpp)
                {
                    color = (uint)((*ptr) | (*(ptr + 1) << 8) | (*(ptr + 2) << 16));

                    if (color != com_color)
                    {
                        *(ptr + 0) = set_color;
                        *(ptr + 1) = set_color;
                        *(ptr + 2) = set_color;
                    }
                }
            }
        }
    }
}
