using FirebirdSql.Data.FirebirdClient;
using System.Collections.Generic;

namespace TokoBuku.DbUtility.Login
{
    internal static class GetKasir
    {
        internal static Dictionary<string, string> LoginKasir(string uname, string pwd)
        {
            Dictionary<string, string> DataKasir = new Dictionary<string, string>();
            using (var con = ConnectDB.Connetc())
            {
                var query = "select id, nama from kasir where username=@uname and pasword=@pwd;";
                using (var cmd = new FbCommand(query, con))
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.Add("@uname", uname);
                    cmd.Parameters.Add("@pwd", pwd);
                    var x = cmd.ExecuteReader();
                    if (x.FieldCount > 0)
                    {
                        while (x.Read())
                        {
                            string id = x[0].ToString();
                            string nama = x[1].ToString();
                            DataKasir.Add("id", id);
                            DataKasir.Add("nama", nama);
                        }
                    }
                }
            }
            return DataKasir;
        }
    }
}
