/// <summary>
    /// Cookies灏佽鎿嶄綔绫?
    /// </summary>
    public class Cookies
    {
        #region 鑾峰彇Cookie
        /// <summary>
        /// 鑾峰彇Cookie
        /// </summary>
        /// <param name="key">閿?/param>
        /// <param name="value">鍊?/param>
        /// <returns></returns>
        public static string getCookie(string key, string value)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[key];
            try
            {
                if (cookie != null)
                {
                    return HttpUtility.UrlDecode(cookie.Values[value]);
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region 璁剧疆Cookie
        /// <summary>
        /// 璁剧疆Cookie
        /// </summary>
        /// <param name="key">閿?/param>
        /// <param name="value">鍊?/param>
        /// <param name="time">杩囨湡鏃堕棿</param>
        /// <returns></returns>
        public static bool setCookie(string key, string value, double time)
        {
            try
            {
                string s = HttpUtility.UrlEncode(value);
                HttpCookie cookie = new HttpCookie(key)
                {
                    Expires = DateTime.Now.AddMinutes(time)
                };
                cookie.Values.Add("Value", HttpContext.Current.Server.UrlEncode(s));
                HttpContext.Current.Response.Cookies.Add(cookie);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region 鏇存柊Cookie
        /// <summary>
        /// 鏇存柊Cookie
        /// </summary>
        /// <param name="key">閿?/param>
        /// <param name="value">鍊?/param>
        /// <param name="time">杩囨湡鏃堕棿</param>
        /// <returns></returns>
        public static bool updateCookies(string key, string value, double time)
        {
            bool flag;
            try
            {
                HttpContext.Current.Response.Cookies[key]["Value"] = value;
                flag = setCookie(key, value, time);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return flag;
        }
        #endregion
    }
