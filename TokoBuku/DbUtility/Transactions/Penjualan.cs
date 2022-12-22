using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TokoBuku.DbUtility;

namespace TokoBuku.DbUtility.Transactions
{
    internal static class Penjualan
    {
        public static void SaveCash(
            string kode_transaksi, int id_kasir, int id_barang,
            double quantity, DateTime tanggal, DateTime waktu,
            int id_kas, string status_pembayaran = "CASH")
        {
            using (var con = ConnectDB.Connetc())
            {
                var query = "insert into Penjualan " +
                    "(kode_transaksi, id_kasir, id_barang, quantity, " +
                    "tanggal, waktu, status_pembayaran, id_kas) " +
                    "values (@kode_transaksi, @id_kasir, @id_barang, @quantity, " +
                    "@tanggal, @waktu, @status_pembayaran, @id_kas);";
                using (var cmd = new FbCommand(query, con))
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.Add("@kode_transaksi", kode_transaksi);
                    cmd.Parameters.Add("@id_kasir", id_kasir);
                    cmd.Parameters.Add("id_barang", id_barang);
                    cmd.Parameters.Add("@quantity", quantity);
                    cmd.Parameters.Add("@tanggal", tanggal);
                    cmd.Parameters.Add("@waktu", waktu);
                    cmd.Parameters.Add("@status_pembayaran", status_pembayaran);
                    cmd.Parameters.Add("@id_kas", id_kas);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
            }
        }

        public static int SaveKredit(
            string kode_transaksi, int id_kasir, int id_barang,
            double quantity, DateTime tanggal, DateTime waktu,
            DateTime tgl_tenggat_bayar, double pembayaran_awal,
            string id_kas, string status_pembayaran = "KREDIT")
        {
            int ids = 0;
            using (var con = ConnectDB.Connetc())
            {
                var query = "insert into Penjualan " +
                    "(kode_transaksi, id_kasir, id_barang, quantity, " +
                    "tanggal, waktu, tgl_tenggat_bayar, pembayaran_awal, " +
                    "status_pembayaran, id_kas) " +
                    "values (@kode_transaksi, @id_kasir, @id_barang, @quantity, " +
                    "@tanggal, @waktu, @tgl_tenggat_bayar, @pembayaran_awal, " +
                    "@status_pembayaran, @id_kas) returning id;";
                using (var cmd = new FbCommand(query, con))
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.Add("@kode_transaksi", kode_transaksi);
                    cmd.Parameters.Add("@id_kasir", id_kasir);
                    cmd.Parameters.Add("id_barang", id_barang);
                    cmd.Parameters.Add("@quantity", quantity);
                    cmd.Parameters.Add("@tanggal", tanggal);
                    cmd.Parameters.Add("@waktu", waktu);
                    cmd.Parameters.Add("@tgl_tenggat_bayar", tgl_tenggat_bayar);
                    cmd.Parameters.Add("@pembayaran_awal", pembayaran_awal);
                    cmd.Parameters.Add("@status_pembayaran", status_pembayaran);
                    cmd.Parameters.Add("@id_kas", id_kas);
                    ids = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                    cmd.Dispose();
                }
                return ids;
            }
        }

        public static void SavePiutang(int id_penjualan, int id_pelanggan)
        {
            using (var con = ConnectDB.Connetc())
            {
                var query = "insert into piutang " +
                    "(id_pelanggan, id_penjualan) " +
                    "values (@pel, @pen);";
                using (var cmd = new FbCommand(query, con))
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.Add("@pel", id_pelanggan);
                    cmd.Parameters.Add("@pen", id_penjualan);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
            }
        }

        public static void DeleteSaveKredit(int id_penjualan)
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

        public static int GetIdPelangganUmum()
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

        public static int GenerateNoTransaksiPenjualan()
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
