using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace RedCoreApi_Test.Util
{
    public class MsgToHtml
    {
        static string scriptTag = "<script type=\"\" language=\"\">{0}</script>";

        public static void consoleLog(string msg)
        {
            string function = "console.log('{0}');";
            string log = string.Format(GenerateCodeFromFunction(function), msg);
            HttpContext.Current.Response.Write(log);
        }

        static string GenerateCodeFromFunction(string function)
        {
            return string.Format(scriptTag, function);
        }
    }
}