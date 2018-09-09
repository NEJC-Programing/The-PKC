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

        private void FastColoredTextBox1_TextChanged(object sender, FastColoredTextBoxNS.TextChangedEventArgs e)
        {
            try
            {
                browser.LoadHtml(MarkDown.MD2HTML(fastColoredTextBox1.Text));
                //MessageBox.Show(MarkDown.MD2HTML(fastColoredTextBox1.Text,""));
            }
            catch { }
        }
    }
}
