using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DotNet.Utilities.Input
{
    /// <summary>
    /// 处理输入字符串的处理判断逻辑
    /// </summary>
    public class InputHelper
    {
        public static string GetInputString(string text)
        {
            return string.IsNullOrEmpty(text) ? string.Empty : text.Trim();
        }
        public static string GetInputString(object obj)
        {
            if (obj == null) return string.Empty;
            return string.IsNullOrEmpty(obj.ToString()) ? string.Empty : obj.ToString().Trim();
        }
        public static string CleanInputString(string text)
        {
            //text = text.Trim();
            if (string.IsNullOrEmpty(text)) return null;

            text = Regex.Replace(text, "(\\s*<[b|B][r|R]\\s*/*>\\s*)+", "\r\n");	//<br><br/><br />
            text = Regex.Replace(text, "(\\s*&[n|N][b|B][s|S][p|P];\\s*)+", " ");	//&nbsp;
            //text = Regex.Replace(text, "((<>)*[<|>]/*)+", string.Empty);	//<>

            return string.IsNullOrEmpty(text) ? null : text;
        }

        public static string CleanInputStringEn(string text)
        {
            //text = text.Trim();
            if (string.IsNullOrEmpty(text)) return null;

            text = Regex.Replace(text, "(<[b|B][r|R]/*>)+)", "\n");	//<br>
            text = Regex.Replace(text, "(&[n|N][b|B][s|S][p|P];)+", " ");	//&nbsp;
            //text = Regex.Replace(text, "<(.|\\n)*?>", string.Empty);	//<>

            return string.IsNullOrEmpty(text) ? null : text;
        }

        public static byte GetInputByteInt(string text)
        {
            if (string.IsNullOrEmpty(text)) return 0;
            byte outResult;
            Byte.TryParse(text.Trim(), out outResult);
            return outResult;
        }

        public static short GetInputShortInt(string text)
        {
            if (string.IsNullOrEmpty(text)) return 0;
            short outResult;
            Int16.TryParse(text.Trim(), out outResult);
            return outResult;
        }

        public static int GetInputInt(string text)
        {
            if (string.IsNullOrEmpty(text)) return 0;
            int outResult;
            Int32.TryParse(text.Trim(), out outResult);
            return outResult;
        }
        public static double GetInputDouble(string text)
        {
            if (string.IsNullOrEmpty(text)) return 0;
            double outResult;
            double.TryParse(text, out outResult);
            return outResult;
        }
        public static float GetInputFloat(string text)
        {
            if (string.IsNullOrEmpty(text)) return 0;
            float outResult;
            float.TryParse(text, out outResult);
            return outResult;
        }
        public static int GetInputInt(object obj)
        {
            return obj == null ? 0 : GetInputInt(obj.ToString());
        }

        public static long GetInputLongInt(string text)
        {
            if (string.IsNullOrEmpty(text)) return 0;
            long outResult;
            Int64.TryParse(text.Trim(), out outResult);
            return outResult;
        }

        public static decimal GetInputDecimal(string text)
        {
            if (string.IsNullOrEmpty(text)) return 0;
            decimal outResult;
            Decimal.TryParse(text.Trim(), out outResult);
            return outResult;
        }

        public static DateTime GetInputDateTime(string text)
        {
            if (string.IsNullOrEmpty(text)) return DateTime.MinValue;
            DateTime outResult;
            DateTime.TryParse(text.Trim(), out outResult);
            return outResult;
        }
    }
}
