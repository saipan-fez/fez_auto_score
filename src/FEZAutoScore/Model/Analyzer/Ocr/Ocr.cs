using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace FEZAutoScore.Model.Analyzer.Ocr
{
    public abstract class Ocr<T>
    {
        protected abstract string Process(Bitmap bitmap);

        protected abstract T ConvertTo(string str);

        public T Process(Bitmap bitmap, Rectangle rect)
        {
            string str = null;

            using (var clip = bitmap.Clone(rect, PixelFormat.Format24bppRgb))
            {
                str = Process(clip);
            }

            return ConvertTo(str);

        }

        protected static unsafe uint ToColor(byte* ptr)
        {
            return (uint)((*ptr) | (*(ptr + 1) << 8) | (*(ptr + 2) << 16));
        }
    }

    public class OcrFailedException : Exception { }

    public abstract class IntegerOcr : Ocr<int>
    {
        protected override int ConvertTo(string str)
        {
            if (int.TryParse(str, out int result))
            {
                return result;
            }
            else
            {
                throw new OcrFailedException();
            }
        }
    }

    public abstract class StringOcr : Ocr<string>
    {
        protected override string ConvertTo(string str)
        {
            return str;
        }
    }

    public abstract class TimeSpanOcr : Ocr<TimeSpan>
    {
        protected override TimeSpan ConvertTo(string str)
        {
            if (TimeSpan.TryParse(str, out TimeSpan result))
            {
                return result;
            }
            else
            {
                throw new OcrFailedException();
            }
        }
    }
}
