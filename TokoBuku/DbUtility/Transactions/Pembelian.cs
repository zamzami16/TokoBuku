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
        public static int SavePembelian(int id_supplier, DateTime tanggal_beli,
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

        public static void SavePembelianCash(int id_supplier, DateTime tanggal_beli,
            string no_nota, double total, string status_pembayaran, int id_kas, DataTable rows)
        {
            var ids = SavePembelian(id_supplier: id_supplier, tanggal_beli: tanggal_beli,
                no_nota: no_nota, total: total, status_pembayaran: status_pembayaran, id_kas: id_kas);
            try
            {
                var rowss = rows;
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
