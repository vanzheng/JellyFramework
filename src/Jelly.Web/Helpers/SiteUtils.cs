using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using Jelly.Helpers;

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

        public static void CreateFolders(string relativePath) 
        {
            if (!Directory.Exists(HttpContext.Current.Server.MapPath(relativePath)))
            {
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath(relativePath));
            }
        }

        #region Html Helpers

        /// <summary>
        /// Convert html entities to txt.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ToTxt(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }
            else
            {
                string temp = input;
                temp = Regex.Replace(temp, "&nbsp;", " ", RegexOptions.IgnoreCase);
                temp = Regex.Replace(temp, @"<\s*br\s*(/)?\s*>", "\r\n", RegexOptions.IgnoreCase);
                temp = Regex.Replace(temp, "&lt;", "<", RegexOptions.IgnoreCase);
                temp = Regex.Replace(temp, "&gt;", ">", RegexOptions.IgnoreCase);
                temp = Regex.Replace(temp, "&amp;", "&", RegexOptions.IgnoreCase);
                return temp;
            }
        }

        public static string ReplaceHtmlTag(string html)
        {
            if (string.IsNullOrEmpty(html))
            {
                return html;
            }

            html = Regex.Replace(html, @"<.[^>]*>", "", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"-->", "", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"<!--.*", "", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"&(nbsp|#160);", "", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"&#(\d+);", "", RegexOptions.IgnoreCase);

            return html;
        }

        public static string HtmlEncode(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return input;
            }

            return HttpUtility.HtmlEncode(input.Trim());
        }

        #endregion

        public static string ReplaceInString(string input, string badStr)
        {
            ExceptionManager.ThrowIfNull<ArgumentNullException>(badStr);

            if (string.IsNullOrWhiteSpace(input)) 
            {
                return input;
            }

            string temp = input;
            string[] badChars = badStr.Split('|');

            foreach (string s in badChars) 
            {
                temp = Regex.Replace(temp, s, string.Empty, RegexOptions.IgnoreCase);
            }

            return temp;
        }

        public static string ReplaceBadQuery(string  input)
        {
            string badStr = @"'|<|>|^|*";
            return ReplaceInString(input, badStr);
        }

        public static string GetSafeQuery(string paramName)
        {
            if (string.IsNullOrWhiteSpace(paramName)) 
            {
                return string.Empty;
            }

            string value = HttpContext.Current.Request.QueryString[paramName];
            if (!string.IsNullOrWhiteSpace(value))
            {
                return HttpUtility.UrlDecode(ReplaceBadQuery(value));
            }

            return value;
        }

        /// <summary>
        /// 棼止外部提交数据
        /// </summary>
        public static void CheckOutSubmit()
        {
            string server_v1 = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_REFERER"];
            string server_v2 = System.Web.HttpContext.Current.Request.ServerVariables["SERVER_NAME"];
            if (String.IsNullOrEmpty(server_v1) == true)
            {
                throw new Exception("禁止从外部提交数据");
            }
            server_v1 = server_v1.Substring(7, server_v2.Length);
            if (server_v1 != server_v2)
            {
                throw new Exception("禁止从站点外部提交数据，请不要乱改参数！");
            }
        }

        /// <summary>
        /// 获取参数名为Act的值，用于后台Action提交数据
        /// </summary>
        /// <returns></returns>
        public static string GetAction(string actName)
        {
            return GetAction(actName, true);
        }

        public static string GetAction(string actName, bool IsCheckOutSubmit)
        {
            if (IsCheckOutSubmit)
                CheckOutSubmit();
            return HttpContext.Current.Request.QueryString[actName];
        }

        /// <summary>
        /// 获取当前页
        /// </summary>
        /// <returns></returns>
        public static int GetPageIndex()
        {
            return GetPageIndex("page");
        }

        /// <summary>
        /// 获取当前页
        /// </summary>
        /// <param name="pageurl"></param>
        /// <returns></returns>
        public static int GetPageIndex(string paramName)
        {
            string page = ReplaceBadQuery(paramName);
            if (string.IsNullOrEmpty(page))
            {
                return 1;
            }
            else
            {
                return Convert.ToInt32(page);
            }
        }

        public static string BuildQueryUrl(string pageParamName)
        {
            string rawurl = HttpContext.Current.Request.RawUrl;
            if (!string.IsNullOrEmpty(rawurl))
            {
                rawurl = Regex.Replace(rawurl, pageParamName + @"=(\d+)?&?", "", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            }

            return rawurl;
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
