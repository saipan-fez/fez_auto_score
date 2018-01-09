using FEZAutoScore.Extension;
using System.Drawing;
using System.Drawing.Imaging;

namespace FEZAutoScore.Model.Analyzer.Ocr
{
    public class KillScoreOcr : IntegerOcr
    {
        protected const uint White = 0xFFFFFF;

        protected override unsafe string Process(Bitmap bitmap)
        {
            BitmapData bitmapData = null;
            string ret = null;

            try
            {
                // 二値化
                bitmap.ToThresholding();

                bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);

                int bpp = 3;
                int stride = bitmapData.Stride;
                byte* startPtr = ((byte*)bitmapData.Scan0) + stride * 5; // y=5の列
                byte* endPtr = startPtr + bitmapData.Width * bpp;

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
                        // 指定のピクセルの色をチェック
                        var c1 = ToColor(ptr + stride * -1 + 1 * bpp); // (+1, -1)px
                        var c2 = ToColor(ptr + stride *  1 + 0 * bpp); // ( 0, +1)px
                        var c3 = ToColor(ptr + stride *  1 + 1 * bpp); // (+1, +1)px
                        var c4 = ToColor(ptr + stride *  2 + 1 * bpp); // (+1, +2)px

                        if (c1 == White && c2 == White && c3 != White && c4 == White)       // true , true , false, true :0
                        {
                            ret += "0";
                        }
                        else if (c1 != White && c2 == White && c3 == White && c4 != White)  // false, true , true , false:9
                        {
                            ret += "9";
                        }
                        else if (c1 != White && c2 == White && c3 != White && c4 != White)  // false, true , false, false:1
                        {
                            ret += "1";
                        }
                        else if (c1 != White && c2 != White && c3 == White && c4 != White)  // false, false, true , false:3
                        {
                            ret += "3";
                        }
                        else if (c1 != White && c2 != White && c3 != White && c4 != White)  // false, false, false, false:4
                        {
                            ret += "4";
                        }
                        else if (c1 == White && c2 == White && c3 != White && c4 != White)  // true , true , false, false
                        {
                            // (+1, -2)pxの色をチェック
                            if (ToColor(ptr + stride * -2 + 1 * bpp) == White)
                            {
                                ret += "7";
                            }
                            else
                            {
                                ret += "2";
                            }
                        }
                    }
                    else if (r1c == White && r2c != White)
                    {
                        // Group:2px
                        // (0, +3)pxの色をチェック
                        if (ToColor(ptr + stride * 3) == White)
                        {
                            ret += "8";
                        }
                        else
                        {
                            ret += "5";
                        }
                    }
                    else if (r1c == White && r2c == White && r3c != White)
                    {
                        // Group:3px
                        ret += "6";
                    }

                    // 同じ数字の部分は走査をスキップさせる
                    //   1文字の中で連続しない#FFFFFFのピクセルがあるため、一律4pxスキップさせる
                    ptr += 4 * bpp;
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
