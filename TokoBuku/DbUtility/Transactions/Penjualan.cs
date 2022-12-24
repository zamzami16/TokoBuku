using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using TokoBuku.DbUtility;

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
            string kode_transaksi, int id_kasir, int id_pelanggan,  DateTime tanggal, DateTime waktu, double total_bayar,
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
                        /// TODO: Update stock -tips: taruh di loop data detail_
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
                int id_piutang = SavePiutang(id_penjualan: id_penjualan, tgl_tenggat_bayar: tgl_tenggat_bayar, pembayaran_awal: pembayaran_awal);
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
                            /// TODO: Update stock -tips: taruh di loop data detail_
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
            catch (Exception ex)
            {
                throw ex;
            }
        }


        internal static int SavePiutang(int id_penjualan, DateTime tgl_tenggat_bayar, double pembayaran_awal, int sudah_lunas = 0)
        {
            int ids = 0;
            using (var con = ConnectDB.Connetc())
            {
                var query = "insert into piutang " +
                    "(id_penjualan, tgl_tenggat_bayar, pembayaran_awal, sudah_lunas) " +
                    "values (@id_penjualan, @tgl_tenggat_bayar, @pembayaran_awal, @sudah_lunas) " +
                    "returning id;";
                using (var cmd = new FbCommand(query, con))
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.Add("@id_penjualan", id_penjualan);
                    cmd.Parameters.Add("@tgl_tenggat_bayar", tgl_tenggat_bayar);
                    cmd.Parameters.Add("@pembayaran_awal", pembayaran_awal);
                    cmd.Parameters.Add("@sudah_lunas", sudah_lunas);
                    ids = (int)cmd.ExecuteScalar();
                    cmd.Dispose();
                }
                return ids;
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
                            ids = Convert.ToInt32(x[x.FieldCount- 1].ToString());
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
    }
}
