using System;
using System.Collections.Generic;
using System.Text;
using System.CodeDom.Compiler;
using System.Reflection;

namespace DotNet.Utilities.JS
{
    /// <summary>
    /// 用于执行JS的类
    /// </summary>
    public class ExeJsHelper
    {

        public object GetMainResult(string js, string mainname)
        {
            CodeDomProvider _provider = new Microsoft.JScript.JScriptCodeProvider();
            Type _evaluateType;
            CompilerParameters parameters = new CompilerParameters();
            parameters.GenerateInMemory = true;
            CompilerResults results = _provider.CompileAssemblyFromSource(parameters, js);
            Assembly assembly = results.CompiledAssembly;
            string time = "";

            _evaluateType = assembly.GetType("aa.JScript");
            object[] w = new object[] { "123", time };

            object ww = _evaluateType.InvokeMember("getm32str", BindingFlags.InvokeMethod,
            null, null, w);
            return js;
        }

        ///// <summary>
        ///// 密码加密
        ///// </summary>
        ///// <param name="pass"></param>
        ///// <returns></returns>
        //public string EncodePass(string pass)
        //{
        //    ScriptControlClass sc = new ScriptControlClass();
        //    sc.UseSafeSubset = true;
        //    sc.Language = "JScript";
        //    sc.AddCode(Properties.Resources.QQRsa);  //从资源中读取js内容,也可以写成Js文件神马的.
        //    string str = sc.Run("rsaEncrypt", new object[] { pass }).ToString();
        //    return str;
        //}
    }
}
