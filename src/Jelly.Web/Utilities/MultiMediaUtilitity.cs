using System.Text;

namespace Jelly.Web.Utilities
{
    /// <summary>
    /// Renders a various of multi media.
    /// </summary>
    public static class MultiMediaUtilitity
    {
        public static string RenderFlash(string src, int width, int height) 
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<object classid=\"clsid:D27CDB6E-AE6D-11cf-96B8-444553540000\" codebase=\"http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7,0,19,0\" width=\""+width+"\" height=\""+height+"\">");
            sb.Append("<param name=\"movie\" value=\""+src+"\" />");
            sb.Append("<param name=\"quality\" value=\"high\" />");
            sb.Append("<param name=\"wmode\" value=\"Transparent\" />");
            sb.Append("<embed src=\""+src+"\" quality=\"high\" wmode=\"transparent\" pluginspage=\"http://www.macromedia.com/go/getflashplayer\" type=\"application/x-shockwave-flash\" width=\""+width+"\" height=\""+height+"\" />");
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
