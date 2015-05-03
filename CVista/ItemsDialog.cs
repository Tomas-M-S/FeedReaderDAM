using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CNegocio.Utils;
using CNegocio.Classes;
using CNegocio.WBManager;

namespace CVista
{
    public partial class ItemsDialog : Form
    {
        protected string title;
        protected string uri;
        protected int id;
        protected RssFeed contact;
        private int ItemMargin;

        public ItemsDialog(string title, string uri, int id)
        {
            this.ItemMargin = 5;
            this.title = title;
            this.uri = uri;
            this.id = id;
            this.contact = Utils.retrieveRssFeedContact(uri);
            
            InitializeComponent();
        }

        private void ItemsDialog_Load(object sender, EventArgs e)
        {
            this.Text = this.title;
            this.label1.Text = this.contact.Title;
            this.label2.Text = this.contact.Description;
            this.label3.Text = this.contact.Link[0].uri;
            this.label4.Text = this.contact.LastBuildDate;

            this.listBox1.DrawMode = DrawMode.OwnerDrawVariable;

            foreach (Item item in this.contact.Items)
            {
                this.listBox1.Items.Add(item.ToString());
            }
        }

        private void listBox1_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            ListBox lst = sender as ListBox;
            string txt = (string)lst.Items[e.Index];

            SizeF txt_size = e.Graphics.MeasureString(txt, this.Font);

            e.ItemHeight = (int)txt_size.Height + 2 * ItemMargin;
            e.ItemWidth = (int)txt_size.Width;
        }

        private void listBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            ListBox lst = sender as ListBox;
            string txt = (string)lst.Items[e.Index];
            e.DrawBackground();
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                e.Graphics.DrawString(txt, this.Font,
                    SystemBrushes.HighlightText, e.Bounds.Left,
                        e.Bounds.Top + ItemMargin);
            }
            else
            {
                using (SolidBrush br = new SolidBrush(e.ForeColor))
                {
                    e.Graphics.DrawString(txt, this.Font, br,
                        e.Bounds.Left, e.Bounds.Top + ItemMargin);
                }
            }

            e.DrawFocusRectangle();
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            string cabecera = this.contact.Title;
            string uriItem = this.contact.Items[this.listBox1.SelectedIndex].Links[0].uri;
            BrowserDialog bd = new BrowserDialog(uriItem, cabecera);
            bd.MdiParent = WrapWindow.ActiveForm;
            bd.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int itemsel = this.listBox1.SelectedIndex;
            if (this.listBox1.SelectedIndex == -1)
            {
                MessageBox.Show("No hay ningún item seleccionado");
            }
            else
            {
                Item item = this.contact.Items[this.listBox1.SelectedIndex];
                if (Utils.saveItem(item, DateTime.Today,id) == 1)
                {
                    MessageBox.Show("Item guardado correctamente");
                }
                else
                {
                    MessageBox.Show("No se guardó el item");
                }
            }
        }
    }
}
