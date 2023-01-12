using FirebirdSql.Data.FirebirdClient;
using System;
using System.Data;

namespace TokoBuku.DbUtility.Report.Transaksi
{
    internal static class BarangTerlaris
    {
        internal static DataTable Get(int num, DateTime dateDari, DateTime dateSampai)
        {
            DataTable data = new DataTable();
            using (var con = ConnectDB.Connetc())
            {
                var query = "select first @num dpe.id_barang, dpe.nama_barang, sum(dpe.jumlah) as jumlah from penjualan as p left join (select dpen.id_penjualan, b.id_barang, b.nama_barang, dpen.jumlah from detail_penjualan as dpen left join barang as b on dpen.id_barang=b.id_barang) as dpe on p.id=dpe.id_penjualan where p.tanggal between @dateDari and @dateSampai group by dpe.id_barang, dpe.nama_barang order by jumlah desc";
                using (var cmd = new FbCommand(query, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("@num", num);
                    cmd.Parameters.Add("@dateDari", dateDari);
                    cmd.Parameters.Add("@dateSampai", dateSampai);
                    var da = new FbDataAdapter(cmd);
                    da.Fill(data);
                }
            }
            return data;
        }
    }
}
