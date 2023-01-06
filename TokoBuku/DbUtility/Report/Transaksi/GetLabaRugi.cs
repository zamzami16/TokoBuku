using FirebirdSql.Data.FirebirdClient;
using System;
using System.Data;

namespace TokoBuku.DbUtility.Report.Transaksi
{
    internal static class GetLabaRugi
    {
        public static DataTable Get()
        {
            DataTable dt = new DataTable();
            try
            {
                using (var con = ConnectDB.Connetc())
                {
                    using (var cmd = new FbCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "select p.id, p.tanggal, p.status_pembayaran, dp.harga_jual, (dp.harga_jual - p.total) as tot_disc_pot, p.total, dp.harga_beli, (p.total - dp.harga_beli) as laba_kotor from penjualan as p inner join (select d.id_penjualan, sum(d.jumlah) as jumlah, sum(d.harga_jual * d.jumlah) as harga_jual, sum(d.harga_beli * d.jumlah) as harga_beli, d.satuan from detail_penjualan as d group by d.id_penjualan, d.satuan) as dp on p.id=dp.id_penjualan;";
                        cmd.Connection= con;
                        FbDataAdapter fba = new FbDataAdapter(cmd);
                        fba.Fill(dt);
                        fba.Dispose();
                        cmd.Dispose();
                        con.Dispose();
                        return dt;
                    }
                }
            }
            catch (Exception) { throw; }
        }
    }
}
