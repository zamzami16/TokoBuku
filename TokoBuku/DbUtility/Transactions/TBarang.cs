using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TokoBuku.DbUtility.Transactions
{
    public class TBarang
    {
        #region Property access
        public string NamaBarang { get; set; }
        public double Harga { get; set; }
        public double Diskon { get; set; }
        public string Rak { get; set; }
        public int Stock { get; set; }
        public string Kategori { get; set; }
        public string Penerbit { get; set; }
        public string Penulis { get; set; }
        public string ISBN { get; set; }
        public string BarCode { get; set; }
        public string Status { get; set; }
        public string Keterangan { get; set; }
        #endregion

        TBarang()
        {

        }
    }
}
