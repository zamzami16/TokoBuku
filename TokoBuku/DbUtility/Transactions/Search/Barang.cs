using FirebirdSql.Data.FirebirdClient;
using System.Data;

namespace TokoBuku.DbUtility.Transactions.Search
{
    internal static class Barang
    {
        internal static DataTable SearchBarang(string searchterm)
        {
            DataTable data = new DataTable();
            using (var con = ConnectDB.Connetc())
            {
                var query = "select b.id_barang, " +
                    "b.kode, b.nama_barang, " +
                    "b.stock, b.harga_jual, b.harga_beli " +
                    "from barang as b " +
                    "where b.nama_barang like '%@SearchTerm%' or b.kode like '%@SearchTerm%';";
                using (var cmd = new FbCommand(query, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("@SearchTerm", searchterm);
                    FbDataAdapter da = new FbDataAdapter(cmd);
                    da.Fill(data);
                    da.Dispose();
                }
            }
            return data;
        }
    }
}
