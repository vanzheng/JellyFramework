using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jelly.Web
{
    public class UploadInfo
    {
        public bool IsSucceed { get; set; }
        public int FileSize { get; set; }
        public string FileName { get; set; }
        public string RelativeUploadPath { get; set; }
        public string SavePath { get; set; }
        public UploadFileErrorCode ErrorCode { get; set; }
        public string ExceptionMessage { get; set; }
    }

    public enum UploadFileErrorCode
    {
        Succeed,
        ExceedMaxSize,
        NotAllowedFileType,
        UploadFileNotFound,
        SavePathIsValid,
        Unknow
    }
}
