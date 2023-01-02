using FirebirdSql.Data.FirebirdClient;
using System;
using System.Data;

namespace TokoBuku.DbUtility.Transactions
{
    internal static class UbahHarga
    {
        private static DataTable InitDataTable()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("id", typeof(int));
            dataTable.Columns.Add("kode", typeof(string));
            dataTable.Columns.Add("nama_barang", typeof(string));
            dataTable.Columns.Add("harga_beli", typeof(double));
            dataTable.Columns.Add("harga_jual", typeof(double));
            dataTable.Columns.Add("diskon_rp", typeof(double));
            dataTable.Columns.Add("diskon_", typeof(double));
            dataTable.Columns["diskon_rp"].Expression = "diskon_ / 100 * harga_jual";
            return dataTable;
        }
        internal static DataTable ListHargaBarang()
        {
            DataTable dataTable = InitDataTable();
            using (var con = ConnectDB.Connetc())
            {
                var query = "select " +
                    "id_barang as id, " +
                    "kode, " +
                    "nama_barang as nama_barang, " +
                    "harga_beli as harga_beli, " +
                    "harga_jual as harga_jual, " +
                    "diskon as diskon_ " +
                    "from barang;";
                using (var cmd = new FbCommand(query, con))
                {
                    FbDataAdapter da = new FbDataAdapter(cmd);
                    da.Fill(dataTable);
                    da.Dispose();
                }
            }
            return dataTable;
        }

        internal static DataTable FilterDataBarang(int id_kategori, int id_rak, int id_penerbit)
        {
            DataTable dataTable = InitDataTable();
            using (var con = ConnectDB.Connetc())
            {
                var query = "select " +
                    "id_barang as id, " +
                    "kode, " +
                    "nama_barang as nama_barang, " +
                    "harga_beli as harga_beli, " +
                    "harga_jual as harga_jual, " +
                    "diskon as diskon_ " +
                    "from barang " +
                    "where ";
                if (id_kategori == -1 && id_rak == -1 && id_penerbit == -1)
                {
                    // no filter
                    query = "select " +
                        "id_barang as id, " +
                        "kode, " +
                        "nama_barang as nama_barang, " +
                        "harga_beli as harga_beli, " +
                        "harga_jual as harga_jual, " +
                        "diskon as diskon_ " +
                        "from barang;";
                }
                else if (id_kategori != -1 && id_rak != -1 && id_penerbit != -1)
                {
                    // filter all
                    query += "id_kategori=@id_kategori and id_rak=@id_rak and id_penerbit=@id_penerbit;";
                }
                else if (id_kategori != -1 && id_rak != -1 && id_penerbit == -1)
                {
                    // filter kategori & rak
                    query += "id_kategori=@id_kategori and id_rak=@id_rak;";
                }
                else if (id_kategori != -1 && id_rak == -1 && id_penerbit != -1)
                {
                    // filter kategori & penerbit
                    query += "id_kategori=@id_kategori and id_penerbit=@id_penerbit;";
                }
                else if (id_kategori == -1 && id_rak != -1 && id_penerbit != -1)
                {
                    // filter rak & penerbit
                    query += "id_rak=@id_rak and id_penerbit=@id_penerbit;";
                }
                else if (id_kategori != -1 && id_rak == -1 && id_penerbit == -1)
                {
                    // filter kategori
                    query += "id_kategori=@id_kategori;";
                }
                else if (id_kategori == -1 && id_rak != -1 && id_penerbit == -1)
                {
                    // filter rak
                    query += "id_rak=@id_rak;";
                }
                else if (id_kategori == -1 && id_rak == -1 && id_penerbit != -1)
                {
                    // filter penerbit
                    query += "id_penerbit=@id_penerbit;";
                }

                using (var cmd = new FbCommand(query, con))
                {
                    if (id_kategori != -1 && id_rak != -1 && id_penerbit != -1)
                    {
                        // filter all
                        cmd.Parameters.Add("@id_kategori", id_kategori);
                        cmd.Parameters.Add("@id_rak", id_rak);
                        cmd.Parameters.Add("@id_penerbit", id_penerbit);
                    }
                    else if (id_kategori != -1 && id_rak != -1 && id_penerbit == -1)
                    {
                        // filter kategori & rak
                        cmd.Parameters.Add("@id_kategori", id_kategori);
                        cmd.Parameters.Add("@id_rak", id_rak);
                    }
                    else if (id_kategori != -1 && id_rak == -1 && id_penerbit != -1)
                    {
                        // filter kategori & penerbit
                        cmd.Parameters.Add("@id_kategori", id_kategori);
                        cmd.Parameters.Add("@id_penerbit", id_penerbit);
                    }
                    else if (id_kategori == -1 && id_rak != -1 && id_penerbit != -1)
                    {
                        // filter rak & penerbit
                        cmd.Parameters.Add("@id_rak", id_rak);
                        cmd.Parameters.Add("@id_penerbit", id_penerbit);
                    }
                    else if (id_kategori != -1 && id_rak == -1 && id_penerbit == -1)
                    {
                        // filter kategori
                        cmd.Parameters.Add("@id_kategori", id_kategori);
                    }
                    else if (id_kategori == -1 && id_rak != -1 && id_penerbit == -1)
                    {
                        // filter rak
                        cmd.Parameters.Add("@id_rak", id_rak);
                    }
                    else if (id_kategori == -1 && id_rak == -1 && id_penerbit != -1)
                    {
                        // filter penerbit
                        cmd.Parameters.Add("@id_penerbit", id_penerbit);
                    }

                    FbDataAdapter da = new FbDataAdapter(cmd);
                    try
                    {
                        da.Fill(dataTable);
                    }
                    catch (Exception) { }
                    finally { da.Dispose(); }
                    da.Dispose();
                }
            }
            return dataTable;
        }

