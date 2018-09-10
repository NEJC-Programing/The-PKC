﻿using Markdig;

namespace TPKC_GUI
{
    class MarkDown
    {
        public static string MDCSS { get; set; } = Properties.Resources.DefualtMDCSS;
        public static string MD2HTML(string MD)
        {
            return "<style>"+MDCSS+ "</style><div id=\"MD\" class=\"markdown-body\">" + Markdown.ToHtml(MD)+"</div>";
        }
        public static string MD2HTML(string MD,string CSS)
        {
            return "<style>" + CSS + "</style><div id=\"MD\" class=\"markdown-body\">" + Markdown.ToHtml(MD) + "</div>";
        }
    }
}
