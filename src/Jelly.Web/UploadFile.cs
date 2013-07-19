using System;
using System.IO;
using System.Web;

namespace Jelly.Web
{
    public class UploadFile
    {
        private int _maxsize = 200 * 1024;
        private string _allowExt;
        private int _filesize = 0;
        private int _error = 0;
        private string _errormessage = string.Empty;
        private string _savefilename;

        public UploadFile() { }

        public UploadFile(int maxsize, string allowExt) 
        {
            this._maxsize = maxsize;
            this._allowExt = allowExt;
        }

        /// <summary>
        /// 最大文件大小
        /// </summary>
        public int MaxSize 
        {
            get { return _maxsize; }
            set { _maxsize = value; }
        }

        /// <summary>
        /// 允许的扩展名，指定的扩展名类型字符串，多个扩展用|分隔
        /// </summary>
        public string AllowExt 
        {
            get { return _allowExt; }
            set { _allowExt = value; }
        }

        /// <summary>
        /// 文件大小
        /// </summary>
        public int FileSize 
        {
            get { return _filesize; }
        }

        public int Error 
        {
            get { return _error; }
        }

        public string ErrorMessage 
        {
            get { return _errormessage; }
        }

        public string SaveFileName 
        {
            get { return _savefilename; }
        }

        /// <summary>
        /// 检查文件扩展名是否符合指定的扩展名类型
        /// </summary>
        /// <param name="fileExt">文件的扩展名</param>
        /// <param name="allowExt">指定的扩展名类型字符串，多个扩展用|分隔</param>
        /// <returns></returns>
        public bool CheckFileExt(string fileExt, string allowExt)
        {
            string[] arrFileExt = allowExt.Split(',');
            for (int i = 0; i < arrFileExt.Length; i++)
            {
                if (arrFileExt[i] == fileExt)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="postedFile">HttpPostedFile对象</param>
        /// <param name="filepath">上传的路径</param>
        public void Upload(HttpPostedFile postedFile, string filepath)
        {
            if (postedFile.ContentLength == 0)
                throw new System.Exception("请选择要上传的文件");

            this._filesize = postedFile.ContentLength;
            if (this._filesize > this._maxsize)
            {
                this._error = 1;
                this._errormessage = string.Format("上传文件不能大于{0}", FormatSizeString(this._maxsize));
            }

            else
            {
                string fullname = postedFile.FileName;
                string filename = fullname.Substring(fullname.LastIndexOf("\\") + 1);
                string ext = filename.Substring(filename.LastIndexOf(".") + 1);
                if (!CheckFileExt(ext, this._allowExt))
                {
                    this._error = 2;
                    this._errormessage = string.Format("文件类型只能为{0}", this._allowExt);
                }

                else
                {
                    Random r = new Random(Guid.NewGuid().GetHashCode());
                    string AutoFileName = DateTime.Now.ToString("yyyyMMddhhmmssfff") + r.Next(10000, 99999).ToString();
                    this._savefilename = filepath + AutoFileName + "." + ext;

                    if (!Directory.Exists(HttpContext.Current.Server.MapPath(filepath)))
                        Directory.CreateDirectory(HttpContext.Current.Server.MapPath(filepath));
                    postedFile.SaveAs(HttpContext.Current.Server.MapPath(this._savefilename));
                }
            }

        }

        private string FormatSizeString(int size) 
        {
            if (size <= 0)
                return "";
            else 
            {
                if (size > 0 && size < 1024)
                    return size + "B";
                else if (size >= 1024 && size < 1024 * 1024)
                    return size / 1024 + "K";
                else if (size >= 1024 * 1024 && size < 1024 * 1024 * 1024)
                    return size / (1024 * 1024) + "M";
                else if (size >= 1024 * 1024 * 1024)
                    return size / (1024 * 1024 * 1024) + "G";
                else
                    return "";
            }
        }
    }
}
