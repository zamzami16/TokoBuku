using FirebirdSql.Data.FirebirdClient;
using System;

namespace TokoBuku.DbUtility
{
    internal static class MasterDataBarang
    {
        internal static double GetPersediaan()
        {
            double persediaan = 0;
            using (var con = ConnectDB.Connetc())
            {
                var query = "select sum(harga_beli * stock) as harga from barang;";
                using (var cmd = new FbCommand(query, con))
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    var x = cmd.ExecuteReader();
                    if (x.FieldCount > 0)
                    {
                        while (x.Read())
                        {
                            try { persediaan = Convert.ToDouble(x[x.FieldCount - 1].ToString()); }
                            catch (Exception) { persediaan= 0; } 
                        }
                    }
                }
            }
            return persediaan;
        }
    }
}
