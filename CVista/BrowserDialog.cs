using System;
using System.Windows.Forms;

namespace CVista
{
    public partial class BrowserDialog : Form
    {
        protected string url;
        protected string titulo;

        public BrowserDialog(string url, string titulo)
        {
            this.url = url;
            this.titulo = titulo;
            InitializeComponent();
        }

        private void BrowserDialog_Load(object sender, EventArgs e)
        {
            //Console.WriteLine(this.url);
            this.Text = this.titulo;
            this.webBrowser1.Url = new Uri(this.url);
        }
    }
}
