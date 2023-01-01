using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections.Generic;
using System.Data;
using TokoBuku.BaseForm.TipeData.DataBase;

namespace TokoBuku.DbUtility.Transactions
{
    internal static class Penjualan
    {
        internal static void SaveDetailPenjualan(List<TDetailPenjualan> detailPenjualan, int id_penjualan)
        {
            using (var con = ConnectDB.Connetc())
            {
                var query = "insert into detail_penjualan " +
                    "(id_penjualan, id_barang, jumlah, harga, satuan) " +
                    "values (@id_penjualan, @id_barang, @jumlah, @harga, @satuan);";
                foreach (TDetailPenjualan detail in detailPenjualan)
                {
                    using (var cmd = new FbCommand(query, con))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Parameters.Add("@id_penjualan", id_penjualan);
                        cmd.Parameters.Add("@id_barang", detail.IdBarang);
                        cmd.Parameters.Add("@jumlah", detail.Jumlah);
                        cmd.Parameters.Add("@harga", detail.Harga);
                        cmd.Parameters.Add("@satuan", detail.Satuan);
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                    }
                    // Update stock barang
                    /// TODO: Saat transaksi error, stock barang belum bisa di re-Update
                    UpdateStockBarangPenjualan(detail);
                }
            }
        }

        internal static int SavePenjualan(TPenjualan penjualan, List<TDetailPenjualan> detailPenjualan)
        {
            int ids = 0;
            using (var con = ConnectDB.Connetc())
            {
                var query = "insert into Penjualan " +
                    "(kode_transaksi, id_kasir, id_pelanggan, total, " +
                    "uang_pembayaran, uang_kembalian, potongan, tanggal, waktu, " +
                    "status_pembayaran, id_kas, keterangan) " +
                    "values (@kode_transaksi, @id_kasir, @id_pelanggan, @total, " +
                    "@uang_pembayaran, @uang_kembalian, @potongan, @tanggal, @waktu, " +
                    "@status_pembayaran, @id_kas, @keterangan)" +
                    "returning id;";
                using (var cmd = new FbCommand(query, con))
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.Add("@kode_transaksi", penjualan.KodeTransaksi);
                    cmd.Parameters.Add("@id_kasir", penjualan.IdKasir);
                    cmd.Parameters.Add("@id_pelanggan", penjualan.IdPelanggan);
                    cmd.Parameters.Add("@total", penjualan.Total);
                    cmd.Parameters.Add("@uang_pembayaran", penjualan.UangPembayaran);
                    cmd.Parameters.Add("@uang_kembalian", penjualan.UangKembalian);
                    cmd.Parameters.Add("@potongan", penjualan.Potongan);
                    cmd.Parameters.Add("@tanggal", penjualan.Tanggal);
                    cmd.Parameters.Add("@waktu", penjualan.Waktu);
                    cmd.Parameters.Add("@status_pembayaran", penjualan.StatusPembayaran);
                    cmd.Parameters.Add("@id_kas", penjualan.IdKas);
                    cmd.Parameters.Add("@keterangan", penjualan.Keterangan);
                    ids = (int)cmd.ExecuteScalar();
                    cmd.Dispose();
                }
                try
                {
                    SaveDetailPenjualan(detailPenjualan, ids);
                }
                catch (Exception)
                {
                    DeleteSavePenjualan(ids);
                    throw;
                }
                UpdateKas.TambahKasPenjualan(penjualan);
            }
            return ids;
        }

        internal static void SaveCash(TPenjualan penjualan, List<TDetailPenjualan> detailPenjualan)
        {
            SavePenjualan(penjualan, detailPenjualan);
        }


        internal static void SaveKredit(TPenjualan penjualan, List<TDetailPenjualan> detailPenjualan, TPiutang piutang)
        {
            int id_penjualan = SavePenjualan(penjualan, detailPenjualan);

            try
            {// Save piutang
                piutang.IdPenjualan = id_penjualan;
                int id_piutang = SavePiutang(piutang);
            }
            catch (Exception ex) { DeleteSavePenjualan(id_penjualan); throw ex; }
        }

        internal static int SavePiutang(TPiutang piutang)
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
                    cmd.Parameters.Add("@id_pelanggan", piutang.IdPelanggan);
                    cmd.Parameters.Add("@id_penjualan", piutang.IdPenjualan);
                    cmd.Parameters.Add("@tgl_tenggat_bayar", piutang.TanggalTenggatBayar);
                    cmd.Parameters.Add("@total", piutang.Total);
                    cmd.Parameters.Add("@sudah_lunas", piutang.Lunas);
                    ids = (int)cmd.ExecuteScalar();
                    cmd.Dispose();
                }
            }
            return ids;
        }

        internal static void BayarPiutang(TBayarPiutang bayarPiutang)
        {
            using (var con = ConnectDB.Connetc())
            {
                var query = "insert into bayar_piutang " +
                    "(id_piutang, pembayaran, tgl_bayar, id_kas, is_dp) " +
                    "values (@id_piutang, @pembayaran, @tgl_bayar, @id_kas, @is_dp);";
                using (var cmd = new FbCommand(query, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("@id_piutang", bayarPiutang.IdPiutang);
                    cmd.Parameters.Add("@pembayaran", bayarPiutang.Pembayaran);
                    cmd.Parameters.Add("@tgl_bayar", bayarPiutang.TglBayar);
                    cmd.Parameters.Add("@id_kas", bayarPiutang.IdKas);
                    cmd.Parameters.Add("@is_dp", bayarPiutang.isDP);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
            }
        }

        internal static void DeleteSavePenjualan(int id_penjualan)
        {
            using (var con = ConnectDB.Connetc())
            {
                var query_ = "delete from detail_penjualan where id_penjualan=@id_penjualan;";
                using (var cmd = new FbCommand(query_, con))
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    // delete detail penjualan
                    cmd.Parameters.Add("@id_penjualan", id_penjualan);
                    cmd.ExecuteNonQuery();
                    // delete penjualan
                    cmd.Parameters.Clear();
                    cmd.CommandText = "delete from penjualan where id=@id;";
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
            if (ids == 0)
            {
                ids = AddPelangganUmum();
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

        internal static void UpdateStockBarangPenjualan(TDetailPenjualan detail)
        {
            int id_barang = detail.IdBarang;
            var stock_db = GetStockBarang(id_barang);
            double stock_minus = detail.Jumlah;
            // Untuk sementara, satuan masih pcs tok
            /*if (row["Satuan"].ToString().ToLower() == "packs")
            {
                stock_minus *= 10;
            }*/
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

        public static int AddPelangganUmum()
        {
            int ids = 0;
            using (var con = ConnectDB.Connetc())
            {
                var query = "insert into pelanggan (nama, alamat, no_hp) " +
                    "values (@nama, @alamat, @noHp) returning id";
                var nama = "UMUM";
                var alamat = "Alamat umum";
                var noHp = "Hp Umum";
                using (var cmd = new FbCommand(query, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("@nama", nama);
                    cmd.Parameters.Add("@alamat", alamat);
                    cmd.Parameters.Add("@noHp", noHp);
                    ids = (int)cmd.ExecuteScalar();
                    cmd.Dispose();
                    return ids;
                }
            }
        }
    }
}
