using FirebirdSql.Data.FirebirdClient;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TokoBuku.DbUtility.Transactions
{
    internal static class Pembelian
    {
        public static int SavePembelian_cash(int id_supplier, DateTime tanggal_beli,
            string no_nota, double total, int id_kas, string status_pembayaran="CASH")
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
                var id_hutang = SaveHutang(id_pembelian: ids, tgl_tenggat_bayar: tgl_tenggat_bayar, pembayaran_awal: pembayaran_awal);
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

        internal static int SaveHutang(int id_pembelian, DateTime tgl_tenggat_bayar, double pembayaran_awal)
        {
            using (var con = ConnectDB.Connetc())
            {
                int id = 0;
                var query = "insert into hutang " +
                    "(id_pembelian, tgl_tenggat_bayar, pembayaran_awal) " +
                    "values (@id_pembelian, @tgl_tenggat_bayar, @pembayaran_awal) " +
                    "returning id;";
                using (var cmd = new FbCommand(query, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("@id_pembelian", id_pembelian);
                    cmd.Parameters.Add("@tgl_tenggat_bayar", tgl_tenggat_bayar);
                    cmd.Parameters.Add("@pembayaran_awal", pembayaran_awal);
                    id = (int)cmd.ExecuteScalar();
                    cmd.Dispose();
                }
                return id;
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
    }
}
