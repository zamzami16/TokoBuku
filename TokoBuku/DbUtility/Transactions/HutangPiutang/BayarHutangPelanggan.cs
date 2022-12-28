using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace TokoBuku.DbUtility.Transactions.HutangPiutang
{
    internal static class BayarHutangPelanggan
    {
        internal static DataTable DataHutangPelanggan(int id_pelanggan)
        {/// TODO: Cari ini ya ges ya
            DataTable table= new DataTable();
            using (var con = ConnectDB.Connetc())
            {
                var query = "select pi.id as id_piutang, pi.id_pelanggan, pi.id_penjualan, pen.kode_transaksi, pel.nama as nama_pelanggan, (pi.total - tterbayar.terbayar) as total, pi.tgl_tenggat_bayar as tenggat_bayar from piutang as pi inner join (select bp.id_piutang, sum(bp.pembayaran) as terbayar from bayar_piutang as bp group by bp.id_piutang) as tterbayar on pi.id=tterbayar.id_piutang inner join penjualan as pen on pi.id_penjualan=pen.id inner join pelanggan as pel on pi.id_pelanggan=pel.id where pi.id_pelanggan=10 and pi.sudah_lunas='belum'";
                using (var cmd = new FbCommand(query, con))
                {
                    cmd.CommandType= CommandType.Text;
                    cmd.Parameters.Add("@id_pelanggan", id_pelanggan);
                    FbDataAdapter da = new FbDataAdapter(cmd);
                    da.Fill(table);
                    da.Dispose();
                }
            }
            return table;
        }

        internal static void BayarHutang(int id_piutang, DateTime tanggal_bayar, double pembayaran, string id_kas, bool sudah_lunas = false)
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
                    cmd.Parameters.Add("@tgl_bayar", tanggal_bayar);
                    cmd.Parameters.Add("@id_kas", id_kas);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
                if (sudah_lunas)
                {
                    string lunas = "sudah";
                    using (var cmd = new FbCommand())
                    {
                        cmd.CommandText = "update piutang set sudah_lunas=@sudah_lunas where id=@id_piutang";
                        cmd.Parameters.Add("@sudah_lunas", lunas);
                        cmd.Parameters.Add("@id_piutang", id_piutang);
                        cmd.Connection = con;
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                    }
                }
            }
        }

    }
}
