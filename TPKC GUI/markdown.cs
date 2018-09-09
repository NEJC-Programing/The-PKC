using Markdig;

namespace TPKC_GUI
{
    class MarkDown
    {
        public static string MDCSS { get; set; } = Properties.Resources.DefualtMDCSS;
        public static string CODEBLOCKCSS { get; set; } = Properties.Resources.DefualtCodeBlockCSS;
        public static string HYLIGHTERJS { get; set; } = Properties.Resources.highlight_pack_js;
        public static string MD2HTML(string MD)
        {
            return "<style>" + CODEBLOCKCSS + MDCSS + "</style><script>" + HYLIGHTERJS + "</script><script>hljs.initHighlightingOnLoad();</script> <div id=\"MD\" class=\"markdown-body\">" + Markdown.ToHtml(MD) + "</div>";
        }
        public static string MD2HTML(string MD,string CSS)
        {
            return "<style>" + CODEBLOCKCSS + CSS + "</style><script>" + HYLIGHTERJS + "</script><script>hljs.initHighlightingOnLoad();</script> <div id=\"MD\" class=\"markdown-body\">" + Markdown.ToHtml(MD) + "</div>";
        }
    }
}
