using System;
using System.Windows.Forms;
using TokoBuku.BaseForm.TipeData.DataBase;

namespace TokoBuku.BaseForm.Master.Input
{
    public partial class FormDataPelangganSupplier : Form
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
        public TPelanggan Pelanggan { get; set; }
        public TSupplier Supplier { get; set; }


        public FormDataPelangganSupplier()
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
            else if (string.IsNullOrWhiteSpace(comboBox1.Text))
            {
                comboBox1.Text = "AKTIF";
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
                    this.Supplier = new TSupplier(Pelanggan);
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
                this.Pelanggan = new TPelanggan();
            }
            else if (this.type_of == TipeForm.Supplier)
            {
                this.Text = "Data Supplier";
                this.labelTitle.Text = "Data Supplier";
                this.Pelanggan = new TPelanggan();
                this.Supplier = new TSupplier();
            }
        }
    }
}
