using System;
using System.Web;
using Jelly.Helpers;
using Jelly.Web.Helpers;

namespace Jelly.Web
{
    public class UploadFileManager
    {
        /// <summary>
        /// Check the file name extension is allowed or not.
        /// </summary>
        /// <param name="fileExt">The file name extension</param>
        /// <param name="allowedExt">The allowed extension name list, separate by "|" symbol.</param>
        /// <returns></returns>
        public static bool CheckFileExt(string fileExt, string allowedExt)
        {
            if (allowedExt == null || string.IsNullOrWhiteSpace(allowedExt) || allowedExt.Equals("*")) 
            {
                return true;
            }

            string[] arrAllowedExts = allowedExt.Split(',');
            foreach (string ext in arrAllowedExts) 
            {
                if (fileExt == ext) 
                {
                    return true;
                }
            }
            
            return false;
        }

        /// <summary>
        /// Upload the file.
        /// </summary>
        /// <param name="postedFile">The HttpPostedFile object.</param>
        /// <param name="relativeUploadPath">The relative upload file path.</param>
        /// <param name="maxSize">The allowed maximum size</param>
        /// <param name="allowedExts">The allowed extension name list, separate by "|" symbol.</param>
        /// <param name="autoGenerateName">Is auto generate file name or not.</param>
        /// <returns>The UploadInfo object.</returns>
        public static UploadInfo Upload(HttpPostedFile postedFile, string relativeUploadPath, int maxSize, string allowedExts, bool autoGenerateName = true)
        {
            UploadInfo uploadInfo = new UploadInfo();

            if (postedFile != null || postedFile.ContentLength > 0)
            {
                uploadInfo.FileSize = postedFile.ContentLength;

                if (uploadInfo.FileSize > maxSize)
                {
                    uploadInfo.IsSucceed = false;
                    uploadInfo.ErrorCode = UploadFileErrorCode.ExceedMaxSize;
                }
                else
                {
                    string fullName = postedFile.FileName;
                    string fileName = fullName.Substring(fullName.LastIndexOf("\\") + 1);
                    string ext = fileName.Substring(fileName.LastIndexOf(".") + 1);

                    if (autoGenerateName)
                    {
                        string randomName = DateTime.Now.ToString("yyyyMMddhhmmssfff") + RandomUtils.GetRandomNumber(10000, 99999).ToString();
                        uploadInfo.FileName = randomName + ext;
                    }
                    else 
                    {
                        uploadInfo.FileName = fileName;
                    }

                    if (!CheckFileExt(ext, allowedExts))
                    {
                        uploadInfo.IsSucceed = false;
                        uploadInfo.ErrorCode = UploadFileErrorCode.NotAllowedFileType;
                    }
                    else
                    {
                        try
                        {
                            string path = IOUtils.EnsurePathEndSlash(relativeUploadPath);
                            SiteUtils.CreateFolders(path);
                            uploadInfo.RelativeUploadPath = path;
                            uploadInfo.SavePath = SiteUtils.MapPath(path + uploadInfo.FileName);
                            postedFile.SaveAs(uploadInfo.SavePath);
                            uploadInfo.IsSucceed = true;
                            uploadInfo.ErrorCode = UploadFileErrorCode.Succeed;
                        }
                        catch (Exception ex) 
                        {
                            uploadInfo.IsSucceed = false;
                            uploadInfo.ErrorCode = UploadFileErrorCode.Unknow;
                            uploadInfo.ExceptionMessage = ex.Message;
                        }
                    }
                }
            }
            else 
            {
                uploadInfo.IsSucceed = false;
                uploadInfo.ErrorCode = UploadFileErrorCode.UploadFileNotFound;
            }

            return uploadInfo;
        }
    }
}
