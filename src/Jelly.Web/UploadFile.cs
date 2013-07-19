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
        /// ����ļ���С
        /// </summary>
        public int MaxSize 
        {
            get { return _maxsize; }
            set { _maxsize = value; }
        }

        /// <summary>
        /// �������չ����ָ������չ�������ַ����������չ��|�ָ�
        /// </summary>
        public string AllowExt 
        {
            get { return _allowExt; }
            set { _allowExt = value; }
        }

        /// <summary>
        /// �ļ���С
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
        /// ����ļ���չ���Ƿ����ָ������չ������
        /// </summary>
        /// <param name="fileExt">�ļ�����չ��</param>
        /// <param name="allowExt">ָ������չ�������ַ����������չ��|�ָ�</param>
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
        /// �ϴ��ļ�
        /// </summary>
        /// <param name="postedFile">HttpPostedFile����</param>
        /// <param name="filepath">�ϴ���·��</param>
        public void Upload(HttpPostedFile postedFile, string filepath)
        {
            if (postedFile.ContentLength == 0)
                throw new System.Exception("��ѡ��Ҫ�ϴ����ļ�");

            this._filesize = postedFile.ContentLength;
            if (this._filesize > this._maxsize)
            {
                this._error = 1;
                this._errormessage = string.Format("�ϴ��ļ����ܴ���{0}", FormatSizeString(this._maxsize));
            }

            else
            {
                string fullname = postedFile.FileName;
                string filename = fullname.Substring(fullname.LastIndexOf("\\") + 1);
                string ext = filename.Substring(filename.LastIndexOf(".") + 1);
                if (!CheckFileExt(ext, this._allowExt))
                {
                    this._error = 2;
                    this._errormessage = string.Format("�ļ�����ֻ��Ϊ{0}", this._allowExt);
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
