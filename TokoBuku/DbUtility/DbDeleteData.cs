using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FirebirdSql.Data.FirebirdClient;

namespace TokoBuku.DbUtility
{
    static public class DbDeleteData
    {
        static public void Pelanggan(int Ids)
        {
            using (var con = ConnectDB.Connetc())
            {
                var query = "delete from pelanggan where Id=@id;";
                using (var cmd = new FbCommand(query, con))
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.Add("@id", Ids);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
            }
        }

        static public void Supplier(int Ids)
        {
            using (var con = ConnectDB.Connetc())
            {
                var query = "delete from supplier where Id=@id;";
                using (var cmd = new FbCommand(query, con))
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.Add("@id", Ids);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
            }
        }
        static public void Kas(int Ids)
        {
            using (var con = ConnectDB.Connetc())
            {
                var query = "delete from kas_master where Id=@id;";
                using (var cmd = new FbCommand(query, con))
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.Add("@id", Ids);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
            }
        }
        static public void Kasir(int Ids)
        {
            using (var con = ConnectDB.Connetc())
            {
                var query = "delete from kasir where Id=@id;";
                using (var cmd = new FbCommand(query, con))
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.Add("@id", Ids);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
            }
        }
    }
}
