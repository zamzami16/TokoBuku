using FirebirdSql.Data.FirebirdClient;
using System.Data;

namespace TokoBuku.DbUtility.Report.Transaksi
{
    internal static class GetKartuStock
    {
        internal static DataTable GetMasuk()
        {
            DataTable dataTable = new DataTable();
            using (var con = ConnectDB.Connetc())
            {
                var query = "select pem.tanggal_beli as tanggal, dpe.nama_barang, sup.nama as sup_pel, dpe.jumlah, dpe.harga_beli as harga from pembelian as pem left join supplier as sup on pem.id_supplier=sup.id left join (select dp.id_pembelian, b.nama_barang as nama_barang, dp.jumlah, dp.harga as harga_beli from detail_pembelian as dp left join barang as b on dp.id_barang=b.id_barang) as dpe on pem.id_pembelian=dpe.id_pembelian;";
                using (var cmd = new FbCommand(query, connection: con))
                {
                    cmd.CommandType = CommandType.Text;
                    FbDataAdapter da = new FbDataAdapter(cmd);
                    da.Fill(dataTable);
                    DataColumn dataColumn = new DataColumn("Tipe", typeof(string)) { DefaultValue = "Masuk" };
                    dataTable.Columns.Add(dataColumn);
                }
            }
            return dataTable;
        }

        internal static DataTable GetKeluar()
        {
            DataTable dataTable = new DataTable();
            using (var con = ConnectDB.Connetc())
            {
                var query = "select p.tanggal, dpe.nama_barang, pel.nama as sup_pel, dpe.jumlah, dpe.harga_jual as harga from penjualan as p left join (select dpen.id_penjualan, b.nama_barang, dpen.jumlah, dpen.harga_jual from detail_penjualan as dpen left join barang as b on dpen.id_barang=b.id_barang) as dpe on p.id=dpe.id_penjualan left join pelanggan as pel on p.id_pelanggan=pel.id;";
                using (var cmd = new FbCommand(query, con))
                {
                    cmd.CommandType = CommandType.Text;
                    FbDataAdapter da = new FbDataAdapter(cmd);
                    da.Fill(dataTable);
                    DataColumn dataColumn = new DataColumn("Tipe", typeof(string)) { DefaultValue = "Keluar" };
                    dataTable.Columns.Add(dataColumn);
                }
            }
            return dataTable;
        }
    }
}
