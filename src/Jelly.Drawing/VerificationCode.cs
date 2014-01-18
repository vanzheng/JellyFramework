using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Text;
using Jelly.Helpers;

namespace Jelly.Drawing
{
    /// <summary>
    /// Represents verification code.
    /// </summary>
    public class VerificationCode
    {
        private string _randomCode;

        private char[] _finalCodes;
        private static char[] numbericCodes = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        private static char[] latinLettersCodes = {'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'w', 'x', 'y', 'z', 
                                                   'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'W', 'X', 'Y', 'Z' };

        private static Font[] fonts = {
                                        new Font(new FontFamily("Times New Roman"), 20 + RandomUtils.GetRandomNumber(4), FontStyle.Bold),
                                        new Font(new FontFamily("Georgia"), 20 + RandomUtils.GetRandomNumber(4), FontStyle.Bold),
                                        new Font(new FontFamily("Arial"), 20 + RandomUtils.GetRandomNumber(4), FontStyle.Bold),
                                        new Font(new FontFamily("Comic Sans MS"), 20 + RandomUtils.GetRandomNumber(4), FontStyle.Bold)
                                     };

        public VerificationCode()
            : this(Category.Numberic | Category.LatinLetters)
        {
        }

        public VerificationCode(Category category) 
        {
            switch (category)
            {
                case Category.Numberic:
                    this._finalCodes = numbericCodes;
                    break;
                case Category.LatinLetters:
                    this._finalCodes = latinLettersCodes;
                    break;
                case Category.Numberic | Category.LatinLetters:
                    this._finalCodes = ArrayUtils.Combin<char>(numbericCodes, latinLettersCodes);
                    break;
            }
        }

        #region 内部私有方法

        private string CreateRandomCode(int codeCount)
        {
            char[] allcodes = this._finalCodes;
            int len = allcodes.Length;
            StringBuilder randomCode = new StringBuilder();
            int pos;

            for (int i = 0; i < codeCount; i++)
            {
                Random random = RandomUtils.CreateRandom();
                pos = random.Next(len - 1);
                randomCode.Append(allcodes[pos]);
            }

            return randomCode.ToString();
        }

        #endregion

        public Bitmap GenerateImage(int codeLength)
        {
            return GenerateImage(codeLength, 120, 60, Color.White, 1);
        }

        public Bitmap GenerateImage(int codeLength, int width, int height, Color bgcolor, int textcolor)
        {
            this._randomCode = CreateRandomCode(codeLength);
            string code = this._randomCode;
            Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format32bppArgb);

            Graphics g = Graphics.FromImage(bitmap);
            Rectangle rect = new Rectangle(0, 0, width, height);
            g.SmoothingMode = SmoothingMode.AntiAlias;

            g.Clear(bgcolor);

            int fixedNumber = textcolor == 2 ? 60 : 0;

            SolidBrush drawBrush = new SolidBrush(Color.FromArgb(RandomUtils.GetRandomNumber(100), RandomUtils.GetRandomNumber(100), RandomUtils.GetRandomNumber(100)));
            for (int x = 0; x < 3; x++)
            {
                Pen linePen = new Pen(Color.FromArgb(RandomUtils.GetRandomNumber(150) + fixedNumber, RandomUtils.GetRandomNumber(150) + fixedNumber, RandomUtils.GetRandomNumber(150) + fixedNumber), 1);
                g.DrawLine(linePen, new PointF(0.0F + RandomUtils.GetRandomNumber(20), 0.0F + RandomUtils.GetRandomNumber(height)), new PointF(0.0F + RandomUtils.GetRandomNumber(width), 0.0F + RandomUtils.GetRandomNumber(height)));
            }


            Matrix m = new Matrix();
            for (int x = 0; x < code.Length; x++)
            {
                m.Reset();
                m.RotateAt(RandomUtils.GetRandomNumber(30) - 15, new PointF(Convert.ToInt64(width * (0.10 * x)), Convert.ToInt64(height * 0.5)));
                g.Transform = m;
                drawBrush.Color = Color.FromArgb(RandomUtils.GetRandomNumber(150) + fixedNumber + 20, RandomUtils.GetRandomNumber(150) + fixedNumber + 20, RandomUtils.GetRandomNumber(150) + fixedNumber + 20);
                PointF drawPoint = new PointF(0.0F + RandomUtils.GetRandomNumber(4) + x * 20, 3.0F + RandomUtils.GetRandomNumber(3));
                g.DrawString(RandomUtils.GetRandomNumber(1) == 1 ? code[x].ToString() : code[x].ToString().ToUpper(), fonts[RandomUtils.GetRandomNumber(fonts.Length - 1)], drawBrush, drawPoint);
                g.ResetTransform();
            }


            double distort = RandomUtils.GetRandomNumber(5, 10) * (RandomUtils.GetRandomNumber(10) == 1 ? 1 : -1);

            using (Bitmap copy = (Bitmap)bitmap.Clone())
            {
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        int newX = (int)(x + (distort * Math.Sin(Math.PI * y / 84.5)));
                        int newY = (int)(y + (distort * Math.Cos(Math.PI * x / 54.5)));
                        if (newX < 0 || newX >= width)
                            newX = 0;
                        if (newY < 0 || newY >= height)
                            newY = 0;
                        bitmap.SetPixel(x, y, copy.GetPixel(newX, newY));
                    }
                }
            }
            drawBrush.Dispose();
            g.Dispose();

            return bitmap;
        }

        public string RandomCode 
        {
            get 
            {
                return this._randomCode;
            }
        }

        [FlagsAttribute]
        public enum Category
        {
            Numberic = 1,
            LatinLetters = 2
        }
    }
}
