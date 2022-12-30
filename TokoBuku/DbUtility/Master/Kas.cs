using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using TokoBuku.BaseForm.TipeData.DataBase;

namespace TokoBuku.DbUtility.Master
{
    internal static class Kas
    {
        internal static DataTable LoadKas()
        {
            DataTable dt = new DataTable();
            FbDataAdapter da = new FbDataAdapter("select id, nama, saldo, keterangan from kas_master", ConnectDB.Connetc());
            da.Fill(dt);
            da.Dispose();
            return dt;
        }

        static public int SaveKas(TKas kas)
        {
            using (var con = ConnectDB.Connetc())
            {
                int ids;
                var query = "insert into kas_master (nama, keterangan, saldo, status) " +
                    "values (@nama, @keterangan, @saldo, @status) " +
                    "returning Id;";
                using (var cmd = new FbCommand(query, con))
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.Add("@nama", kas.Nama);
                    cmd.Parameters.Add("@keterangan", kas.Keterangan);
                    cmd.Parameters.Add("@saldo", kas.Saldo);
                    cmd.Parameters.Add("@status", kas.Status);
                    ids = (int)cmd.ExecuteScalar();
                    cmd.Dispose();
                    return ids;
                }
            }
        }

        public static void EditKas(TKas kas)
        {
            using (var con = ConnectDB.Connetc())
            {
                var query = "update kas_master " +
                    "set nama=@nama, saldo=@saldo, keterangan=@keterangan " +
                    "where id=@id;";
                using (var cmd = new FbCommand(query, con))
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.Add("@nama", kas.Nama);
                    cmd.Parameters.Add("@saldo", kas.Saldo);
                    cmd.Parameters.Add("@keterangan", kas.Keterangan);
                    cmd.Parameters.Add("@id", kas.Id);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }

            }
        }

        static public void DeleteKas(int Ids)
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
    }
}
