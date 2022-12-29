using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TokoBuku.Login
{
    public partial class FormLogin : Form
    {
        private bool showed = false;
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
                var username = kasir["pwd"];
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
