using FirebirdSql.Data.FirebirdClient;
using System.Data;

namespace TokoBuku.DbUtility.Report.Master
{
    internal static class LaporanBarang
    {
        internal static DataTable Get()
        {
            DataTable dt = new DataTable();
            using (var con = ConnectDB.Connetc())
            {
                var query = "select b.id_barang as id_barang, b.nama_barang as nama_barang, (b.stock - (penj.jumlah_beli - pem.jumlah_terjual))  as stock_awal, penj.jumlah_beli, penj.rerata_harga_beli, pem.jumlah_terjual, pem.rerata_harga_jual, b.stock as stock_sekarang from (select dpem.id_barang, sum(dpem.jumlah) as jumlah_beli, avg(dpem.harga) as rerata_harga_beli from detail_pembelian as dpem group by dpem.id_barang) as penj inner join (select dp.id_barang, sum(dp.jumlah) as jumlah_terjual, avg(dp.harga_jual) as rerata_harga_jual from detail_penjualan as dp group by dp.id_barang) as pem on penj.id_barang=pem.id_barang left join barang as b on b.id_barang=penj.id_barang";
                using (var cmd = new FbCommand(query, con))
                {
                    cmd.CommandType = CommandType.Text;
                    var da = new FbDataAdapter(cmd);
                    da.Fill(dt);
                    return dt;
                }
            }
        }
    }
}
