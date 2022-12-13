using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TokoBuku.Transaksi;

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

            //Application.Run(new TokoBukuWindows());

            //Application.Run(new FormMasterDataViewer());
            //Application.Run(new FormAddDataBarang());
            //Application.Run(new FormAddDataKasir());
            //Application.Run(new FormDataPelangganSupplier("SUPPLIER"));
            //Application.Run(new FormDataRakKasKategoriPenerbitMaster("kategori"));
            Application.Run(new Penjualan());
        }
    }
}