        internal static DataTable GetDataKategori()
        {
            DataTable dataTable = new DataTable();
            using (var con = ConnectDB.Connetc())
            {
                using (var cmd = new FbCommand())
                {
                    cmd.CommandText = "select id as id, nama as nama from kategori;";
                    cmd.Connection = con;
                    FbDataAdapter da = new FbDataAdapter(cmd);
                    da.Fill(dataTable);
                    da.Dispose();
                    cmd.Dispose();
                }
            }
            return dataTable;
        }

        internal static DataTable GetDataRak()
        {
            DataTable dataTable = new DataTable();
            using (var con = ConnectDB.Connetc())
            {
                using (var cmd = new FbCommand())
                {
                    cmd.CommandText = "select id as id, nama as nama from rak;";
                    cmd.Connection = con;
                    FbDataAdapter da = new FbDataAdapter(cmd);
                    da.Fill(dataTable);
                    da.Dispose();
                    cmd.Dispose();
                }
            }
            return dataTable;
        }
        internal static DataTable GetDataPenerbit()
        {
            DataTable dataTable = new DataTable();
            using (var con = ConnectDB.Connetc())
            {
                using (var cmd = new FbCommand())
                {
                    cmd.CommandText = "select id as id, nama_penerbit as nama from penerbit;";
                    cmd.Connection = con;
                    FbDataAdapter da = new FbDataAdapter(cmd);
                    da.Fill(dataTable);
                    da.Dispose();
                    cmd.Dispose();
                }
            }
            return dataTable;
        }

        internal static void UpdateDiskon(int id, decimal diskon)
        {
            using (var con = ConnectDB.Connetc())
            {
                var query = "update barang set diskon=@diskon where id_barang=@id";
                using (var cmd = new FbCommand(query, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("@diskon", diskon);
                    cmd.Parameters.Add("@id", id);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
            }
        }

        internal static void UpdateHargaJual(int id, double hargaJual)
        {
            using (var con = ConnectDB.Connetc())
            {
                var query = "update barang set harga_jual=@harga where id_barang=@id";
                using (var cmd = new FbCommand(query, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("@harga", hargaJual);
                    cmd.Parameters.Add("@id", id);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
            }
        }
    }
}
