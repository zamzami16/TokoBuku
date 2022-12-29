using FirebirdSql.Data.FirebirdClient;
using System;
using System.Data;

namespace TokoBuku.DbUtility.Transactions.HutangPiutang
{
    internal static class BayarHutangKeSupplier
    {
        internal static DataTable DataHutangKeSupplier(int id_supplier)
        {
            DataTable dataTable = new DataTable();
            using (var con = ConnectDB.Connetc())
            {
                var query = "select hu.id as id_hutang, hu.id_pembelian as id_pembelian, " +
                    "pem.no_nota as no_nota, hu.id_supplier, sup.nama as nama_supplier, " +
                    "(hu.total - bhu.pembayaran) as nominal_hutang, hu.tgl_tenggat_bayar " +
                    "from hutang as hu " +
                    "left join " +
                    "(select bh.id_hutang, sum(bh.pembayaran) as pembayaran " +
                    "from bayar_hutang as bh " +
                    "group by bh.id_hutang) as bhu " +
                    "on hu.id=bhu.id_hutang " +
                    "left join pembelian as pem " +
                    "on hu.id_pembelian=pem.id_pembelian " +
                    "right join supplier as sup " +
                    "on hu.id_supplier=sup.id " +
                    "where sup.id=@id_supplier " +
                    "and hu.sudah_lunas='belum';";
                using (var cmd = new FbCommand(query, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("@id_supplier", id_supplier);
                    FbDataAdapter da = new FbDataAdapter(cmd);
                    da.Fill(dataTable);
                    da.Dispose();
                }
            }
            return dataTable;
        }

        internal static void BayarHutang(int id_hutang, double pembayaran, DateTime tgl_bayar, string id_kas, bool lunas = false)
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
                if (lunas)
                {
                    var lunas_ = "sudah";
                    var query_ = "update hutang set sudah_lunas=@sudah_lunas where id=@id_hutang";
                    using (var cmd = new FbCommand(query_, con))
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add("@sudah_lunas", lunas_);
                        cmd.Parameters.Add("@id_hutang", id_hutang);
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                    }
                }
            }
        }
    }
}
