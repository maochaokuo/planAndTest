using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace planAndTest.web.Helper
{
    public class htmlHelper
    {
        public static string removeHtmlTags(string htmlInput
            , out string pureText)
        {
            // 
            string ret = "";
            HtmlDocument mainDoc = new HtmlDocument();
            mainDoc.LoadHtml(htmlInput);
            pureText = mainDoc.DocumentNode.InnerText;
            return ret;
        }
    }
}
