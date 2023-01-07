using FirebirdSql.Data.FirebirdClient;
using System;
using System.Data;
using TokoBuku.BaseForm.TipeData.DataBase;

namespace TokoBuku.DbUtility.Transactions.HutangPiutang
{
    internal static class BayarHutangPelanggan
    {
        internal static DataTable DataHutangPelanggan(int id_pelanggan)
        {
            DataTable table = new DataTable();
            using (var con = ConnectDB.Connetc())
            {
                var query = "select pi.id as id_piutang, pi.id_pelanggan, pel.nama as nama_pelanggan, pi.id_penjualan, pen.kode_transaksi, pi.tgl_tenggat_bayar, pi.total as total_hutang, coalesce(pembayaran_piutang.piutang_terbayar, 0) as piutang_terbayar, (pi.total - coalesce(pembayaran_piutang.piutang_terbayar, 0)) as piutang_belum_dibayar from piutang as pi left join (select bp.id_piutang, sum(bp.pembayaran) as piutang_terbayar from bayar_piutang as bp   where bp.is_dp='bukan' group by bp.id_piutang) as pembayaran_piutang on pi.id=pembayaran_piutang.id_piutang inner join pelanggan as pel on pi.id_pelanggan=pel.id inner join penjualan as pen on pi.id_penjualan=pen.id where pi.id_pelanggan=@id_pelanggan;";
                using (var cmd = new FbCommand(query, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("@id_pelanggan", id_pelanggan);
                    FbDataAdapter da = new FbDataAdapter(cmd);
                    da.Fill(table);
                    da.Dispose();
                }
            }
            return table;
        }

        internal static void BayarHutang(TBayarPiutang bayarPiutang, double kembalian, TLunas lunas=TLunas.Belum)
        {
            using (var con = ConnectDB.Connetc())
            {
                var query = "insert into bayar_piutang " +
                    "(id_piutang, pembayaran, tgl_bayar, id_kas, is_dp) " +
                    "values (@id_piutang, @pembayaran, @tgl_bayar, @id_kas, @isdp);";
                using (var cmd = new FbCommand(query, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("@id_piutang", bayarPiutang.IdPiutang);
                    cmd.Parameters.Add("@pembayaran", bayarPiutang.Pembayaran);
                    cmd.Parameters.Add("@tgl_bayar", bayarPiutang.TglBayar);
                    cmd.Parameters.Add("@id_kas", bayarPiutang.IdKas);
                    cmd.Parameters.Add("@isdp", bayarPiutang.isDP);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
                UpdateKas.TambahKasHutangPelanggan(bayarPiutang);
                if (lunas == TLunas.Lunas)
                {
                    using (var cmd = new FbCommand())
                    {
                        cmd.CommandText = "update piutang set sudah_lunas=@sudah_lunas where id=@id_piutang";
                        cmd.Parameters.Add("@sudah_lunas", lunas);
                        cmd.Parameters.Add("@id_piutang", bayarPiutang.IdPiutang);
                        cmd.Connection = con;
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                    }
                    SimpanKembalian(kembalian, bayarPiutang.IdPiutang);
                }
            }
        }

        internal static void SimpanKembalian(double kembalian, int IdPiutang)
        {
            using (var con = ConnectDB.Connetc())
            {
                var query = "update piutang set kembalian=@kembalian where id=@id_piutang;";
                using (var cmd = new FbCommand(query, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("@kembalian", kembalian);
                    cmd.Parameters.Add("@id_piutang", IdPiutang);
                    cmd.ExecuteNonQuery ();
                    cmd.Dispose();
                }
            }
        }
    }
}
