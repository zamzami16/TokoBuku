using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace TokoBuku.DbUtility.Report.HutangPiutang
{
    internal static class GetHutangPiutang
    {
        internal static DataTable GetHutang()
        {
            DataTable dt = new DataTable();
            using (var con = ConnectDB.Connetc())
            {
                var query = "select hu.id as id_hutang, pem.no_nota, sup.nama as nama_supplier, hu.tgl_tenggat_bayar as tenggat, hu.total, hu.sudah_lunas, bhud.dp, bhun.pembayaran as sudah_bayar, (hu.total - bhud.dp - bhun.pembayaran) as hutang_sekarang from hutang as hu left join (select bh.id_hutang, coalesce(sum(bh.pembayaran), 0) as pembayaran from bayar_hutang as bh  where bh.is_dp='bukan' group by bh.id_hutang) as bhun on hu.id=bhun.id_hutang left join (select bh.id_hutang, sum(bh.pembayaran) as dp from bayar_hutang as bh    where bh.is_dp='ya' group by bh.id_hutang) as bhud on hu.id=bhud.id_hutang left join pembelian as pem on hu.id_pembelian=pem.id_pembelian left join supplier as sup on hu.id_supplier=sup.id order by hu.id asc;";
                using (var cmd = new FbCommand(query, con))
                {
                    cmd.CommandType= CommandType.Text;
                    FbDataAdapter fb = new FbDataAdapter(cmd);
                    fb.Fill(dt);
                }
            }
            return dt;
        }

        internal static DataTable GetPiutang()
        {
            DataTable dt = new DataTable();
            using (var con = ConnectDB.Connetc())
            {
                var query = "select pi.id as id_piutang, pen.kode_transaksi, pel.nama as nama_pelanggan, pi.tgl_tenggat_bayar as tenggat, coalesce(pi.total,0) as total_hutang, coalesce(bhp.sudah_bayar,0) as sudah_bayar, (coalesce(pi.total,0) - coalesce(bhp.sudah_bayar,0)) as belum_dibayar, pi.sudah_lunas from piutang as pi left join (select bp.id_piutang, sum(coalesce(bp.pembayaran, 0)) as sudah_bayar from bayar_piutang as bp group by bp.id_piutang) as bhp on pi.id=bhp.id_piutang left join penjualan as pen on pi.id_penjualan=pen.id left join pelanggan as pel on pi.id_pelanggan=pel.id";
                using (var cmd = new FbCommand(query, con))
                {
                    cmd.CommandType = CommandType.Text;
                    FbDataAdapter fb = new FbDataAdapter(cmd);
                    fb.Fill(dt);
                }
            }
            return dt;
        }
    }
}
