using FirebirdSql.Data.FirebirdClient;
using System.Windows.Forms;

namespace TokoBuku.DbUtility
{
    static public class ConnectDB
    {
        static public FbConnection Connetc()
        {
            string path = Application.StartupPath + @"\Database\TOKOBUKU.fdb";
            FbConnection conn = new FbConnection(@"User ID=SYSDBA;Password=masterkey;Database=localhost:C:\Users\yusuf\OneDrive\Desktop\Axata\DB\TOKOBUKU.fdb");
            /*FbConnection conn = new FbConnection(@"User ID=SYSDBA;Password=masterkey;Database=localhost:" + path);*/
            conn.Open();
            return conn;
        }
    }
}
