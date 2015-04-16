using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CVista
{
    public partial class WrapWindow : Form
    {
        public WrapWindow()
        {
            InitializeComponent();
        }

        private void administraFeedsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RssContactsDialog rssDialog = new RssContactsDialog();
            rssDialog.MdiParent = this;
            rssDialog.Show();
        }

        private void administraItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StoredEntriesDialog steDialog = new StoredEntriesDialog();
            steDialog.MdiParent = this;
            steDialog.Show();
        }

        private void ayudaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Llamar a un archivo chm externo creado con SandCastle
        }

        private void creditosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreditosDialog crdtDialog = new CreditosDialog();
            crdtDialog.MdiParent = this;
            crdtDialog.Show();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
