using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace TokoBuku.Login
{
    public partial class FormLogin : Form
    {
        private bool showed = false;
        public string IdKasir { get; set; }
        public string NamaKasir { get;set; }
        public FormLogin() { InitializeComponent(); }

        private void buttonCancell_Click(object sender, EventArgs e) { this.Close(); }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            string uname = this.textBoxUserName.Text.Trim();
            string pwd = this.textBoxPassword.Text.Trim();
            Dictionary<string,string> kasir = TokoBuku.DbUtility.Login.GetKasir.LoginKasir(uname, pwd);
            if (kasir.Count > 0)
            {
                var idKasir = kasir["id"];
                var nama = kasir["nama"];
                this.IdKasir = idKasir;
                this.NamaKasir = nama;
                this.DialogResult= DialogResult.OK;
                MessageBox.Show("Login Berhasil. Selamat datang" + nama, "Login Success.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Masukkan username dan password yang benar.", "Login Failed.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void buttonShowPassword_Click(object sender, EventArgs e)
        {
            if (!showed) { ButShowPassword(); } else { ButHidePassword(); }
        }
        private void ButShowPassword() { this.buttonShowPassword.ImageIndex = 0; showed = true; this.textBoxPassword.UseSystemPasswordChar = false; toolTip1.SetToolTip(buttonShowPassword, "Hide Password"); }
        private void ButHidePassword() { this.buttonShowPassword.ImageIndex = 1; showed = false; this.textBoxPassword.UseSystemPasswordChar = true; toolTip1.SetToolTip(buttonShowPassword, "Show Password"); }
    }
}
