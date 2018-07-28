using System.Collections;
using System.Diagnostics;
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
             * 1. リサイズ
             * 2. グレースケール
             * 3, 自ピクセルと次のピクセルを比較。
             * 4, ビットの割り当て
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

                byte[] buffer = target.ToByteArray();
                const int pixel_size = 4;
                // target Bitmapは12*11なため、target.Height 画像サイズ分減らす。
                var bits = new BitArray(buffer.Length / pixel_size - target.Height);
                for (int i = 0, index = 0; i < bits.Length; i++, index += pixel_size)
                {
                    // 3と4の処理
                    // グレースケールはBGR値が同じなためB値のみで判定
                    bits.Set(i, buffer[index] < buffer[index + pixel_size]);
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

        public static byte[] ToByteArray(this Bitmap bitmap)
        {
            /*
             * このインスタンスのビットマップデータをBGRA順でbyte配列にコピーします。
             * note: 4バイト単位でアクセスするのでint配列でもよいかも。
             */
            Debug.Assert(bitmap.PixelFormat == PixelFormat.Format32bppArgb);
            byte[] array = new byte[0];
            var bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, bitmap.PixelFormat);
            try
            {
                array = new byte[bitmapData.Stride * bitmap.Height];
                Marshal.Copy(bitmapData.Scan0, array, 0, array.Length);
            }
            finally
            {
                bitmap.UnlockBits(bitmapData);
            }
            return array;
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
