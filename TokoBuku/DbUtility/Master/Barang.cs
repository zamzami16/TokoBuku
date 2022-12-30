using FirebirdSql.Data.FirebirdClient;
using System.Data;
using TokoBuku.BaseForm.TipeData.DataBase;

namespace TokoBuku.DbUtility.Master
{
    internal static class Barang
    {
        internal static DataTable GetDataBarang()
        {
            DataTable dt = new DataTable();
            var query = "select " +
                "b.id_barang, " +
                "kode, " +
                "b.nama_barang as Nama_Barang, " +
                "p.nama_penerbit as Penerbit, " +
                "k.nama as Kategori, " +
                "rak.nama as Rak, " +
                "b.stock, b.harga_jual as harga_jual, b.harga_beli as harga_beli, b.isbn, " +
                "b.penulis, b.diskon, b.status, b.barcode, b.keterangan " +
                "from barang as b " +
                "INNER JOIN kategori as k ON b.id_kategori = k.id " +
                "INNER JOIN penerbit as p ON b.id_penerbit = p.id " +
                "INNER JOIN rak ON b.id_rak = rak.id " +
                "order by b.kode asc;";
            FbDataAdapter da = new FbDataAdapter(query, ConnectDB.Connetc());
            da.Fill(dt);
            da.Dispose();
            return dt;
        }

        internal static void UpdateDataBarang(TBarang dbBarang)
        {
            using (var con = ConnectDB.Connetc())
            {
                var strSql = "UPDATE barang " +
                    "SET NAMA_BARANG=@nama, " +
                    "Kode=@kode, " +
                    "ID_KATEGORI=@kategori, " +
                    "id_penerbit=@penerbit, " +
                    "id_rak=@rak, " +
                    "stock=@Stock, " +
                    "harga_jual=@Harga, " +
                    "harga_beli=@hargaBeli, " +
                    "isbn=@Isbn, " +
                    "penulis=@Penulis, " +
                    "diskon=@Diskon, " +
                    "barcode=@Barcode, " +
                    "keterangan=@Keterangan " +
                    "where id_barang=@Id";
                using (var cmd = new FbCommand(strSql, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("@nama", dbBarang.NamaBarang);
                    cmd.Parameters.Add("@kode", dbBarang.Kode);
                    cmd.Parameters.Add("@kategori", dbBarang.IdKategori);
                    cmd.Parameters.Add("@penerbit", dbBarang.IdPenerbit);
                    cmd.Parameters.Add("@rak", dbBarang.IdRak);
                    cmd.Parameters.Add("@Stock", dbBarang.Stock);
                    cmd.Parameters.Add("@Harga", dbBarang.HargaJual);
                    cmd.Parameters.Add("@hargaBeli", dbBarang.HargaBeli);
                    cmd.Parameters.Add("@Isbn", dbBarang.ISBN);
                    cmd.Parameters.Add("@Penulis", dbBarang.Penulis);
                    cmd.Parameters.Add("@Diskon", dbBarang.Diskon);
                    cmd.Parameters.Add("@Barcode", dbBarang.BarCode);
                    cmd.Parameters.Add("@Keterangan", dbBarang.Keterangan);
                    cmd.Parameters.Add("@Id", dbBarang.IdBarang);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
            }
        }

        static public int SaveBarang(TBarang dbBarang)
        {
            using (var con = ConnectDB.Connetc())
            {
                int ids;
                if (dbBarang.Kode == "--OTOMATIS--")
                {
                    dbBarang.Kode = TokoBuku.DbUtility.Etc.GenerateKodeBarang();
                }
                var strSql = "INSERT INTO BARANG (ID_KATEGORI, ID_PENERBIT, ID_RAK, KODE, NAMA_BARANG, STOCK, HARGA_jual, harga_beli," +
                    "ISBN, PENULIS, DISKON, STATUS, BARCODE, KETERANGAN) " +
                    "VALUES (@kategori, @penerbit, @rak, @kode, @nama, @stock, @harga, @beli, @isbn, @penulis, @diskon, @status, " +
                    "@barcode, @keterangan) returning ID_BARANG;";
                using (var cmd = new FbCommand(strSql, con))
                {/// TODO: cek variabel dio data base
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("@kategori", dbBarang.IdKategori);
                    cmd.Parameters.Add("@penerbit", dbBarang.IdPenerbit);
                    cmd.Parameters.Add("@rak", dbBarang.IdRak);
                    cmd.Parameters.Add("@kode", dbBarang.Kode);
                    cmd.Parameters.Add("@nama", dbBarang.NamaBarang);
                    cmd.Parameters.Add("@stock", dbBarang.Stock);
                    cmd.Parameters.Add("@harga", dbBarang.HargaJual);
                    cmd.Parameters.Add("@beli", dbBarang.HargaBeli);
                    cmd.Parameters.Add("@isbn", dbBarang.ISBN);
                    cmd.Parameters.Add("@penulis", dbBarang.Penulis);
                    cmd.Parameters.Add("@diskon", dbBarang.Diskon);
                    cmd.Parameters.Add("@status", dbBarang.Status);
                    cmd.Parameters.Add("@barcode", dbBarang.BarCode);
                    cmd.Parameters.Add("@keterangan", dbBarang.Keterangan);
                    ids = (int)cmd.ExecuteScalar();
                    cmd.Dispose();
                }
                return ids;
            }
        }
    }
}

