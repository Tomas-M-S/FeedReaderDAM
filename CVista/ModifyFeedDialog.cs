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
    public partial class ModifyFeedDialog : Form
    {
        public int flag;

        public int idfeed;
        public string savedate;
        public string url;
        public string comment;
        public string type;
        public string title;
        public RssContactsDialog parent;

        public ModifyFeedDialog(
                RssContactsDialog parent,
                int flag,
                int idfeed = -1,
                string savedate = "",
                string url = "",
                string comm = "",
                string type = "",
                string title = "")
        {
            this.parent = parent;
            this.flag = flag;
            this.idfeed = idfeed;
            this.savedate = savedate;
            this.url = url;
            this.comment = comm;
            this.type = type;
            this.title = title;
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ModifyFeedDialog_Load(object sender, EventArgs e)
        {
            if (this.flag == Constants.MODIFY_FEED)
            {
                this.Text = "Modificar Feed";
                this.button1.Text = "Modificar";
                this.textBox1.Text = this.idfeed.ToString();
                this.textBox2.Text = this.savedate;
                this.textBox3.Text = this.url;
                this.textBox4.Text = this.comment;
                int index = this.comboBox1.FindString(this.type);
                this.comboBox1.SelectedIndex = index;
                this.textBox5.Text = this.title;
            }
            else
            {
                this.Text = "Guardar Feed";
                this.button1.Text = "Guardar";
                this.textBox1.Text = this.idfeed.ToString();
                this.textBox2.Text = DateTime.Today.ToString("d");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] fecha = this.textBox2.Text.Split('/');
            int dia = Convert.ToInt32(fecha[0]);
            int mes = Convert.ToInt32(fecha[1]);
            int anyo = Convert.ToInt32(fecha[2]);
            DateTime dateu = new DateTime(anyo, mes, dia);

            string url = this.textBox3.Text;
            string comm = this.textBox4.Text;
            string type = (string)this.comboBox1.SelectedItem;
            string title = this.textBox5.Text;

            if (this.flag == Constants.MODIFY_FEED)
            {
                // Modifica
                int id = Convert.ToInt32(this.textBox1.Text);
                try
                {
                    if (Utils.updateRssContact(id, true, dateu, url, comm, type, title) == 1)
                    {
                        MessageBox.Show("Feed modificado correctamente", "Modificar Feed", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                        this.parent.cargarTabla1();
                        this.parent.cargarTabla2();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo modificar el Feed", "Modificar Feed", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("El campo URL es obligatorio. " + Environment.NewLine + ex.Message);
                }
            }
            else
            {
                // Guarda
                try
                {
                    if (Utils.saveRssContact(dateu, url, comm, type, title) == 1)
                    {
                        MessageBox.Show("Feed guardado correctamente", "Guardar Feed", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                        this.parent.cargarTabla1();
                        this.parent.cargarTabla2();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo guardar el Feed", "Guardar Feed", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("El campo URL es obligatorio. " + Environment.NewLine + ex.Message);
                }
            }
        }
    }
}
