using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp.WinForms;
using CefSharp;
using FastColoredTextBoxNS;

namespace TPKC_GUI
{
    public partial class Form1 : Form
    {
        ChromiumWebBrowser browser;
        FastColoredTextBox fastColoredTextBox1;
        bool runjs = false;

        public Form1()
        {
            InitializeComponent();
            browser = new ChromiumWebBrowser("0.0.0.0")
            {
                Anchor = (AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom),
                Width = splitContainer1.Panel2.Width,
                Height = splitContainer1.Panel2.Height,
                Parent = splitContainer1.Panel2
            };            
            browser.Show();
            KeyPreview = true;
            browser.IsBrowserInitializedChanged += (sender, args) =>
            {
                if (args.IsBrowserInitialized)
                {
                    //MessageBox.Show("load");
                    browser.LoadHtml(Properties.Resources.LoadPage, true);
                    //browser.ShowDevTools();
                }

            };
            browser.LoadingStateChanged += (sender, args) =>
            {
                runjs = !args.IsLoading;
            };



            fastColoredTextBox1 = new FastColoredTextBox
            {
                Anchor = (AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom),
                Width = splitContainer1.Panel1.Width,
                Height = splitContainer1.Panel1.Height,
                Parent = splitContainer1.Panel1
            };
            fastColoredTextBox1.TextChanged += FastColoredTextBox1_TextChanged;
            fastColoredTextBox1.Show();

            
        }

        private void splittermoveing(object sender, SplitterCancelEventArgs e)
        {
            
        }
        bool not_init = true;
        private void FastColoredTextBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                //browser.LoadHtml(MarkDown.MD2HTML(fastColoredTextBox1.Text));
                string data = Convert.ToBase64String(Encoding.UTF8.GetBytes(MarkDown.MD2HTML(fastColoredTextBox1.Text, true)));
                //MessageBox.Show(data);
                //browser.ExecuteScriptAsync("newmd(\""+data+"\");");
                if (runjs && not_init)
                {
                    not_init = false;
                    browser.ExecuteScriptAsync(Properties.Resources.jquery_3_3_1_min);
                    browser.ExecuteScriptAsync(Properties.Resources.highlight_pack);
                }
                if (runjs)
                   browser.ExecuteScriptAsync("newmd('" + data+ "');");
                
                //MessageBox.Show(MarkDown.MD2HTML(fastColoredTextBox1.Text,""));
            }
            catch { }
        }

        private void Form1_KeyPress(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F12)
                browser.ShowDevTools();
        }
    }
}
