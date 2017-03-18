using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace DotNet.Utilities.Web
{
    /// <summary>
    /// 作用：用于操作Html标签的类
    /// </summary>
    public class FilterHelper
    {
        /// <summary>
        /// 返回过滤掉所有的Html标签后的字符串
        /// </summary>
        /// <param name="html">Html源码</param>
        /// <returns>过滤Html标签后的字符串</returns>
        public static string FilterAllHtml(string html)
        {
            string filter = "<[\\s\\S]*?>";
            html = URLDecode(html);
            if (Regex.IsMatch(html, filter))
            {
                html = Regex.Replace(html, filter, "");
            }
            filter = "[<>][\\s\\S]*?";
            if (Regex.IsMatch(html, filter))
            {
                html = Regex.Replace(html, filter, "");
            }
            return html;
        }

        /// <summary>
        /// 检查是否有Html标签
        /// </summary>
        /// <param name="html">Html源码</param>
        /// <returns>存在为True</returns>
        public static bool CheckHtml(string html)
        {
            html = URLDecode(html);
            string filter = "<[\\s\\S]*?>";

            if (Regex.IsMatch(html, filter))
            {
                return true;
            }
            filter = "[<>][\\s\\S]*?";
            if (Regex.IsMatch(html, filter))
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 对Html进行解码
        /// </summary>
        /// <param name="html">html</param>
        /// <returns></returns>
        public static string URLDecode(string html)
        {
            return HttpUtility.UrlDecode(html, Encoding.UTF8);
        }
    }
}
