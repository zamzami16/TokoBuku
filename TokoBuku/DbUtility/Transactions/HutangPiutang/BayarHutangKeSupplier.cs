using FirebirdSql.Data.FirebirdClient;
using System;
using System.Data;
using TokoBuku.BaseForm.TipeData.DataBase;

namespace TokoBuku.DbUtility.Transactions.HutangPiutang
{
    internal static class BayarHutangKeSupplier
    {
        internal static DataTable DataHutangKeSupplier(int id_supplier)
        {
            DataTable dataTable = new DataTable();
            using (var con = ConnectDB.Connetc())
            {
                var query = "select hu.id, " +
                    "hu.id_supplier, " +
                    "sup.nama as nama_supplier, " +
                    "hu.id_pembelian, " +
                    "pem.no_nota as nota_pembelian, " +
                    "hu.tgl_tenggat_bayar as tenggat_bayar, " +
                    "coalesce(hu.total, 0) as total_hutang, " +
                    "coalesce(sudah_bayar.tot_sudah_pembayaran, 0) as sudah_dibayar, " +
                    "(coalesce(hu.total, 0) - coalesce(sudah_bayar.tot_sudah_pembayaran, 0)) as belum_bayar " +
                    "from hutang as hu " +
                    "left join " +
                    "(select bh.id_hutang, sum(bh.pembayaran) as tot_sudah_pembayaran " +
                    "from bayar_hutang as bh " +
                    "where not bh.is_dp='ya' " +
                    "group by bh.id_hutang) as sudah_bayar " +
                    "on hu.id=sudah_bayar.id_hutang " +
                    "inner join supplier as sup " +
                    "on hu.id_supplier=sup.id " +
                    "inner join pembelian as pem " +
                    "on hu.id_pembelian=pem.id_pembelian " +
                    "where hu.sudah_lunas='Belum' " +
                    "and hu.id_supplier=@id_supplier;";
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

        /*internal static void BayarHutang(int id_hutang, double pembayaran, DateTime tgl_bayar, string id_kas, bool lunas = false)
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
        }*/

        internal static void BayarHutang(TBayarHutang bayarHutang, TLunas lunas=TLunas.Belum)
        {
            using (var con = ConnectDB.Connetc())
            {
                var query = "insert into bayar_hutang " +
                    "(id_hutang, pembayaran, tgl_bayar, id_kas, is_dp) " +
                    "values (@id_hutang, @pembayaran, @tgl_bayar, @id_kas, @is_dp);";
                using (var cmd = new FbCommand(query, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("@id_hutang", bayarHutang.IdHutang);
                    cmd.Parameters.Add("@pembayaran", bayarHutang.Pembayaran);
                    cmd.Parameters.Add("@tgl_bayar", bayarHutang.TglBayar);
                    cmd.Parameters.Add("@id_kas", bayarHutang.IdKas);
                    cmd.Parameters.Add("@is_dp", bayarHutang.isDP);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
                UpdateKas.KurangKasPembelianKredit(bayarHutang);
                if (lunas == TLunas.Sudah)
                {
                    var query_ = "update hutang set sudah_lunas=@sudah_lunas where id=@id_hutang";
                    using (var cmd = new FbCommand(query_, con))
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add("@sudah_lunas", lunas);
                        cmd.Parameters.Add("@id_hutang", bayarHutang.IdHutang);
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                    }
                }
            }
        }
    }
}
