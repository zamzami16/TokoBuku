using FirebirdSql.Data.FirebirdClient;
using System;
using System.Data;

namespace TokoBuku.DbUtility.Transactions
{
    static public class GetDataPartial
    {
        static public DataTable Barang(int Ids, double jumlah, string satuan)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Id", typeof(string));        // 0
            dataTable.Columns.Add("Kode", typeof(string));      // 1
            dataTable.Columns.Add("Nama", typeof(string));      // 2
            dataTable.Columns.Add("Jumlah", typeof(double));    // 3
            dataTable.Columns.Add("Satuan", typeof(string));    // 4
            dataTable.Columns.Add("Harga", typeof(double));     // 5
            dataTable.Columns.Add("Diskon", typeof(double));    // 6
            dataTable.Columns.Add("Total", typeof(double));     // 7
            dataTable.Columns[3].DefaultValue = jumlah;
            dataTable.Columns[4].DefaultValue = satuan;
            var query = "select id_barang AS ID, kode as KODE, nama_barang AS NAMA, harga_jual as harga, diskon " +
                "from barang where id_BARANG=@id;";
            using (FbCommand cmd = new FbCommand(query, ConnectDB.Connetc()))
            {
                cmd.Parameters.Add("@id", Ids);
                FbDataAdapter fbData = new FbDataAdapter(cmd);
                fbData.Fill(dataTable);
                if (satuan.ToLower() == "packs")
                {
                    DataRow harga = dataTable.Rows[0];
                    double harga_ = Convert.ToDouble(harga["Harga"].ToString()) * 10;
                    dataTable.Rows[0].SetField("Harga", harga_);
                }
                return dataTable;
            }
        }
    }
}
