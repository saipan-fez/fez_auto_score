using System.Drawing;
using System.Drawing.Imaging;

namespace FEZAutoScore.Model.Analyzer.Ocr
{
    public class DetailScoreOcr : IntegerOcr
    {
        protected const uint White = 0xFFFFFF;

        protected override unsafe string Process(Bitmap bitmap)
        {
            BitmapData bitmapData = null;
            string ret = null;

            try
            {
                bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);

                int bpp = 3;
                int stride = bitmapData.Stride;
                byte* startPtr = (byte*)bitmapData.Scan0; // y=0の列
                byte* endPtr = startPtr + bitmapData.Width * bpp;
                int skipPixel = 0;

                for (byte* ptr = startPtr; ptr < endPtr; ptr += bpp)
                {
                    // 白でなければ次のピクセル
                    if (ToColor(ptr) != White)
                    {
                        continue;
                    }

                    var r1c = ToColor(ptr + 1 * bpp); // 1px横のピクセルの色
                    var r2c = ToColor(ptr + 2 * bpp); // 2px横のピクセルの色
                    var r3c = ToColor(ptr + 3 * bpp); // 3px横のピクセルの色

                    if (r1c != White)
                    {
                        // Group:1px
                        // (-1, +2)pxの色をチェック
                        if (ToColor(ptr + stride * 2 - 1 * bpp) == White)
                        {
                            ret += "4";
                        }
                        else
                        {
                            ret += "1";
                        }

                        skipPixel = 0;
                    }
                    else if (r1c == White && r2c != White)
                    {
                        // Group:2px
                        // 指定のピクセルの色をチェック
                        var c1 = ToColor(ptr + stride * 3 - 1 * bpp); // (-1, +3)px
                        var c2 = ToColor(ptr + stride * 4 - 1 * bpp); // (-1, +4)px
                        var c3 = ToColor(ptr + stride * 5 - 1 * bpp); // (-1, +5)px
                        var c4 = ToColor(ptr + stride * 4 - 0 * bpp); // ( 0, +4)px

                        if (c1 == White && c2 == White && c3 == White && c4 == White)       // true,  true,  true,  true :6
                        {
                            ret += "6";
                        }
                        else if (c1 == White && c2 == White && c3 == White && c4 != White)  // true,  true,  true,  false:0
                        {
                            ret += "0";
                        }
                        else if (c1 == White && c2 != White && c3 == White && c4 == White)  // true,  false, true,  true :8
                        {
                            ret += "8";
                        }
                        else if (c1 == White && c2 != White && c3 != White && c4 == White)  // true,  false, false, true :9
                        {
                            ret += "9";
                        }
                        else if (c1 != White && c2 != White && c3 != White && c4 != White)  // false, false, false, false:2
                        {
                            ret += "2";
                        }
                        else if (c1 != White && c2 != White && c3 != White && c4 == White)  // false, false, false, true :3
                        {
                            ret += "3";
                        }

                        skipPixel = 1;
                    }
                    else if (r1c == White && r2c == White && r3c == White)
                    {
                        // Group:4px
                        // (0, +1)pxの色をチェック
                        if (ToColor(ptr + stride * 1 - 0 * bpp) == White)
                        {
                            ret += "5";
                        }
                        else
                        {
                            ret += "7";
                        }

                        skipPixel = 3;
                    }

                    // 同じ数字の部分は走査をスキップさせる
                    ptr += skipPixel * bpp;
                }
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

            return ret;
        }
    }
}
