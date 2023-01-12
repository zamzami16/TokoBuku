namespace TokoBuku.BaseForm.TipeData.DataBase
{
    public class TKas
    {
        public int Id { get; set; }
        public string Nama { get; set; }
        public double Saldo { get; set; }
        public string Keterangan { get; set; }
        public TStatus Status { get; set; }
        public TKas() { this.Status = TStatus.Aktif; }
        public TKas(int id, string nama, double saldo, string keterangan, TStatus status = TStatus.Aktif)
        {
            Id = id;
            Nama = nama;
            Saldo = saldo;
            Keterangan = keterangan;
            Status = status;
        }
    }
}
