using System;
using System.Windows.Forms;

namespace TokoBuku
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new TokoBukuWindows());
            //using (var formLogin = new Login.FormLogin())
            //{
            //    Application.Run(formLogin);

            //}


            //Application.Run(new FormUbahHarga());

            //Application.Run(new TokoBuku.BaseForm.Master.FormMasterViewPelanggan());
            //Application.Run(new TokoBuku.BaseForm.Transaksi.HutangPiutang.FormBayarHutang());

            //Application.Run(new FormMasterDataViewer());
            //Application.Run(new FormAddDataBarang());
            //Application.Run(new FormAddDataKasir());
            //Application.Run(new FormDataPelangganSupplier("SUPPLIER"));
            //Application.Run(new FormDataRakKasKategoriPenerbitMaster("kategori"));
            //Application.Run(new Penjualan());
        }
    }
}
