using System;

namespace TokoBuku.BaseForm.TipeData.DataBase
{
    public class TBayarHutang
    {
        public int IdHutang { get; set; }
        public double Pembayaran { get; set; }
        public DateTime TglBayar { get; set; }
        public int IdKas { get; set; }
        public TIsDP isDP { get; set; }
        public TBayarHutang() { }
        public TBayarHutang(int idHutang, double pembayaran, DateTime tglBayar, int idKas, TIsDP isDP)
        {
            IdHutang = idHutang;
            Pembayaran = pembayaran;
            TglBayar = tglBayar;
            IdKas = idKas;
            this.isDP = isDP;
        }
    }
}
