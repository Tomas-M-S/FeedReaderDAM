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
using CDatos.DBEntities;

namespace CVista
{
    public partial class RssContactsDialog : Form
    {
        public RssContactsDialog()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                uint interv = Convert.ToUInt32(textBox2.Text);
                Properties.Settings.Default.intervalo = interv;
                Properties.Settings.Default.Save();
                textBox1.Text = Properties.Settings.Default.intervalo.ToString();
                textBox2.Text = String.Empty;
            }
            catch (OverflowException ex)
            {
                String msg = ex.Message + Environment.NewLine + " Debe de ser un número comprendido entre De 0 y 4294967295.";
                MessageBox.Show(msg);
            }
            catch (FormatException ex)
            {
                String msg = ex.Message + Environment.NewLine + " Debe de ser un número.";
                MessageBox.Show(msg);
            }
        }

        private void RssContactsDialog_Leave(object sender, EventArgs e)
        {
        }

        private void RssContactsDialog_Load(object sender, EventArgs e)
        {
            textBox1.Text = Properties.Settings.Default.intervalo.ToString();
            dataGridView1.DataSource = Utils.retrieveRssContact();
            dataGridView2.DataSource = Utils.retrieveRssContact();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String textboton = button1.Text;
            if (textboton.Equals("ACTIVAR COMPROBACIÓN"))
            {
                //MessageBox.Show("Activar comprobación");
                button1.Text = "DESACTIVAR COMPROBACIÓN";
                button1.BackColor = Color.Green;
                button2.Enabled = false;
                textBox2.Enabled = false;
                textBox2.Text = String.Empty;
            }
            else
            {
                //MessageBox.Show("Desactivar comprobación");
                button1.Text = "ACTIVAR COMPROBACIÓN";
                button1.BackColor = Color.Red;
                button2.Enabled = true;
                textBox2.Enabled = true;
                textBox2.Text = String.Empty;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ModifyFeedDialog mfDialog = new ModifyFeedDialog();
            mfDialog.MdiParent = WrapWindow.ActiveForm;
            mfDialog.Show();
        }

        private void dataGridView2_MouseClick(object sender, MouseEventArgs e)
        {
        }

        private void dataGridView2_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                if (e.Button == MouseButtons.Right)
                {
                    DataGridViewRow clickedRow = (sender as DataGridView).Rows[e.RowIndex];

                    Point relativeMousePosition = dataGridView1.PointToClient(Cursor.Position);
                    this.dataGridView2.Rows[this.dataGridView2.SelectedRows[0].Index].Selected = false;
                    this.dataGridView2.Rows[e.RowIndex].Selected = true;
                    //this.contextMenuStrip1.Items.Add(e.RowIndex.ToString());
                    this.contextMenuStrip1.Show(dataGridView1, relativeMousePosition);
                }
            }
        }
    }
}
