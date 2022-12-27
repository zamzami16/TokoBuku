using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TokoBuku.DbUtility
{
    internal static class MasterDataBarang
    {
        internal static double GetPersediaan()
        {
            double persediaan = 0;
            using (var con = ConnectDB.Connetc())
            {
                var query = "select sum(beli * stock) from barang;";
                using (var cmd = new FbCommand(query, con))
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    var x = cmd.ExecuteReader();
                    if (x.FieldCount > 0)
                    {
                        while (x.Read())
                        {
                            persediaan = Convert.ToDouble(x[x.FieldCount - 1].ToString());
                        }
                    }
                }
            }
            return persediaan;
        }
    }
}
