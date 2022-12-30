namespace TokoBuku.BaseForm.TipeData.DataBase
{
    public class TPelanggan
    {
        public int Id { get; set; }
        public string Nama { set; get; }
        public string Alamat { set; get; }
        public string NoHp { set; get; }
        public string Email { set; get; }
        public string Keterangan { set; get; }
        public TStatus Status { set; get; }

        public TPelanggan() { this.Status = TStatus.Aktif; }

        public TPelanggan(int idPelanggan, string namaPelanggan, string alamat, string noHp, string email, string keterangan, TStatus status=TStatus.Aktif)
        {
            Id = idPelanggan;
            Nama = namaPelanggan;
            Alamat = alamat;
            NoHp = noHp;
            Email = email;
            Keterangan = keterangan;
            Status = status;
        }
    }
}
