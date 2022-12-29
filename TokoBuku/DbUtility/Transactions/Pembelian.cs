using FirebirdSql.Data.FirebirdClient;
using System;
using System.Data;

namespace TokoBuku.DbUtility.Transactions
{
    internal static class Pembelian
    {
        public static int SavePembelian_cash(int id_supplier, DateTime tanggal_beli,
            string no_nota, double total, int id_kas, string status_pembayaran = "CASH")
        {
            int ids = 0;
            using (var con = ConnectDB.Connetc())
            {
                var query = "insert into pembelian " +
                    "(id_supplier, tanggal_beli, no_nota, " +
                    "total, status_pembelian, id_kas) " +
                    "values (@id_supplier, @tanggal_beli, @no_nota, " +
                    "@total, @status_pembayaran, @id_kas) " +
                    "returning id_pembelian;";
                using (var cmd = new FbCommand(query, con))
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.Add("@id_supplier", id_supplier);
                    cmd.Parameters.Add("@tanggal_beli", tanggal_beli);
                    cmd.Parameters.Add("@no_nota", no_nota);
                    cmd.Parameters.Add("@total", total);
                    cmd.Parameters.Add("@status_pembayaran", status_pembayaran);
                    cmd.Parameters.Add("id_kas", id_kas);
                    ids = (int)cmd.ExecuteScalar();
                    cmd.Dispose();
                }
            }
            return ids;
        }

