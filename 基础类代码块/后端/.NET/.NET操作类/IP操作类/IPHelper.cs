using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace DotNet.Utilities.IP
{
    public class IPHelper
    {
        /// <summary>
        /// 提取开启代理/cdn服务后的客户端真实IP
        /// </summary>
        /// <returns></returns>
        public static string GetTrueIP()
        {
            string ip = string.Empty;
            string X_Forwarded_For = HttpContext.Current.Request.Headers["X-Forwarded-For"];
            if (!string.IsNullOrWhiteSpace(X_Forwarded_For))
            {
                ip = X_Forwarded_For;
            }
            else
            {
                string CF_Connecting_IP = HttpContext.Current.Request.Headers["CF-Connecting-IP"];
                if (!string.IsNullOrWhiteSpace(CF_Connecting_IP))
                {
                    ip = CF_Connecting_IP;
                }
                else
                {
                    ip = HttpContext.Current.Request.UserHostAddress;
                }
            }
            return ip;
        }
    }
}
