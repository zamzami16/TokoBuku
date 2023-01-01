using System;

namespace TokoBuku.BaseForm.TipeData.DataBase
{
    internal class TPiutang
    {
        public int Id { get; set; }
        public int IdPenjualan { get; set; }
        public int IdPelanggan { get; set; }
        public DateTime TanggalTenggatBayar { get; set; }
        public double Total { get; set; }
        public TLunas Lunas { get; set; }
        public TPiutang() { this.Lunas = TLunas.Belum; }

        public TPiutang(int id, int idPenjualan, int idPelanggan, DateTime tanggalTenggatBayar, double total, TLunas lunas)
        {
            Id = id;
            IdPenjualan = idPenjualan;
            IdPelanggan = idPelanggan;
            TanggalTenggatBayar = tanggalTenggatBayar;
            Total = total;
            Lunas = lunas;
        }
    }
}
