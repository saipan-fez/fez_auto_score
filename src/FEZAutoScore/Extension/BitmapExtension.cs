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
        
        public static BitArray GenerateAverageHash(this Bitmap bitmap)
        {
            /*
             * 処理フロー
             * @see http://www.hackerfactor.com/blog/index.php?/archives/432-Looks-Like-It.html
             * 1. 画像をグレースケール
             * 2, グレースケール画像の平均値を取得
             * 3, ハッシュ化
             */
            byte[] grayscale = new byte[bitmap.Width * bitmap.Height];
            // グレースケール
            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    Color pixel = bitmap.GetPixel(x, y);
                    byte value = (byte)((306 * pixel.R + 601 * pixel.G + 117 * pixel.B) / 1024);
                    grayscale[x + (y * bitmap.Height)] = value;
                }
            }
            var average = grayscale.Average(x => x);
            var bits = new BitArray(grayscale.Length);
            foreach (var element in grayscale
                .Select((value, index) => new { value, index })
                .Where(x => x.value >= average))
            {
                bits.Set(element.index, true);
            }
            return bits;
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
