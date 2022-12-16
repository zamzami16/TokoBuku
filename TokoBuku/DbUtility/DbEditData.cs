using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FirebirdSql.Data.FirebirdClient;

namespace TokoBuku.DbUtility
{
    public static class DbEditData
    {
        public static void Pelanggan(int Ids, string nama, string alamat, string no_hp, string email, string keterangan)
        {
            using (var con = ConnectDB.Connetc())
            {
                var query = "update pelanggan " +
                    "set nama=@nama, alamat=@alamat, no_hp=@hp, email=@email, keterangan=@keterangan " +
                    "where id=@id;";
                using (var cmd = new FbCommand(query, con))
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.Add("@nama", nama);
                    cmd.Parameters.Add("@alamat", alamat);
                    cmd.Parameters.Add("@hp", no_hp);
                    cmd.Parameters.Add("@email", email);
                    cmd.Parameters.Add("@keterangan", keterangan);
                    cmd.Parameters.Add("@id", Ids);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }

            }
        }

        public static void Supplier(int Ids, string nama, string alamat, string no_hp, string email, string keterangan)
        {
            using (var con = ConnectDB.Connetc())
            {
                var query = "update supplier " +
                    "set nama=@nama, alamat=@alamat, no_hp=@hp, email=@email, keterangan=@keterangan " +
                    "where id=@id;";
                using (var cmd = new FbCommand(query, con))
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.Add("@nama", nama);
                    cmd.Parameters.Add("@alamat", alamat);
                    cmd.Parameters.Add("@hp", no_hp);
                    cmd.Parameters.Add("@email", email);
                    cmd.Parameters.Add("@keterangan", keterangan);
                    cmd.Parameters.Add("@id", Ids);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }

            }
        }
        public static void Kas(int Ids, string nama, string keterangan)
        {
            using (var con = ConnectDB.Connetc())
            {
                var query = "update kas_master " +
                    "set nama=@nama, keterangan=@keterangan " +
                    "where id=@id;";
                using (var cmd = new FbCommand(query, con))
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.Add("@nama", nama);
                    cmd.Parameters.Add("@keterangan", keterangan);
                    cmd.Parameters.Add("@id", Ids);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }

            }
        }
    }
}
