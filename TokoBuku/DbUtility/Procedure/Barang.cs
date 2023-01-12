using FirebirdSql.Data.FirebirdClient;
using System.Data;
using TokoBuku.BaseForm.TipeData.DataBase;

namespace TokoBuku.DbUtility.Procedure
{
    public static class Barang
    {
        public static double GetStockBarang()
        {
            double stock = 0;
            using (FbConnection con = ConnectDB.Connetc())
            {
                using (FbCommand cmd = new FbCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection= con;
                    cmd.CommandText = "get_stock_barang";
                    var x = cmd.ExecuteReader();
                    if (x.FieldCount > 0)
                    {
                        while (x.Read())
                        {
                            double.TryParse(x[x.FieldCount - 1].ToString(), out stock);
                            return stock;
                        }
                    }
                }
            }
            return stock;
        }
        public static DataTable GetDataBarang()
        {
            DataTable dataTable= new DataTable();
            using (FbConnection con = ConnectDB.Connetc())
            {
                using (FbCommand cmd = new FbCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection= con;
                    cmd.CommandText = "get_data_barang";
                    var da = new FbDataAdapter(cmd);
                    da.Fill(dataTable);
                }
            }
            return dataTable;
        }

        internal static void UpdateDataBarang(TBarang dbBarang)
        {
            using (FbConnection con = ConnectDB.Connetc())
            {
                using (FbCommand cmd = new FbCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "update_data_barang";
                    cmd.Connection= con;
                    cmd.Parameters.Add("@id_barang", dbBarang.IdBarang);
                    cmd.Parameters.Add("@kode", dbBarang.Kode);
                    cmd.Parameters.Add("@nama_barang", dbBarang.NamaBarang);
                    cmd.Parameters.Add("@id_penerbit", dbBarang.IdPenerbit);
                    cmd.Parameters.Add("@id_kategori", dbBarang.IdKategori);
                    cmd.Parameters.Add("@id_rak", dbBarang.IdRak);
                    cmd.Parameters.Add("@stock", dbBarang.Stock);
                    cmd.Parameters.Add("@harga_jual", dbBarang.HargaJual);
                    cmd.Parameters.Add("@harga_beli", dbBarang.HargaBeli);
                    cmd.Parameters.Add("@isbn", dbBarang.ISBN);
                    cmd.Parameters.Add("@penulis", dbBarang.Penulis);
                    cmd.Parameters.Add("@diskon", dbBarang.Diskon);
                    cmd.Parameters.Add("@barcode", dbBarang.BarCode);
                    cmd.Parameters.Add("@keterangan", dbBarang.Keterangan);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
            }
        }

    }
}
