using FirebirdSql.Data.FirebirdClient;

namespace TokoBuku.DbUtility
{
    static public class ConnectDB
    {
        static public FbConnection Connetc()
        {
            FbConnection conn = new FbConnection(@"User ID=SYSDBA;Password=masterkey;Database=localhost:C:\Users\yusuf\OneDrive\Desktop\Axata\DB\TOKOBUKU.fdb");
            conn.Open();
            return conn;
        }
    }
}
