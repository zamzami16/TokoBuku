namespace TokoBuku.BaseForm.TipeData.DataBase
{
    public class TSupplier : TPelanggan
    {
        public TSupplier() { this.Status = TStatus.Aktif; }
        public TSupplier(TPelanggan pelanggan) 
        {
            this.Id= pelanggan.Id;
            this.Nama = pelanggan.Nama;
            this.Alamat = pelanggan.Alamat;
            this.NoHp = pelanggan.NoHp;
            this.Email = pelanggan.Email;
            this.Keterangan = pelanggan.Keterangan;
            this.Status = pelanggan.Status;
        }

    }
}
