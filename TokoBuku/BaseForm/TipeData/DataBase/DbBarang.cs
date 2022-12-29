namespace TokoBuku.BaseForm.TipeData.DataBase
{
    public class DbBarang
    {
        #region Property access
        public int IdBarang { get; set; }
        public int IdKategori { get; set; }
        public int IdPenerbit { get; set; }
        public int IdRak { get; set; }
        public string Kode { get; set; }
        public string NamaBarang { get; set; }
        public double Stock { get; set; }
        public double HargaJual { get; set; }
        public double HargaBeli { get; set; }
        public string ISBN { get; set; }
        public string Penulis { get; set; }
        public double Diskon { get; set; }
        public StatusPenggunaan Status { get; set; }
        public string BarCode { get; set; }
        public string Keterangan { get; set; }
        public string Rak { get; set; }
        public string Kategori { get; set; }
        public string Penerbit { get; set; }
        #endregion

        public DbBarang() { }
        public DbBarang(int IdBarang, int IdKategori, int IdPenerbit, int IdRak, string Kode, string NamaBarang, double Stock, double HargaJual, double HargaBeli, string ISBN, string Penulis, double Diskon, StatusPenggunaan statusPenggunaan, string BarCode, string Keterangan)
        {
            this.IdBarang = IdBarang;
            this.IdKategori = IdKategori;
            this.IdPenerbit = IdPenerbit;
            this.IdRak = IdRak;
            this.Kode = Kode;
            this.NamaBarang = NamaBarang;
            this.Stock = Stock;
            this.HargaJual = HargaJual;
            this.HargaBeli = HargaBeli;
            this.ISBN = ISBN;
            this.Penulis = Penulis;
            this.Diskon = Diskon;
            this.Status = statusPenggunaan;
            this.BarCode = BarCode;
            this.Keterangan = Keterangan;
        }
    }
}
