using System;
using System.Text;
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
        AutocompleteMenu autocomplete;
        System.Timers.Timer timer;
        
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
            //fastColoredTextBox1.KeyDown += fctb_KeyDown;

            autocomplete = new AutocompleteMenu(fastColoredTextBox1);
            autocomplete.Items.MaximumSize = new System.Drawing.Size(200, 300);
            autocomplete.Items.Width = 200;

            fastColoredTextBox1.Show();

            timer = new System.Timers.Timer(2000)
            {
                AutoReset = true,
                Enabled = true
            };
            timer.Elapsed += Timer_Elapsed;
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                autocomplete.Items.SetAutocompleteItems(TPKC.APIs.Text.Suggest(fastColoredTextBox1.Text));
            }
            catch { }
            MethodInvoker mi = new MethodInvoker(() => autocomplete.Show(true) );

            if (autocomplete.InvokeRequired)
                autocomplete.Invoke(mi);
            else mi.Invoke();
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

        private void fctb_KeyDown(object sender, KeyEventArgs e)
        {
            timer.Stop();
            timer.Start();
            if (e.KeyData == (Keys.Control | Keys.K))
            {
                try
                {
                    autocomplete.Items.SetAutocompleteItems(TPKC.APIs.Text.Suggest(fastColoredTextBox1.Text));
                    autocomplete.Show(true);
                    e.Handled = true;
                }
                catch { }
            }
        }
    }
}
