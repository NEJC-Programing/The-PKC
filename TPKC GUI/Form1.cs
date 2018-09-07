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

namespace TPKC_GUI
{
    public partial class Form1 : Form
    {
        ChromiumWebBrowser browser;
        public Form1()
        {
            InitializeComponent();
            browser = new ChromiumWebBrowser();
            browser.Anchor = (AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom);
            browser.Width = splitContainer1.Panel2.Width;
            browser.Height = splitContainer1.Panel2.Height;
            browser.Parent = splitContainer1.Panel2;
            browser.Show();
        }

        private void splittermoveing(object sender, SplitterCancelEventArgs e)
        {
            
        }

        private void TextChangedd(object sender, FastColoredTextBoxNS.TextChangedEventArgs e)
        {
            try
            {
                browser.LoadHtml(MarkDown.MD2HTML(fastColoredTextBox1.Text));
            }
            catch { }
        }
    }
}
