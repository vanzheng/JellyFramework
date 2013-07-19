using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using Jelly.Utilities;

namespace Jelly.Drawing
{
    /// <summary>
    /// The making thumnail helper. 
    /// </summary>
    public static class Thumbnail
    {
        public static void MakeThumbnail(string sourceImage, string destinationImage, int width, int height)
        {
            if (string.IsNullOrWhiteSpace(sourceImage))
            {
                throw new ArgumentNullException("sourceImage");
            }

            if (!File.Exists(sourceImage)) 
            {
                throw new Exception("The source image doesn't exist.");
            }

            destinationImage = IOUtility.CreateDirectory(destinationImage);
            System.Drawing.Image originalImage = null;
            try
            {
                originalImage = Image.FromFile(sourceImage);
            }
            catch (OutOfMemoryException)
            {
                if (originalImage != null)
                    originalImage.Dispose();
                throw;
            }

            int ox = 0;
            int oy = 0;
            int ow = originalImage.Width;
            int oh = originalImage.Height;

            if (ow > oh)
            {
                oh = originalImage.Width * height / width;
                ox = 0;
                oy = (originalImage.Height - oh) / 2;
            }

            if (ow < oh)
            {
                oh = originalImage.Height;
                ow = originalImage.Height * width / height;
                oy = 0;
                ox = (originalImage.Width - ow) / 2;
            }

            //新建一个bmp图片
            Image bitmap = new System.Drawing.Bitmap(width, height);
            //新建一个画板
            Graphics g = Graphics.FromImage(bitmap);
            //设置高质量插值法
            g.InterpolationMode = InterpolationMode.High;
            //设置高质量,低速度呈现平滑程度
            g.SmoothingMode = SmoothingMode.HighQuality;
            //清空画布并以透明背景色填充
            g.Clear(Color.White);
            //在指定位置并且按指定大小绘制原图片的指定部分
            g.DrawImage(originalImage, new Rectangle(0, 0, width, height),
                 new Rectangle(ox, oy, ow, oh),
                 GraphicsUnit.Pixel);
            try
            {
                string extname = Path.GetExtension(destinationImage).ToLowerInvariant();
                ImageFormat ift = null;
                switch (extname)
                {
                    case ".jpg":
                    case ".jpeg":
                        ift = ImageFormat.Jpeg;
                        break;
                    case ".bmp":
                        ift = ImageFormat.Bmp;
                        break;
                    case ".gif":
                        ift = ImageFormat.Gif;
                        break;
                    case ".png":
                        ift = ImageFormat.Png;
                        break;
                    default:
                        ift = ImageFormat.Jpeg;
                        break;
                }
                bitmap.Save(destinationImage, ift);
            }
            catch (System.Exception)
            {
                //throw e;
            }
            finally
            {
                originalImage.Dispose();
                bitmap.Dispose();
                g.Dispose();
            }
        }

        public static void MakeThumbnail(string sourceImage, string destinationImage, int size)
        {
            MakeThumbnail(sourceImage, destinationImage, size, size);
        }
    }
}
