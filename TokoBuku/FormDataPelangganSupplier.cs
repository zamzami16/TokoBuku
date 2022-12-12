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
    public partial class FormDataPelangganSupplier : Form
    {
        //private string Type_Of;

        public FormDataPelangganSupplier(string type_of)
        {
            InitializeComponent();
            if (type_of == "PELANGGAN")
            {
                this.labelTitle.Text = "DATA PELANGGAN";
                //this.Type_Of = "pelanggan";
                this.Text = "FORM DATA PELANGGAN";
            }
            else if (type_of == "SUPPLIER")
            {
                this.labelTitle.Text = "DATA SUPPLIER";
                //this.Type_Of = "supplier";
                this.Text = "FORM DATA SUPPLIER";
            }
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
            else if(this.labelTitle.Text == "DATA PELANGGAN")
            {
                // do for Data Pelanggan
                string message = "DATA PELANGGAN DISIMPAN" +
                    $"NAMA PELANGGAN: {textBoxNama.Text}\n" +
                    $"ALAMAT: {textBoxAlamat.Text} \n" +
                    $"EMAIL: {textBoxEmail.Text}";
                ShowSaved(message);
                this.Close();
            }
            else if (this.labelTitle.Text == "DATA SUPPLIER")
            {
                // do for data Supplier
                string message1 = "DATA SUPLLIER DISIMPAN" +
                    $"NAMA PELANGGAN: {textBoxNama.Text}\n" +
                    $"ALAMAT: {textBoxAlamat.Text}\n" +
                    $"EMAIL: {textBoxEmail.Text}";
                ShowSaved(message1);
                this.Close();
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
    }
}
