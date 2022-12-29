using FirebirdSql.Data.FirebirdClient;
using System;
using System.Data;

namespace TokoBuku.DbUtility.Transactions
{
    internal static class Penjualan
    {
        internal static void SaveDetailPenjualan(DataTable Dt, int id_pembelian)
        {
            using (var con = ConnectDB.Connetc())
            {
                var query = "insert into detail_penjualan " +
                    "(id_barang, id_pembelian, jumlah, harga, satuan) " +
                    "values (@id_barang, @id_pembelian, @jumlah, @harga, @satuan);";
                foreach (DataRow row in Dt.Rows)
                {
                    using (var cmd = new FbCommand(query, con))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Parameters.Add("@id_barang", Convert.ToInt32(row["id"].ToString()));
                        cmd.Parameters.Add("@id_pembelian", id_pembelian);
                        cmd.Parameters.Add("@jumlah", Convert.ToDouble(row["jumlah"].ToString()));
                        cmd.Parameters.Add("@harga", Convert.ToDouble(row["subtotal_harga"].ToString()));
                        cmd.Parameters.Add("@satuan", row["satuan"].ToString());
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                    }
                }
            }
        }

        internal static int SavePenjualan(
            string kode_transaksi, int id_kasir, int id_pelanggan, double total_bayar, DateTime tanggal, DateTime waktu,
            string id_kas, string keterangan, string status_pembayaran)
        {
            int ids = 0;
            using (var con = ConnectDB.Connetc())
            {
                var query = "insert into Penjualan " +
                    "(kode_transaksi, id_kasir, id_pelanggan, total, tanggal, waktu, status_pembayaran, " +
                    "id_kas, keterangan) " +
                    "values (@kode_transaksi, @id_kasir, @id_pelanggan, @total, @tanggal, @waktu, @status_pembayaran, " +
                    "@id_kas, @keterangan)" +
                    "returning id;";
                using (var cmd = new FbCommand(query, con))
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.Add("@kode_transaksi", kode_transaksi);
                    cmd.Parameters.Add("@id_kasir", id_kasir);
                    cmd.Parameters.Add("@id_pelanggan", id_pelanggan);
                    cmd.Parameters.Add("@total", total_bayar);
                    cmd.Parameters.Add("@tanggal", tanggal);
                    cmd.Parameters.Add("@waktu", waktu);
                    cmd.Parameters.Add("@status_pembayaran", status_pembayaran);
                    cmd.Parameters.Add("@id_kas", id_kas);
                    cmd.Parameters.Add("@keterangan", keterangan);
                    ids = (int)cmd.ExecuteScalar();
                    cmd.Dispose();
                }
            }
            return ids;
        }

        internal static void SaveCash(
            string kode_transaksi, int id_kasir, int id_pelanggan, DateTime tanggal, DateTime waktu, double total_bayar,
            string id_kas, DataTable rows, string keterangan, string status_pembayaran = "CASH")
        {
            int id_penjualan = SavePenjualan(kode_transaksi: kode_transaksi, id_kasir: id_kasir, id_pelanggan: id_pelanggan, total_bayar: total_bayar, tanggal: tanggal, waktu: waktu, id_kas: id_kas, status_pembayaran: status_pembayaran, keterangan: keterangan);
            try
            {
                using (var con = ConnectDB.Connetc())
                {
                    var query = "insert into detail_penjualan " +
                        "(id_barang, id_penjualan, jumlah, harga, satuan) " +
                        "values (@id_barang, @id_penjualan, @jumlah, @harga, @satuan);";
                    foreach (DataRow row in rows.Rows)
                    {
                        using (var cmd = new FbCommand(query, con))
                        {
                            cmd.CommandType = System.Data.CommandType.Text;
                            cmd.Parameters.Add("@id_barang", Convert.ToInt32(row["Id"].ToString()));
                            cmd.Parameters.Add("@id_penjualan", id_penjualan);
                            cmd.Parameters.Add("@jumlah", Convert.ToDouble(row["Jumlah"].ToString()));
                            cmd.Parameters.Add("@harga", Convert.ToDouble(row["Total"].ToString()));
                            cmd.Parameters.Add("@satuan", row["Satuan"].ToString());
                            cmd.ExecuteNonQuery();
                            cmd.Dispose();
                        }
                        UpdateStockBarangPenjualan(row);
                    }
                }
            }
            catch (Exception ex)
            {
                DeleteSavePenjualan(id_penjualan: id_penjualan);
                throw ex;
            }
        }


        internal static void SaveKredit(
            string kode_transaksi, int id_kasir, int id_pelanggan, double total_bayar, DateTime tanggal, DateTime waktu,
            DateTime tgl_tenggat_bayar, double pembayaran_awal,
            string id_kas, DataTable dt, string keterangan,
            string status_pembayaran = "KREDIT")
        {
            int id_penjualan = SavePenjualan(kode_transaksi: kode_transaksi, id_kasir: id_kasir, id_pelanggan: id_pelanggan, total_bayar: total_bayar, tanggal: tanggal, waktu: waktu, id_kas: id_kas, status_pembayaran: status_pembayaran, keterangan: keterangan);

            try
            { /// save detail penjualan
                int id_piutang = SavePiutang(id_pelanggan: id_pelanggan, id_penjualan: id_penjualan, tgl_tenggat_bayar: tgl_tenggat_bayar, tgl_beli: tanggal, total: total_bayar, pembayaran_awal: pembayaran_awal, id_kas: id_kas);
                try
                {
                    using (var con = ConnectDB.Connetc())
                    {
                        var query = "insert into detail_penjualan " +
                            "(id_barang, id_penjualan, jumlah, harga, satuan) " +
                            "values (@id_barang, @id_penjualan, @jumlah, @harga, @satuan);";
                        foreach (DataRow row in dt.Rows)
                        {
                            using (var cmd = new FbCommand(query, con))
                            {
                                cmd.CommandType = System.Data.CommandType.Text;
                                cmd.Parameters.Add("@id_barang", Convert.ToInt32(row["Id"].ToString()));
                                cmd.Parameters.Add("@id_penjualan", id_penjualan);
                                cmd.Parameters.Add("@jumlah", Convert.ToDouble(row["Jumlah"].ToString()));
                                cmd.Parameters.Add("@harga", Convert.ToDouble(row["Total"].ToString()));
                                cmd.Parameters.Add("@satuan", row["Satuan"].ToString());
                                cmd.ExecuteNonQuery();
                                cmd.Dispose();
                            }
                            UpdateStockBarangPenjualan(row);
                        }
                    }
                }
                catch (Exception ex)
                {
                    DeleteSavePenjualan(id_penjualan);
                    DeleteSavePiutang(id_piutang);
                    throw ex;
                }
            }
            catch (Exception ex) { DeleteSavePenjualan(id_penjualan); throw ex; }

        }


        internal static int SavePiutang(int id_pelanggan, int id_penjualan, DateTime tgl_tenggat_bayar, DateTime tgl_beli, double total, double pembayaran_awal, string id_kas, string sudah_lunas = "belum")
        {
            int ids = 0;
            using (var con = ConnectDB.Connetc())
            {
                var query = "insert into piutang " +
                    "(id_pelanggan, id_penjualan, tgl_tenggat_bayar, total, sudah_lunas) " +
                    "values (@id_pelanggan, @id_penjualan, @tgl_tenggat_bayar, @total, @sudah_lunas) " +
                    "returning id;";
                using (var cmd = new FbCommand(query, con))
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.Add("@id_pelanggan", id_pelanggan);
                    cmd.Parameters.Add("@id_penjualan", id_penjualan);
                    cmd.Parameters.Add("@tgl_tenggat_bayar", tgl_tenggat_bayar);
                    cmd.Parameters.Add("@total", total);
                    cmd.Parameters.Add("@sudah_lunas", sudah_lunas);
                    ids = (int)cmd.ExecuteScalar();
                    cmd.Dispose();
                }
                BayarPiutang(id_piutang: ids, pembayaran: pembayaran_awal, tgl_bayar: tgl_beli, id_kas: id_kas);
                return ids;
            }
        }

        internal static void BayarPiutang(int id_piutang, double pembayaran, DateTime tgl_bayar, string id_kas)
        {
            using (var con = ConnectDB.Connetc())
            {
                var query = "insert into bayar_piutang " +
                    "(id_piutang, pembayaran, tgl_bayar, id_kas) " +
                    "values (@id_piutang, @pembayaran, @tgl_bayar, @id_kas);";
                using (var cmd = new FbCommand(query, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("@id_piutang", id_piutang);
                    cmd.Parameters.Add("@pembayaran", pembayaran);
                    cmd.Parameters.Add("@tgl_bayar", tgl_bayar);
                    cmd.Parameters.Add("@id_kas", id_kas);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
            }
        }

        internal static void DeleteSavePenjualan(int id_penjualan)
        {
            using (var con = ConnectDB.Connetc())
            {
                var query = "delete from penjualan where id=@id;";
                using (var cmd = new FbCommand(query, con))
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.Add("@id", id_penjualan);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
            }
        }

        internal static void DeleteSavePiutang(int id_piutang)
        {
            using (var con = ConnectDB.Connetc())
            {
                /// hapus bayar hutang dulu
                var query_ = "delete from bayar_piutang where id_piutang=@id_piutang";
                using (var cmd = new FbCommand(query_, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("@id_piutang", id_piutang);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
                var query = "delete from piutang where id=@id;";
                using (var cmd = new FbCommand(query, con))
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.Add("@id", id_piutang);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
            }
        }

        internal static int GetIdPelangganUmum()
        {
            int ids = 0;
            using (var con = ConnectDB.Connetc())
            {
                var query = "select id from pelanggan where NAMA='UMUM';";
                using (var cmd = new FbCommand(query, con))
                {
                    var x = cmd.ExecuteReader();
                    if (x.FieldCount > 0)
                    {
                        while (x.Read())
                        {
                            ids = Convert.ToInt32(x[x.FieldCount - 1].ToString());
                        }
                    }
                }
            }
            return ids;
        }

        internal static int GenerateNoTransaksiPenjualan()
        {
            int num = 0;
            using (var con = ConnectDB.Connetc())
            {
                var query = "select first 1 kode_transaksi from penjualan where tanggal=current_date order by id desc;";
                using (var cmd = new FbCommand(query, con))
                {
                    var x = cmd.ExecuteReader();
                    if (x.FieldCount > 0)
                    {
                        while (x.Read())
                        {
                            string[] dump_ = x[x.FieldCount - 1].ToString().Trim().Split('-');
                            num = Convert.ToInt32(dump_[1]);
                        }
                    }
                }
            }
            return num + 1;
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

        internal static void UpdateStockBarangPenjualan(DataRow row)
        {
            int id_barang = Convert.ToInt32(row["Id"].ToString());
            var stock_db = GetStockBarang(id_barang);
            double stock_minus = Convert.ToDouble(row["Jumlah"].ToString());
            if (row["Satuan"].ToString().ToLower() == "packs")
            {
                stock_minus *= 10;
            }
            var stock_ = stock_db - stock_minus;
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

        internal static DataTable GetKodeBarang(string kode)
        {
            DataTable data = new DataTable();
            //data.Columns.Add("id", typeof(int));
            //data.Columns.Add("kode", typeof(string));
            //data.Columns.Add("nama", typeof(string));
            using (var con = ConnectDB.Connetc())
            {
                var query = "select id_barang as id, kode, nama_barang as nama from barang where kode=@kode";
                using (var cmd = new FbCommand(query, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("@kode", kode);
                    FbDataAdapter da = new FbDataAdapter(cmd);
                    try
                    {
                        da.Fill(data);
                    }
                    catch (Exception) { }
                    finally { cmd.Dispose(); }
                }
            }
            return data;
        }
        internal static DataTable GetNamaBarang(string nama)
        {
            DataTable data = new DataTable();
            //data.Columns.Add("id", typeof(int));
            //data.Columns.Add("kode", typeof(string));
            //data.Columns.Add("nama", typeof(string));
            using (var con = ConnectDB.Connetc())
            {
                var query = "select id_barang as id, kode, NAMA_BARANG as nama from barang where NAMA_BARANG=@nama";
                using (var cmd = new FbCommand(query, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("@nama", nama.ToUpper());
                    FbDataAdapter da = new FbDataAdapter(cmd);
                    try
                    {
                        da.Fill(data);
                    }
                    catch (Exception) { }
                    finally { cmd.Dispose(); }
                }
            }
            return data;
        }

        internal static DataTable GetHistoriPenjualan()
        {
            DataTable data = new DataTable();
            using (var con = ConnectDB.Connetc())
            {
                var query = "select p.id, p.kode_transaksi as no_transaksi, k.nama as nama_kasir, " +
                    "pe.nama as nama_pelanggan, p.total, p.tanggal, " +
                    "p.status_pembayaran as pembayaran, ka.nama as nama_kas, " +
                    "p.keterangan " +
                    "from penjualan as p " +
                    "left join kasir as k " +
                    "on p.id_kasir=k.id " +
                    "left join pelanggan as pe " +
                    "on p.id_pelanggan=pe.id " +
                    "left join kas_master as ka " +
                    "on p.id_kas=ka.id " +
                    "order by p.tanggal desc;";
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
