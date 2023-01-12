using System;

namespace TokoBuku.BaseForm.TipeData.DataBase
{
    internal class TPenjualan
    {
        public int Id { get; set; }
        public string KodeTransaksi { get; set; }
        public int IdKasir { get; set; }
        public int IdPelanggan { get; set; }
        public double Total { get; set; }
        public double UangPembayaran { get; set; }
        public double UangKembalian { get; set; }
        public double Potongan { get; set; }
        public DateTime Tanggal { get; set; }
        public DateTime Waktu { get; set; }
        public TJenisPembayaran StatusPembayaran { get; set; }
        public int? IdKas { get; set; }
        public string Keterangan { get; set; }

        public TPenjualan() { }
        public TPenjualan(int id, string kodeTransaksi, int idKasir, int idPelanggan, double total, double uangPembayaran, double uangKembalian, double potongan, DateTime tanggal, DateTime waktu, TJenisPembayaran statusPembayaran, int idKas, string keterangan)
        {
            Id = id;
            KodeTransaksi = kodeTransaksi;
            IdKasir = idKasir;
            IdPelanggan = idPelanggan;
            Total = total;
            UangPembayaran = uangPembayaran;
            UangKembalian = uangKembalian;
            Potongan = potongan;
            Tanggal = tanggal;
            Waktu = waktu;
            StatusPembayaran = statusPembayaran;
            IdKas = idKas;
            Keterangan = keterangan;
        }
    }
}
