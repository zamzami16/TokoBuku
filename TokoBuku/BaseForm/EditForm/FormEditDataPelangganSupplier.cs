using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TokoBuku.BaseForm.EditForm
{
    public partial class FormEditDataPelangganSupplier : Form
    {
        public enum TipeForm
        {
            Pelanggan,
            Supplier
        }

        public TipeForm type_of { get; set; }
        public TipeForm type_of_supplier { get; set; }
        public TipeForm type_of_pelanggan { get; set; }
        public string inputNama { get; set; }
        public string inputALamat { get; set; }
        public string inputNoHP { get; set; }
        public string inputEmail { get; set; }
        public string inputKeterangan { get; set; }
        public string inputStatus { get; set; }

        public FormEditDataPelangganSupplier()
        {
            InitializeComponent();
            this.type_of_pelanggan = TipeForm.Pelanggan;
            this.type_of_supplier = TipeForm.Supplier;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxNama.Text))
            {
                ShowErrorPrompt("NAMA TIDAK BOLOEH KOSONG");
            }
            else if (string.IsNullOrWhiteSpace(textBoxAlamat.Text))
            {
                ShowErrorPrompt("ALAMAT TIDAK BOLOEH KOSONG");
            }
            else if (string.IsNullOrWhiteSpace(maskedTextBox1.Text))
            {
                ShowErrorPrompt("NAMA TIDAK BOLOEH KOSONG");
            }
            else if (string.IsNullOrWhiteSpace(textBoxEmail.Text))
            {
                ShowErrorPrompt("NAMA TIDAK BOLOEH KOSONG");
            }
            else if (string.IsNullOrWhiteSpace(comboBox1.Text))
            {
                comboBox1.Text = "AKTIF";
            }
            else
            {
                this.inputALamat = this.textBoxAlamat.Text;
                this.inputEmail = this.textBoxEmail.Text;
                this.inputKeterangan = this.richTextBoxKeterangan.Text;
                this.inputNama = this.textBoxNama.Text;
                this.inputNoHP = this.maskedTextBox1.Text;
                this.inputStatus = "AKTIF";
                this.DialogResult = DialogResult.OK;
            }
        }

        private void ShowErrorPrompt(string message)
        {
            MessageBox.Show(message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ShowSaved(string message)
        {
            MessageBox.Show(message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void FormDataPelangganSupplier_Load(object sender, EventArgs e)
        {
            if (this.type_of == TipeForm.Pelanggan)
            {
                this.Text = "Data Pelanggan";
                this.labelTitle.Text = "Data Pelanggan";
            }
            else if (this.type_of == TipeForm.Supplier)
            {
                this.Text = "Data Supplier";
                this.labelTitle.Text = "Data Supplier";
            }
        }

        public void SetToEditForm(DataGridViewRow row)
        {
            //string formated_no_hp = row.Cells["no_hp"].Value.ToString().Remove(0, 1);

            this.textBoxNama.Text = row.Cells["nama_supplier"].Value.ToString();
            this.textBoxAlamat.Text = row.Cells["alamat"].Value.ToString();
            try { this.maskedTextBox1.Text = row.Cells["no_hp"].Value.ToString().Remove(0, 2); }
            catch (Exception) { this.maskedTextBox1.Text = string.Empty; }
            this.textBoxEmail.Text = row.Cells["email"].Value.ToString();
            this.richTextBoxKeterangan.Text = row.Cells["keterangan"].Value.ToString();
        }
    }
}
