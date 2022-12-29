using FirebirdSql.Data.FirebirdClient;

namespace TokoBuku.DbUtility
{
    internal static class Database
    {
        internal static void ResetDatabase()
        {
            using (var con = ConnectDB.Connetc())
            {
                using (var cmd = new FbCommand())
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Connection= con;
                    cmd.CommandText = "RESETDATA";
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
            }
        }
    }
}
