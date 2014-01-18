using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Jelly.Web.Helpers
{
    /// <summary>
    /// 封装网站Server,Application等对象
    /// </summary>
    public class SiteUtils
    {
        /// <summary>
        /// 获得当前页面客户端的IP
        /// </summary>
        /// <returns>当前页面客户端的IP</returns>
        public static string GetIP()
        {
            string result = String.Empty;
            result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrWhiteSpace(result))
            {
                result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }

            if (string.IsNullOrWhiteSpace(result))
            {
                result = HttpContext.Current.Request.UserHostAddress;
            }

            if (string.IsNullOrWhiteSpace(result) || !IsIPAddress(result))
            {
                return "0.0.0.0";
            }
            return result;
        }


        /// <summary>
        /// 判断是否是IP地址格式 0.0.0.0
        /// </summary>
        /// <param name="str1">待判断的IP地址</param>
        /// <returns>true or false</returns>
        public static bool IsIPAddress(string str1)
        {
            if (str1 == null || str1 == string.Empty || str1.Length < 7 || str1.Length > 15) return false;

            string regformat = @"^\d{1,3}[\.]\d{1,3}[\.]\d{1,3}[\.]\d{1,3}$";

            Regex regex = new Regex(regformat, RegexOptions.IgnoreCase);
            return regex.IsMatch(str1);
        }

        /// <summary>
        /// 获得当前完整Url地址
        /// </summary>
        /// <returns>当前完整Url地址</returns>
        public static string GetUrl()
        {
            return HttpContext.Current.Request.Url.ToString();
        }

        /// <summary>
        /// 获得当前页面的名称
        /// </summary>
        /// <returns>当前页面的名称</returns>
        public static string GetPageName()
        {
            string[] urlArr = HttpContext.Current.Request.Url.AbsolutePath.Split('/');
            return urlArr[urlArr.Length - 1].ToLower();
        }

        /// <summary>
        /// 返回表单或Url参数的总个数
        /// </summary>
        /// <returns></returns>
        public static int GetParamCount()
        {
            return HttpContext.Current.Request.Form.Count + HttpContext.Current.Request.QueryString.Count;
        }

        /// <summary>
        /// 上一次请求Url来源
        /// </summary>
        /// <returns></returns>
        public static string ReferrerUrl() 
        {
            if (HttpContext.Current.Request.UrlReferrer == null)
                return "";
            else
                return HttpContext.Current.Request.UrlReferrer.ToString();
        }

        public static string MapPath(string filepath) 
        {
            return HttpContext.Current.Server.MapPath(filepath);
        }

        /// <summary>
        /// 向字符串中添加?或&
        /// </summary>
        public static string JoinChar(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return str;
            else
            {
                if (str.Contains("?"))  //含有问号
                {
                    //?不是最后一个字符
                    if (str.LastIndexOf("?") < str.Length - 1)
                    {
                        if (str.LastIndexOf("&") < str.Length - 1)  //&不是最后一个字符
                            str += "&";
                    }
                }
                else
                    str += "?";
                return str;
            }
        }

        public static string GetStatusUrl(string url,string statusText) 
        {
            url = JoinChar(url);
            url = Regex.Replace(url,@"(\?|\&){1}status=.*(\&)?","",RegexOptions.IgnoreCase);
            return JoinChar(url) + "status=" + statusText;
        }

        public static string RenderFlash(string src, int width, int height)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<object classid=\"clsid:D27CDB6E-AE6D-11cf-96B8-444553540000\" codebase=\"http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7,0,19,0\" width=\"" + width + "\" height=\"" + height + "\">");
            sb.Append("<param name=\"movie\" value=\"" + src + "\" />");
            sb.Append("<param name=\"quality\" value=\"high\" />");
            sb.Append("<param name=\"wmode\" value=\"Transparent\" />");
            sb.Append("<embed src=\"" + src + "\" quality=\"high\" wmode=\"transparent\" pluginspage=\"http://www.macromedia.com/go/getflashplayer\" type=\"application/x-shockwave-flash\" width=\"" + width + "\" height=\"" + height + "\" />");
            sb.Append("</object>");
            return sb.ToString();
        }

        public static string RenderFlash(string src)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<object classid=\"clsid:D27CDB6E-AE6D-11cf-96B8-444553540000\" codebase=\"http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7,0,19,0\">");
            sb.Append("<param name=\"movie\" value=\"" + src + "\" />");
            sb.Append("<param name=\"quality\" value=\"high\" />");
            sb.Append("<param name=\"wmode\" value=\"Transparent\" />");
            sb.Append("<embed src=\"" + src + "\" quality=\"high\" wmode=\"transparent\" pluginspage=\"http://www.macromedia.com/go/getflashplayer\" type=\"application/x-shockwave-flash\" />");
            sb.Append("</object>");
            return sb.ToString();
        }

    }
}
