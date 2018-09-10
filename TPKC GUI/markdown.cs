using Markdig;

namespace TPKC_GUI
{
    class MarkDown
    {
        public static string MDCSS { get; set; } = "";
        public static string MD2HTML(string MD, bool pure = false)
        {
            if (pure)
                return Markdown.ToHtml(MD);
            else 
                return "<style>"+MDCSS+ "</style><div id=\"MD\" class=\"markdown-body\">" + Markdown.ToHtml(MD)+"</div>";
        }
        public static string MD2HTML(string MD,string CSS)
        {
            return "<style>" + CSS + "</style><div id=\"MD\" class=\"markdown-body\">" + Markdown.ToHtml(MD) + "</div>";
        }
    }
}
