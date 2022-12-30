using System;
using System.Windows.Forms;
using TokoBuku.BaseForm.TipeData.DataBase;

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
        public TPelanggan Pelanggan { get; set; }
        public TSupplier Supplier { get; set; }

        public FormEditDataPelangganSupplier()
        {
            InitializeComponent();
        }

        private void buttonCancel_Click(object sender, EventArgs e) { this.Close(); }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxNama.Text))
            {
                ShowErrorPrompt("NAMA TIDAK BOLOEH KOSONG");
            }
            else
            {
                string hp = this.maskedTextBox1.Text.Replace("-", "").Replace("(", "").Replace(")", "").Replace("+", "").Replace(" ", "");
                if (hp.Length < 3) { hp = string.Empty; }
                this.Pelanggan.Nama = this.textBoxNama.Text;
                this.Pelanggan.Alamat = this.textBoxAlamat.Text;
                this.Pelanggan.NoHp = hp;
                this.Pelanggan.Email = this.textBoxEmail.Text;
                this.Pelanggan.Keterangan = this.richTextBoxKeterangan.Text;
                this.Pelanggan.Status = TStatus.Aktif;

                if (this.type_of == TipeForm.Supplier)
                {
                    this.Supplier = new TSupplier(this.Pelanggan);
                    this.Pelanggan = null;
                }

                this.DialogResult = DialogResult.OK;
            }
        }

        private void ShowErrorPrompt(string message)
        {
            MessageBox.Show(message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        public void SetToEditFormPelanggan(TPelanggan pelanggan)
        {
            //string formated_no_hp = row.Cells["no_hp"].Value.ToString().Remove(0, 1);
            this.type_of = TipeForm.Pelanggan;
            this.textBoxNama.Text = pelanggan.Nama;
            this.textBoxAlamat.Text = pelanggan.Alamat;
            try { this.maskedTextBox1.Text = pelanggan.NoHp.Remove(0, 2); }
            catch (Exception) { this.maskedTextBox1.Text = string.Empty; }
            this.textBoxEmail.Text = pelanggan.Email;
            this.richTextBoxKeterangan.Text = pelanggan.Keterangan;
            this.Pelanggan = pelanggan;
        }

        public void SetToEditFormSupplier(TSupplier supplier)
        {
            //string formated_no_hp = row.Cells["no_hp"].Value.ToString().Remove(0, 1);
            this.type_of = TipeForm.Supplier;
            this.textBoxNama.Text = supplier.Nama;
            this.textBoxAlamat.Text = supplier.Alamat;
            try { this.maskedTextBox1.Text = supplier.NoHp.Remove(0, 2); }
            catch (Exception) { this.maskedTextBox1.Text = string.Empty; }
            this.textBoxEmail.Text = supplier.Email;
            this.richTextBoxKeterangan.Text = supplier.Keterangan;
            this.Pelanggan = supplier;
            this.Supplier= supplier;
        }
    }
}
