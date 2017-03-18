using System;
using System.Collections.Generic;
using System.Text;
using DotNet.Utilities.Json;
namespace DotNet.Utilities.baiduApi
{
    /// <summary>
    /// 百度经纬度转化帮助类
    /// </summary>
    public class BaiduMapHelper
    {
        /// <summary>
        /// 百度地理位置转化URl
        /// </summary>
        private static string CoordsUrl = "http://api.map.baidu.com/geoconv/v1/";
        /// <summary>
        /// 根据输入的经纬度List转化为百度地图的经纬度List，一一对应
        /// </summary>
        /// <param name="citem">要转化的经纬度List</param>
        /// <returns>转化后的经纬度List</returns>
        public static List<CoordsItem> GetCoords(List<CoordsItem> citem)
        {
            HttpHelper http = new HttpHelper();
            string strCoords = ListToString(citem);
            HttpItem item = new HttpItem()
            {
                URL = string.Format("{0}?coords={1}&from=3&to=5&ak=17bc43866bbd51f7507e0c618f890e64", CoordsUrl, strCoords),//URL     必需项    
            };
            HttpResult result = http.GetHtml(item);
            string html = result.Html;
            citem = StringToList(html);
            return citem;
        }

        /// <summary>
        /// 根据输入的经纬度转化为百度地图的经纬度
        /// </summary>
        /// <param name="citem">要转化的经纬度</param>
        /// <returns>转化后的经纬度List</returns>
        public static CoordsItem GetCoords(CoordsItem citem)
        {
            List<CoordsItem> list = new List<CoordsItem>();
            list.Add(citem);
            list = GetCoords(list);
            if (list != null & list.Count > 0)
            {
                return list[0];
            }
            return null;
        }
        /// <summary>
        /// 根据List转化为相应的字符串经纬度
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private static string ListToString(List<CoordsItem> item)
        {
            StringBuilder strCoords = new StringBuilder();
            foreach (var obj in item)
            {
                if (string.IsNullOrEmpty(strCoords.ToString()))
                {
                    strCoords.Append(obj.X + "," + obj.Y);
                }
                else
                {
                    strCoords.Append(";" + obj.X + "," + obj.Y);
                }
            }
            return strCoords.ToString();
        }
        //{"status":0,"result":[{"x":114.22539195429,"y":29.581585367458},{"x":114.2253919533,"y":29.581585366942}]}
        //{"status":24,"message":"param error:coords format error","result":[]}
        /// <summary>
        /// 根据Json格式转化为List
        /// </summary>
        /// <param name="str">Json字符串</param>
        /// <returns></returns>
        private static List<CoordsItem> StringToList(string str)
        {
            List<CoordsItem> item = new List<CoordsItem>();
            //json to list
            object obj = JsonHelper.jsonDes<CoordsList>(str);

            CoordsList c = (CoordsList)obj;
            return c.result;
        }
    }
    /// <summary>
    /// 返回参数类
    /// </summary>
    public class CoordsList
    {
        /// <summary>
        /// 状态0为成功其他不成功
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// result不为0时才显示
        /// </summary>
        public string message { get; set; }
        /// <summary>
        /// 经纬度列表
        /// </summary>
        public List<CoordsItem> result { get; set; }
    }
    /// <summary>
    /// 经纬度类
    /// </summary>
    public class CoordsItem
    {
        /// <summary>
        /// 经
        /// </summary>
        public double X { get; set; }
        /// <summary>
        /// 纬
        /// </summary>
        public double Y { get; set; }
    }
}
