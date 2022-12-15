using FirebirdSql.Data.FirebirdClient;

namespace TokoBuku.DbUtility
{
    public class ConnectDB
    {
        public FbConnection Connetc()
        {
            FbConnection conn = new FbConnection(@"User ID=SYSDBA;Password=masterkey;Database=localhost:C:\Users\yusuf\OneDrive\Desktop\Axata\DB\TOKOBUKU.fdb");
            conn.Open();
            return conn;
        }
    }
}
