namespace TokoBuku.BaseForm.TipeData.DataBase
{
    internal class TDetailPembelian
    {
        public int IdBarang { get; set; }
        public int IdPembelian { get; set; }
        public double Jumlah { get; set; }
        public double Harga { get; set; }
        public string Satuan { get; set; }

        public TDetailPembelian() { this.Satuan = "Pcs"; }
    }
}
