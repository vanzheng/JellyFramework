using System.Web;
using System.Web.UI;


namespace Jelly.Web.Utilities
{
    /// <summary>
    /// 封装常用Js代码
    /// </summary>
    public class JavascriptUtility
    {
        /// <summary>
        /// 弹出消息框
        /// </summary>
        /// <param name="msg"></param>
        public static void ShowMsg(string msg)
        {
            string js = @"<script type='text/javascript'>
                    alert('" + msg + "');</script>";
            HttpContext.Current.Response.Write(js);
        }

        public static void ShowMsg(string msg, int go)
        {
            string js = @"<script type='text/javascript'>alert('" + msg + "'); window.history.go(" + go + ")</script>";
            HttpContext.Current.Response.Write(js);
        }

        /// <summary>
        /// 弹出消息框并且转向到新的URL
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="toURL"></param>
        public static void ShowMsg(string msg, string toURL)
        {
            string js = "<script type='text/javascript'>alert('{0}');window.location.replace('{1}')</script>";
            HttpContext.Current.Response.Write(string.Format(js, msg, toURL));
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// 回到历史页面
        /// </summary>
        /// <param name="value">-1/1</param>
        public static void GoHistory(int value)
        {
            string js = @"<script type='text/javascript'>
                    history.go({0});  
                  </script>";
            HttpContext.Current.Response.Write(string.Format(js, value));
        }

        /// <summary>
        /// 关闭当前窗口
        /// </summary>
        public static void CloseWindow()
        {
            string js = @"<script type='text/javascript'>
                    parent.opener=null;window.close();  
                  </script>";
            HttpContext.Current.Response.Write(js);
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// 刷新父窗口
        /// </summary>
        /// <param name="url"></param>
        public static void RefreshParent(string url)
        {
            string js = @"<script type='text/javascript'>
                    window.opener.location.href='" + url + "';window.close();</script>";
            HttpContext.Current.Response.Write(js);
        }


        /// <summary>
        /// 刷新打开窗口
        /// </summary>
        public static void RefreshOpener()
        {
            string js = @"<script type='text/javascript'>
                    opener.location.reload();
                  </script>";
            HttpContext.Current.Response.Write(js);
        }


        /// <summary>
        /// 打开指定大小的新窗体
        /// </summary>
        /// <param name="url"></param>
        /// <param name="width"></param>
        /// <param name="heigth"></param>
        /// <param name="top"></param>
        /// <param name="left"></param>
        public static void OpenWindow(string url, int width, int heigth, int top, int left)
        {
            string js = @"<script type='text/javascript'>window.open('" + url + @"','','height=" + heigth + ",width=" + width + ",top=" + top + ",left=" + left + ",location=no,menubar=no,resizable=yes,scrollbars=yes,status=yes,titlebar=no,toolbar=no,directories=no');</script>";

            HttpContext.Current.Response.Write(js);
        }


        /// <summary>
        /// 转向指定的Url
        /// </summary>
        /// <param name="url"></param>
        public static void LocationNewHref(string url)
        {
            string js = @"<script type='text/javascript'>
                    window.location.replace('{0}');
                  </script>";
            js = string.Format(js, url);
            HttpContext.Current.Response.Write(js);
        }

        /// <summary>
        /// 打开指定大小位置的模式对话框
        /// </summary>
        /// <param name="url"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="top"></param>
        /// <param name="left"></param>
        public static void ShowModalDialog(string url, int width, int height, int top, int left)
        {
            string features = "dialogWidth:" + width.ToString() + "px"
                + ";dialogHeight:" + height.ToString() + "px"
                + ";dialogLeft:" + left.ToString() + "px"
                + ";dialogTop:" + top.ToString() + "px"
                + ";center:yes;help=no;resizable:no;status:no;scroll=yes";
            string js = @"<script type='text/javascript'>							
							showModalDialog('" + url + "','','" + features + "');</script>";
            HttpContext.Current.Response.Write(js);
        }

        /// <summary>
        /// 停留指定时间后，跳转到指定页
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="goUrl"></param>
        /// <param name="second"></param>
        public static void TipAndRedirect(string msg, string goUrl, string second)
        {
            HttpContext.Current.Response.Write("<meta http-equiv='refresh' content='" + second + ";url=" + goUrl + "'>");
            HttpContext.Current.Response.Write("<br/><br/><p align=center><div style=\"size:12px\">&nbsp;&nbsp;&nbsp;&nbsp;" + msg.Replace("!", "") + ",页面2秒内跳转!<br/><br/>&nbsp;&nbsp;&nbsp;&nbsp;<a href=\"" + goUrl + "\">如果没有跳转，请点击!</a></div></p>");
            HttpContext.Current.Response.End();
        }




        //-------------------------------------------新版本----------------------------------------------//
        /// <summary>
        /// 弹出消息框
        /// </summary>
        /// <param name="msg"></param>
        public static void ShowMsg(string msg, Page page)
        {
            string js = @"<script type='text/javascript'>
                    alert('" + msg + "');</script>";
            if (page.ClientScript.IsStartupScriptRegistered(page.GetType(), "Alert"))
                page.ClientScript.RegisterStartupScript(page.GetType(), "Alert", js);
        }

        public static void ShowMsg(string msg, int go, Page page)
        {
            string js = @"<script type='text/javascript'>alert('" + msg + "'); window.history.go(" + go + ")</script>";
            if (page.ClientScript.IsStartupScriptRegistered(page.GetType(), "Alert2"))
                page.ClientScript.RegisterStartupScript(page.GetType(), "Alert2", js);
        }

        /// <summary>
        /// 弹出消息框并且转向到新的URL
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="toURL"></param>
        public static void ShowMsg(string msg, string toURL, Page page)
        {
            string js = @"<script type='text/javascript'>alert('{0}');window.location.replace('{1}')</script>";
            if (page.ClientScript.IsStartupScriptRegistered(page.GetType(), "replace"))
                page.ClientScript.RegisterStartupScript(page.GetType(), "replace", js);

        }

        /// <summary>
        /// 回到历史页面
        /// </summary>
        /// <param name="value">-1/1</param>
        public static void GoHistory(int value, Page page)
        {
            string js = @"<script type='text/javascript'>
                    history.go({0});  
                  </script>";
            if (page.ClientScript.IsStartupScriptRegistered(page.GetType(), "gohistory"))
                page.ClientScript.RegisterStartupScript(page.GetType(), "gohistory", js);

        }

        /// <summary>
        /// 关闭当前窗口
        /// </summary>
        public static void CloseWindow(Page page)
        {
            string js = @"<script type='text/javascript'>
                    parent.opener=null;window.close();  
                  </script>";
            if (page.ClientScript.IsStartupScriptRegistered(page.GetType(), "close"))
                page.ClientScript.RegisterStartupScript(page.GetType(), "close", js);
        }

        /// <summary>
        /// 刷新父窗口
        /// </summary>
        /// <param name="url"></param>
        public static void RefreshParent(string url, Page page)
        {
            string js = @"<script type='text/javascript'>
                    window.opener.location.href='" + url + "';window.close();</script>";
            if (page.ClientScript.IsStartupScriptRegistered(page.GetType(), "refresh"))
                page.ClientScript.RegisterStartupScript(page.GetType(), "refresh", js);

        }


        /// <summary>
        /// 刷新打开窗口
        /// </summary>
        public static void RefreshOpener(Page page)
        {
            string js = @"<script type='text/javascript'>
                    opener.location.reload();
                  </script>";
            if (page.ClientScript.IsStartupScriptRegistered(page.GetType(), "refresh2"))
                page.ClientScript.RegisterStartupScript(page.GetType(), "refresh2", js);

        }


        /// <summary>
        /// 打开指定大小的新窗体
        /// </summary>
        /// <param name="url"></param>
        /// <param name="width"></param>
        /// <param name="heigth"></param>
        /// <param name="top"></param>
        /// <param name="left"></param>
        public static void OpenWindow(string url, int width, int heigth, int top, int left, Page page)
        {
            string js = @"<script type='text/javascript'>window.open('" + url + @"','','height=" + heigth + ",width=" + width + ",top=" + top + ",left=" + left + ",location=no,menubar=no,resizable=yes,scrollbars=yes,status=yes,titlebar=no,toolbar=no,directories=no');</script>";

            if (page.ClientScript.IsStartupScriptRegistered(page.GetType(), "openwindow"))
                page.ClientScript.RegisterStartupScript(page.GetType(), "openwindow", js);
        }


        /// <summary>
        /// 转向指定的Url
        /// </summary>
        /// <param name="url"></param>
        public static void LocationNewHref(string url, Page page)
        {
            string js = @"<script type='text/javascript'>
                    window.location.replace('{0}');
                  </script>";
            js = string.Format(js, url);
            if (page.ClientScript.IsStartupScriptRegistered(page.GetType(), "location"))
                page.ClientScript.RegisterStartupScript(page.GetType(), "location", js);

        }

        /// <summary>
        /// 打开指定大小位置的模式对话框
        /// </summary>
        /// <param name="url"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="top"></param>
        /// <param name="left"></param>
        public static void ShowModalDialog(string url, int width, int height, int top, int left, Page page)
        {
            string features = "dialogWidth:" + width.ToString() + "px"
                + ";dialogHeight:" + height.ToString() + "px"
                + ";dialogLeft:" + left.ToString() + "px"
                + ";dialogTop:" + top.ToString() + "px"
                + ";center:yes;help=no;resizable:no;status:no;scroll=yes";
            string js = @"<script type='text/javascript'>							
							showModalDialog('" + url + "','','" + features + "');</script>";
            if (page.ClientScript.IsStartupScriptRegistered(page.GetType(), "showDialog"))
                page.ClientScript.RegisterStartupScript(page.GetType(), "showDialog", js);

        }


    }
}