        public static int SavePembelian_kredit(int id_supplier, DateTime tanggal_beli,
            string no_nota, double total, string id_kas, string status_pembayaran = "CASH")
        {
            int ids = 0;
            using (var con = ConnectDB.Connetc())
            {
                var query = "insert into pembelian " +
                    "(id_supplier, tanggal_beli, no_nota, " +
                    "total, status_pembelian, id_kas) " +
                    "values (@id_supplier, @tanggal_beli, @no_nota, " +
                    "@total, @status_pembayaran, @id_kas) " +
                    "returning id_pembelian;";
                using (var cmd = new FbCommand(query, con))
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.Add("@id_supplier", id_supplier);
                    cmd.Parameters.Add("@tanggal_beli", tanggal_beli);
                    cmd.Parameters.Add("@no_nota", no_nota);
                    cmd.Parameters.Add("@total", total);
                    cmd.Parameters.Add("@status_pembayaran", status_pembayaran);
                    cmd.Parameters.Add("id_kas", id_kas);
                    ids = (int)cmd.ExecuteScalar();
                    cmd.Dispose();
                }
            }
            return ids;
        }

        internal static void SavePembelianCash(int id_supplier, DateTime tanggal_beli,
            string no_nota, double total, string status_pembayaran, int id_kas, DataTable rows)
        {
            var ids = SavePembelian_cash(id_supplier: id_supplier, tanggal_beli: tanggal_beli,
                no_nota: no_nota, total: total, status_pembayaran: status_pembayaran, id_kas: id_kas);
            try
            {
                using (var con = ConnectDB.Connetc())
                {
                    var query = "insert into detail_pembelian " +
                        "(id_barang, id_pembelian, jumlah, harga, satuan) " +
                        "values (@id_barang, @id_pembelian, @jumlah, @harga, @satuan);";
                    foreach (DataRow row in rows.Rows)
                    {
                        using (var cmd = new FbCommand(query, con))
                        {
                            cmd.CommandType = System.Data.CommandType.Text;
                            cmd.Parameters.Add("@id_barang", Convert.ToInt32(row["id"].ToString()));
                            cmd.Parameters.Add("@id_pembelian", ids);
                            cmd.Parameters.Add("@jumlah", Convert.ToDouble(row["jumlah"].ToString()));
                            cmd.Parameters.Add("@harga", Convert.ToDouble(row["subtotal_harga"].ToString()));
                            cmd.Parameters.Add("@satuan", row["satuan"].ToString());
                            cmd.ExecuteNonQuery();
                            cmd.Dispose();
                        }
                        UpdateStockBarangPembelian(row);
                        UpdateHargaBeli(row);
                    }
                }
            }
            catch (Exception ex)
            {
                DeletePembelian(ids);
                throw ex;
            }
        }

        internal static void SavePembelianKredit(int id_supplier, DateTime tanggal_beli,
            DateTime tgl_tenggat_bayar, double pembayaran_awal, string no_nota,
            double total, string status_pembelian, string id_kas, DataTable rows)
        {
            var ids = SavePembelian_kredit(id_supplier: id_supplier, tanggal_beli: tanggal_beli,
                no_nota: no_nota, total: total, id_kas: id_kas, status_pembayaran: status_pembelian);
            try
            {
                var id_hutang = SaveHutang(id_pembelian: ids, id_supplier: id_supplier, tgl_tenggat_bayar: tgl_tenggat_bayar, total: total, pembayaran_awal: pembayaran_awal, id_kas: id_kas, tgl_bayar: tanggal_beli);
                try
                {
                    using (var con = ConnectDB.Connetc())
                    {
                        var query = "insert into detail_pembelian " +
                            "(id_barang, id_pembelian, jumlah, harga, satuan) " +
                            "values (@id_barang, @id_pembelian, @jumlah, @harga, @satuan);";
                        foreach (DataRow row in rows.Rows)
                        {
                            using (var cmd = new FbCommand(query, con))
                            {
                                cmd.CommandType = System.Data.CommandType.Text;
                                cmd.Parameters.Add("@id_barang", Convert.ToInt32(row["id"].ToString()));
                                cmd.Parameters.Add("@id_pembelian", ids);
                                cmd.Parameters.Add("@jumlah", Convert.ToDouble(row["jumlah"].ToString()));
                                cmd.Parameters.Add("@harga", Convert.ToDouble(row["subtotal_harga"].ToString()));
                                cmd.Parameters.Add("@satuan", row["satuan"].ToString());
                                cmd.ExecuteNonQuery();
                                cmd.Dispose();
                            }
                            UpdateStockBarangPembelian(row);
                            UpdateHargaBeli(row);
                        }
                    }
                }
                catch (Exception)
                {
                    DeleteHutang(id_hutang);
                }
            }
            catch (Exception)
            {
                DeletePembelian(ids);
            }
        }

        internal static int SaveHutang(int id_pembelian, int id_supplier, DateTime tgl_tenggat_bayar, DateTime tgl_bayar, double total, double pembayaran_awal, string id_kas)
        {
            using (var con = ConnectDB.Connetc())
            {
                int id = 0;
                var query = "insert into hutang " +
                    "(id_pembelian, id_supplier, tgl_tenggat_bayar, total) " +
                    "values (@id_pembelian, @id_supplier, @tgl_tenggat_bayar, @total) " +
                    "returning id;";
                using (var cmd = new FbCommand(query, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("@id_pembelian", id_pembelian);
                    cmd.Parameters.Add("@id_supplier", id_supplier);
                    cmd.Parameters.Add("@tgl_tenggat_bayar", tgl_tenggat_bayar);
                    cmd.Parameters.Add("@total", total);
                    id = (int)cmd.ExecuteScalar();
                    cmd.Dispose();
                }
                if (pembayaran_awal >= 0)
                {
                    BayarHutang(id_hutang: id, pembayaran: pembayaran_awal, tgl_bayar: tgl_bayar, id_kas: id_kas);
                }
                return id;
            }
        }

        internal static void BayarHutang(int id_hutang, double pembayaran, DateTime tgl_bayar, string id_kas)
        {
            using (var con = ConnectDB.Connetc())
            {
                var query = "insert into bayar_hutang " +
                    "(id_hutang, pembayaran, tgl_bayar, id_kas) " +
                    "values (@id_hutang, @pembayaran, @tgl_bayar, @id_kas);";
                using (var cmd = new FbCommand(query, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("@id_hutang", id_hutang);
                    cmd.Parameters.Add("@pembayaran", pembayaran);
                    cmd.Parameters.Add("@tgl_bayar", tgl_bayar);
                    cmd.Parameters.Add("@id_kas", id_kas);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
            }
        }

        internal static void DeleteHutang(int id_hutang)
        {
            using (var con = ConnectDB.Connetc())
            {
                var query = "delete hutang where id=@id";
                using (var cmd = new FbCommand(query, con))
                {
                    cmd.Parameters.Add("@id", id_hutang);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
            }
        }

        internal static void DeletePembelian(int id)
        {
            using (var con = ConnectDB.Connetc())
            {
                var query = "delete pembelian where id=@id";
                using (var cmd = new FbCommand(query, con))
                {
                    cmd.Parameters.Add("@id", id);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
            }
        }

        internal static void UpdateStockBarangPembelian(DataRow row)
        {
            int id_barang = Convert.ToInt32(row["id"].ToString());
            var stock_db = GetStockBarang(id_barang);
            double stock_minus = Convert.ToDouble(row["jumlah"].ToString());
            if (row["satuan"].ToString().ToLower() == "packs")
            {
                stock_minus *= 10;
            }
            var stock_ = stock_db + stock_minus;
            using (var con = ConnectDB.Connetc())
            {
                var query = "update barang " +
                    "set stock=@stock_ " +
                    "where id_barang=@id_barang;";
                using (var cmd = new FbCommand(query, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("@stock_", stock_);
                    cmd.Parameters.Add("@id_barang", id_barang);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
            }
        }

        internal static double GetStockBarang(int id_barang)
        {
            double stock = 0;
            using (var con = ConnectDB.Connetc())
            {
                var query = "select stock from barang where id_barang=@id_barang";
                using (var cmd = new FbCommand(query, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("@id_barang", id_barang);
                    var x = cmd.ExecuteReader();
                    if (x.FieldCount > 0)
                    {
                        while (x.Read())
                        {
                            string dump_ = x[x.FieldCount - 1].ToString();
                            stock = Convert.ToDouble(dump_);
                        }
                    }
                    cmd.Dispose();
                }
            }
            return stock;
        }

        internal static void UpdateHargaBeli(DataRow row)
        {
            int id_barang = Convert.ToInt32(row["id"].ToString());
            double harga_beli = Convert.ToDouble(row["harga_Satuan"].ToString());
            if (row["satuan"].ToString().ToLower() == "packs")
            {
                harga_beli /= 10;
            }
            using (var con = ConnectDB.Connetc())
            {
                var query = "update barang " +
                    "set beli=@harga_beli " +
                    "where id_barang=@id_barang;";
                using (var cmd = new FbCommand(query, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("@harga_beli", harga_beli);
                    cmd.Parameters.Add("@id_barang", id_barang);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
            }
        }

        internal static DataTable HistoriPembelian()
        {
            DataTable data = new DataTable();
            using (var con = ConnectDB.Connetc())
            {
                var query = "select pem.id_pembelian, sup.nama as suplier, " +
                    "pem.tanggal_beli, pem.no_nota as nota, pem.total, " +
                    "pem.status_pembelian as pembayaran, k.nama as kas " +
                    "from pembelian as pem " +
                    "left join supplier as sup " +
                    "on pem.id_supplier=sup.id " +
                    "left join kas_master as k " +
                    "on pem.id_kas=k.id " +
                    "order by pem.tanggal_beli desc;";
                using (var cmd = new FbCommand(query, con))
                {
                    cmd.CommandType = CommandType.Text;
                    FbDataAdapter da = new FbDataAdapter(cmd);
                    da.Fill(data);
                    da.Dispose();
                }
            }
            return data;
        }
    }
}
