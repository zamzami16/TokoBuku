using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FirebirdSql.Data.FirebirdClient;

namespace TokoBuku.DbUtility
{
    public static class DbLoadData
    {
        public static DataTable Kategori(FbConnection conn)
        {
            DataTable dt = new DataTable();
            FbDataAdapter da = new FbDataAdapter("select * from kategori", conn);
            //da.SelectCommand.Parameters.Add("@id", 123);
            da.Fill(dt);
            da.Dispose();
            return dt;
        }
    }
}
