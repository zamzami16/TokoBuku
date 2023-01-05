namespace TokoBuku.BaseForm.TipeData.DataBase
{
    internal class TDetailPenjualan
    {
        public int IdBarang { get; set; }
        public int IdPenjualan { get; set; }
        public double Jumlah { get; set; }
        public double HargaJual { get; set; }
        public double HargaBeli { get; set; }
        public string Satuan { get; set; }

        public TDetailPenjualan() { this.Satuan = "Pcs"; }

        public TDetailPenjualan(int idBarang, int idPenjualan, double jumlah, double hargaJual, double hargaBeli, string satuan)
        {
            IdBarang = idBarang;
            IdPenjualan = idPenjualan;
            Jumlah = jumlah;
            HargaJual = hargaJual;
            HargaBeli = hargaBeli;
            Satuan = satuan;
        }
    }
}
