using System;

namespace TokoBuku.BaseForm.TipeData.DataBase
{
    internal class THutang
    {
        public int Id { get; set; }
        public int IdPembelian { get; set; }
        public int IdSupplier { get; set; }
        public DateTime TanggalTenggatBayar { get; set; }
        public double Total { get; set; }
        public TLunas Lunas { get; set; }
        public THutang() { this.Lunas = TLunas.Belum; }
    }
}
