using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using CNegocio.Utils;

namespace CVista
{
    public partial class StoredEntriesDialog : Form
    {
        protected int itemSel;

        public StoredEntriesDialog()
        {
            this.itemSel = -1;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            this.cargaVentana();
        }

        private void StoredEntriesDialog_Load(object sender, EventArgs e)
        {
            this.cargaCombo();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int indexSel = (int)this.dataGridView1.CurrentCell.RowIndex;

            if (indexSel <= -1)
            {
                string msg = "No hay items seleccionados";
                MessageBox.Show(msg);
            }
            else
            {
                int iditem = (int)this.dataGridView1.Rows[indexSel].Cells[0].Value;

                if (MessageBox.Show("¿Estas seguro de que desea eliminar este Item?", "Eliminar Item", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    if (Utils.deleteItem(iditem) == 1)
                    {
                        MessageBox.Show("Item eliminado", "Eliminar", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                        this.cargaCombo();
                        this.cargaVentana();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo elimina el Item", "Eliminar", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                    }
                }
            }
        }


        private void cargaCombo()
        {
            this.itemSel = -1;
            this.comboBox1.Items.Clear();
            this.comboBox1.Items.Add("Todos los Items");

            foreach (string item in Utils.retrieveFeedsWithItems())
            {
                this.comboBox1.Items.Add(item);
            }
        }

        private void cargaVentana()
        {
            int indexList = this.comboBox1.SelectedIndex;
            if (indexList == -1)
            {
                // Borra el contenido del DataGridView
                this.dataGridView1.DataSource = null;
            }
            else if (indexList == 0)
            {
                // Carga el DataGridView con todos los items
                this.dataGridView1.DataSource = Utils.retrieveAllItems();
            }
            else
            {
                string textitem = this.comboBox1.SelectedItem.ToString();
                string[] splitTextItem = textitem.Split('-');

                string textid = splitTextItem[0].Trim();
                string texttitle = splitTextItem[1].Trim();
                int intId = Convert.ToInt32(textid);
                this.itemSel = intId;

                // Carga DataGridView con los Items del Feed intId
                this.dataGridView1.DataSource = Utils.retrieveItems(this.itemSel);
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.itemSel = e.RowIndex;
            string cabecera = (string)this.dataGridView1.Rows[this.itemSel].Cells[3].Value;
            string uriItem = (string)this.dataGridView1.Rows[this.itemSel].Cells[3].Value;

            BrowserDialog bd = new BrowserDialog(uriItem, cabecera);
            bd.MdiParent = WrapWindow.ActiveForm;
            bd.Show();
        }
    }
}
