using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TokoBuku
{
    public partial class FormDataRakKasKategoriPenerbitMaster : Form
    {
        private bool isEdit = true;
        private string Type_OF = "kas";
        public FormDataRakKasKategoriPenerbitMaster(string type_of, bool isEdit=false)
        {
            InitializeComponent();
            if (type_of == "kas")
            {
                this.Text = "DATA KAS";
                this.labelTitle.Text = "DATA KAS";
                this.isEdit = isEdit;
                this.Type_OF = "kas";
            }
            else if (type_of == "kategori")
            {
                this.Text = "DATA KATEGORI";
                this.labelTitle.Text = "DATA KATEGORI BUKU";
                this.isEdit = isEdit;
                this.Type_OF = "kategori";
            }
            else if (type_of == "penerbit")
            {
                this.Text = "DATA PENERBIT";
                this.labelTitle.Text = "DATA PENERBIT BUKU";
                this.isEdit = isEdit;
                this.Type_OF = "penerbit";
            }
            else if (type_of == "rak")
            {
                this.Text = "DATA RAK";
                this.labelTitle.Text = "DATA RAK BUKU";
                this.isEdit = isEdit;
                this.Type_OF = "rak";
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("KOLOM NAMA TIDAK BOLEH KOSONG", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (string.IsNullOrWhiteSpace(comboBoxStatus.Text))
            {
                comboBoxStatus.Text = "AKTIF";
            }
            else
            {
                MessageBox.Show($"DATA NAMA {this.Type_OF.ToUpper()} disimpan.");
                this.Close();
            }
        }
    }
}
