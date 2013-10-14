using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace Jelly.Drawing.Watermark
{
    /// <summary>
    /// 图片水印类
    /// </summary>
    public class ImageWatermark
    {
        private string _watermarkImage;
        private WatermarkPosition _markPosition = WatermarkPosition.Center;
        private int[] sizes = new int[] { 20, 18, 16, 14, 12, 10, 8, 6, 4 };

        public ImageWatermark(string watermarkImage, WatermarkPosition markPosition)
        {
            this._watermarkImage = watermarkImage;
            this._markPosition = markPosition;
        }

        public WatermarkPosition MarkPosition
        {
            get { return this._markPosition; }
            set { this._markPosition = value; }
        }

        /// <summary>
        /// 添加图片水印
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

            //获得水印图像
            Image markImg = Image.FromFile(this._watermarkImage);

            //获取原图
            Image img = Image.FromFile(sourceImage);

            //创建颜色矩阵
            float[][] colorMatrixElements = {
                new float[] {1.0f,  0.0f,  0.0f,  0.0f, 0.0f},
                new float[] {0.0f,  1.0f,  0.0f,  0.0f, 0.0f},
                new float[] {0.0f,  0.0f,  1.0f,  0.0f, 0.0f},
                new float[] {0.0f,  0.0f,  0.0f,  0.3f, 0.0f},
                new float[] {0.0f,  0.0f,  0.0f,  0.0f, 1.0f}
            };
            ColorMatrix colorMatrix = new ColorMatrix(colorMatrixElements);
            //新建一个Image属性
            ImageAttributes imageAttributes = new ImageAttributes();
            //将颜色矩阵添加到属性
            imageAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Default);
            //生成位图作图区
            Bitmap newBitmap = new Bitmap(img.Width, img.Height, PixelFormat.Format24bppRgb);
            //设置分辨率
            newBitmap.SetResolution(img.HorizontalResolution, img.VerticalResolution);
            //创建Graphics
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(newBitmap);
            //消除锯齿
            g.SmoothingMode = SmoothingMode.AntiAlias;
            //拷贝原图到作图区
            g.DrawImage(img, new Rectangle(0, 0, img.Width, img.Height), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel);

            int xpos, ypos;
            int[] mypos;

            //如果原图过小
            if (markImg.Width > img.Width || markImg.Height > img.Height)
            {
                //对水印图片生成缩略图,缩小到原图得1/4
                string newWaterImageDirectory = Path.Combine(Path.GetDirectoryName(destinationImage), "WaterImages");
                string waterImageFileName = Path.GetFileName(this._watermarkImage);
                string newWaterImageFileName = waterImageFileName.Insert(waterImageFileName.LastIndexOf('.'), "_0.25");
                string newWaterImage = Path.Combine(newWaterImageDirectory, newWaterImageFileName);

                if (!File.Exists(newWaterImage)) 
                {
                    Thumbnail.MakeThumbnail(this._watermarkImage, newWaterImage, markImg.Width / 4, markImg.Height / 4);
                }
                Image new_markimg = Image.FromFile(newWaterImage);

                //添加水印
                mypos = GetImageXY(img, new_markimg);
                xpos = mypos[0];
                ypos = mypos[1];
                g.DrawImage(new_markimg, new Rectangle(xpos, xpos, new_markimg.Width, new_markimg.Height), 0, 0, new_markimg.Width, new_markimg.Height, GraphicsUnit.Pixel, imageAttributes);

                //释放缩略图
                new_markimg.Dispose();
                //释放Graphics
                g.Dispose();
                newBitmap.Save(destinationImage, ImageFormat.Jpeg);
            }

            //原图足够大
            else
            {
                mypos = GetImageXY(img, markImg);
                xpos = mypos[0];
                ypos = mypos[1];
                //添加水印
                g.DrawImage(markImg, new Rectangle(xpos, ypos, markImg.Width, markImg.Height), 0, 0, markImg.Width, markImg.Height, GraphicsUnit.Pixel, imageAttributes);
                //释放Graphics
                g.Dispose();
                newBitmap.Save(destinationImage, ImageFormat.Jpeg);
            }
        }

        /// <summary>
        /// 获取水印图片的坐标
        /// </summary>
        /// <param name="img1">原图对象</param>
        /// <param name="img2">水印图片对象</param>
        /// <returns></returns>
        private int[] GetImageXY(Image img1, Image img2)
        {
            int[] pos = new int[2];
            switch (this._markPosition)
            {
                case WatermarkPosition.TopLeft:
                    pos[0] = 10;
                    pos[1] = 10;
                    break;
                case WatermarkPosition.TopRight:
                    pos[0] = img1.Width - img2.Width - 10;
                    pos[1] = 10;
                    break;
                case WatermarkPosition.Center:
                    pos[0] = (img1.Width - img2.Width) / 2;
                    pos[1] = (img1.Height - img2.Height) / 2;
                    break;
                case WatermarkPosition.BottomRight:
                    pos[0] = img1.Width - img2.Width - 10;
                    pos[1] = img1.Height - img2.Width - 10;
                    break;
                case WatermarkPosition.BottomLeft:
                    pos[0] = 10;
                    pos[1] = img1.Height - img2.Height - 10;
                    break;
            }
            return pos;
        }
    }
}
