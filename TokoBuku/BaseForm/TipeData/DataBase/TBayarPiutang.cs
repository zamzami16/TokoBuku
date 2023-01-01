using System;

namespace TokoBuku.BaseForm.TipeData.DataBase
{
    public class TBayarPiutang
    {
        public int IdPiutang { get; set; }
        public double Pembayaran { get; set; }
        public DateTime TglBayar { get; set; }
        public int IdKas { get; set; }
        public TIsDP isDP { get; set; }
        public TBayarPiutang() { }
        public TBayarPiutang(int idPiutang, double pembayaran, DateTime tglBayar, int idKas, TIsDP isDP)
        {
            IdPiutang = idPiutang;
            Pembayaran = pembayaran;
            TglBayar = tglBayar;
            IdKas = idKas;
            this.isDP = isDP;
        }
    }
}
