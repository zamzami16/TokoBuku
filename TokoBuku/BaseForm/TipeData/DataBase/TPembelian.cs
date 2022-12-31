using System;

namespace TokoBuku.BaseForm.TipeData.DataBase
{
    internal class TPembelian
    {
        public int Id { get; set; }
        public int IdSupplier { get; set; }
        public DateTime TanggalBeli { get; set; }
        public string NoNota { get; set; }
        public string NotaAsli { get; set; }
        public double Total { get; set; }
        public TJenisPembayaran JenisPembayaran { get; set; }
        public int IdKas { get; set; }


        public TPembelian() { }
        public TPembelian(TJenisPembayaran jenisPembayaran) { this.JenisPembayaran = jenisPembayaran; }


        public TPembelian(int id, int idSupplier, DateTime tanggalBeli, string noNota, string notaAsli, double total, TJenisPembayaran jenisPembayaran, int idKas)
        {
            Id = id;
            IdSupplier = idSupplier;
            TanggalBeli = tanggalBeli;
            NoNota = noNota;
            NotaAsli = notaAsli;
            Total = total;
            JenisPembayaran = jenisPembayaran;
            IdKas = idKas;
        }
    }
}
