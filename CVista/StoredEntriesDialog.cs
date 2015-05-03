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

namespace CVista
{
    public partial class StoredEntriesDialog : Form
    {
        public StoredEntriesDialog()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            string[] textitem = this.comboBox1.SelectedText.Split('-');

            if(this.comboBox1.SelectedIndex > -1)
            {
                string seltext = this.comboBox1.SelectedItem.ToString();
                string textid = seltext.Split('-')[0].Trim();
                string texttitle = seltext.Split('-')[1].Trim();
                int intId = Convert.ToInt32(textid);

                // Carga DataGridView
                this.dataGridView1.DataSource = Utils.retrieveItems(intId);
            }
        }

        private void StoredEntriesDialog_Load(object sender, EventArgs e)
        {
            foreach (string item in Utils.retrieveFeedsWithItems())
            {
                this.comboBox1.Items.Add(item);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
