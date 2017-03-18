using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Web.Script.Serialization;

namespace DotNet.Utilities.seo
{
    /// <summary>
    /// 专门给各大搜索引擎提供操作帮助的类
    /// </summary>
    public class SeoHelper
    {
        /// <summary>
        ///直接将提供的Url发送到Ping百度http://ping.baidu.com/ping.html
        /// </summary>
        /// <param name="url">要发送的url注意带上http://</param>
        /// <returns>成功true 否则为False</returns>
        public static Boolean PingBaidu(string url)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("<?xml version=\"1.0\"?>");
                sb.Append("<methodCall>");
                sb.Append("<methodName>weblogUpdates.ping</methodName>");
                sb.Append("<params>");
                sb.Append("<param>");
                sb.Append("<value><string>" + url + "</string></value>");
                sb.Append("</param><param><value><string>" + url + "</string></value>");
                sb.Append("</param>");
                sb.Append("</params>");
                sb.Append("</methodCall>");

                HttpHelper http = new HttpHelper();
                HttpItem item = new HttpItem()
                {
                    URL = "http://ping.baidu.com/ping/RPC2",//URL     必需项
                    Method = "POST",//URL     可选项 默认为Get
                    Referer = "http://ping.baidu.com/ping.html",//来源URL     可选项
                    Postdata = sb.ToString(),//Post数据     可选项GET时不需要写
                    ProtocolVersion = HttpVersion.Version10,
                };
                HttpResult result = http.GetHtml(item);

                if (result.Html.Contains("<int>0</int>"))
                {
                    return true;
                }
            }
            catch { }
            return false;
        }

        /// <summary>
        ///直接将提供的Url提交给百度原创提交接口，需要自行申请Taken
        /// </summary>
        /// <param name="curl">要发送的url注意带上http://</param>
        /// <param name="token">TzIJxrHBBTH9VdsX默认的Token值</param>
        /// <returns>成功true 否则为False</returns>
        public static OriginalModel OriginalPingBaidu(string curl, string token = "TzIJxrHBBTH9VdsX")
        {
            string url = string.Format("http://data.zz.baidu.com/urls?site={0}&token={1}", new Uri(curl).Host, token);
            HttpHelper http = new HttpHelper();
            HttpItem item = new HttpItem()
            {
                URL = url,//URL     必需项
                Method = "POST",//URL     可选项 默认为Get
                Referer = curl,//来源URL     可选项
                Postdata = curl,//Post数据     可选项GET时不需要写
                ProtocolVersion = HttpVersion.Version10,
                ContentType = "text/plain",
                UserAgent = "curl/7.12.1"
            };
            HttpResult result = http.GetHtml(item);

            JavaScriptSerializer jss = new JavaScriptSerializer();
            var model = (OriginalModel)jss.Deserialize<OriginalModel>(result.Html);

            return model;
            //if (result.Html.Contains("\"success\":1"))
            //{
            //    return true;
            //}

            //return false;
        }
        public class OriginalModel
        {
            public int remain { get; set; }
            public int success { get; set; }
        }
    }
}
