using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using Jelly.Utilities;
using System.IO;

namespace Jelly.Drawing.Watermark
{
    /// <summary>
    /// Represents a text watermark.
    /// </summary>
    public class TextWatermark
    {
        private int[] sizes = new int[] { 20, 18, 16, 14, 12, 10, 8, 6, 4 };
        private TextWatermarkSettings _settings;
        private WatermarkPosition _markPosition = WatermarkPosition.Center;

        public TextWatermark(TextWatermarkSettings settings, WatermarkPosition markPosition)
        {
            this._settings = settings;
            this._markPosition = markPosition;
        }

        public TextWatermarkSettings TextWatermarkSettings 
        {
            get { return this._settings; }
            set { this._settings = value; }
        }

        public WatermarkPosition MarkPosition 
        {
            get { return this._markPosition; }
            set { this._markPosition = value; }
        }

        /// <summary>
        /// Makes the text watermark with given the <see cref="TextWatermarkSettings"/> object.
        /// </summary>
        public void Make(string sourceImage, string destinationImage)
        {
            if (string.IsNullOrWhiteSpace(sourceImage))
            {
                throw new ArgumentNullException("sourceImage");
            }

            string path = Path.GetDirectoryName(destinationImage);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            //放入Image对象，获取图片高度和宽度
            Image image = Image.FromFile(sourceImage);
            Bitmap b = new Bitmap(image.Width, image.Height, PixelFormat.Format24bppRgb);
            Graphics g = Graphics.FromImage(b);

            //设置高质量插值法
            g.SmoothingMode = SmoothingMode.HighQuality;
            //设置高质量,低速度呈现平滑程度
            g.InterpolationMode = InterpolationMode.High;
            //清空画布并以透明背景色填充
            g.Clear(Color.White);
            //在指定位置并且按指定大小绘制原图片的指定部分
            g.DrawImage(image, 0, 0, image.Width, image.Height);

            Font crFont = null;
            SizeF crSize = new SizeF();

            //探测出一个适合图片大小得字体大小，以适应水印文字大小得自适应
            for (int i = 0; i < 9; i++)
            {
                crFont = new Font(this._settings.TextFamily, sizes[i], this._settings.FontStyle);
                //测量文本大小
                crSize = g.MeasureString(this._settings.Text, crFont);

                if ((ushort)crSize.Width < (ushort)image.Width)
                    break;
            }

            float xpos = 0;
            float ypos = 0;

            //获取水印位置
            switch (this._markPosition)
            {
                case WatermarkPosition.TopLeft:
                    xpos = ((float)image.Width * (float).01) + (crSize.Width / 2);
                    ypos = (float)image.Height * (float).01;
                    break;
                case WatermarkPosition.TopRight:
                    xpos = ((float)image.Width * (float).99) - (crSize.Width / 2);
                    ypos = (float)image.Height * (float).01;
                    break;
                case WatermarkPosition.Center:
                    xpos = (float)image.Width - (crSize.Width / 2);
                    ypos = (float)image.Height - (crSize.Height / 2);
                    break;
                case WatermarkPosition.BottomRight:
                    xpos = ((float)image.Width * (float).99) - (crSize.Width / 2);
                    ypos = ((float)image.Height * (float).99) - crSize.Height;
                    break;
                case WatermarkPosition.BottomLeft:
                    xpos = ((float)image.Width * (float).01) + (crSize.Width / 2);
                    ypos = ((float)image.Height * (float).99) - crSize.Height;
                    break;
            }

            StringFormat StrFormat = new StringFormat();
            StrFormat.Alignment = StringAlignment.Center;  //居中字体

            SolidBrush semiTransBrush2 = new SolidBrush(this._settings.TextColor);
            g.DrawString(this._settings.Text, crFont, semiTransBrush2, xpos, ypos, StrFormat);

            b.Save(destinationImage, ImageFormat.Jpeg);

            semiTransBrush2.Dispose();
            g.Dispose();
        }
    }

    /// <summary>
    /// Represents a text watermark settings.
    /// </summary>
    public class TextWatermarkSettings
    {
        private string _text;
        private string _textFamily = "宋体";
        private FontStyle _fontStyle = FontStyle.Regular;
        private Color _textColor = Color.Black;

        /// <summary>
        /// Gets or sets the watermark text.
        /// </summary>
        public string Text
        {
            get { return this._text; }
            set { this._text = value; }
        }

        /// <summary>
        /// Gets or sets the watermark text family.
        /// </summary>
        public string TextFamily
        {
            get { return this._textFamily; }
            set { this._textFamily = value; }
        }

        /// <summary>
        /// 文字风格，默认正常
        /// </summary>
        public FontStyle FontStyle
        {
            get { return this._fontStyle; }
            set { this._fontStyle = value; }
        }

        /// <summary>
        /// 字体颜色
        /// </summary>
        public Color TextColor
        {
            get { return _textColor; }
            set { _textColor = value; }
        }
    }
}
