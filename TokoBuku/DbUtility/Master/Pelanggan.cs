using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TokoBuku.BaseForm.TipeData.DataBase;

namespace TokoBuku.DbUtility.Master
{
    internal static class Pelanggan
    {
        internal static void GetDataPelanggan()
        {

        }
        static internal int SavePelanggan(TPelanggan pelanggan)
        {
            using (var con = ConnectDB.Connetc())
            {
                int ids;
                var query = "insert into pelanggan (nama, alamat, no_hp, email, keterangan, status) " +
                    "values (@nama, @alamat, @no_hp, @email, @keterangan, @status) " +
                    "returning id;";
                using (var cmd = new FbCommand(query, con))
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.Add("@nama", pelanggan.Nama);
                    cmd.Parameters.Add("@alamat", pelanggan.Alamat);
                    cmd.Parameters.Add("@no_hp", pelanggan.NoHp);
                    cmd.Parameters.Add("@email", pelanggan.Email);
                    cmd.Parameters.Add("@keterangan", pelanggan.Keterangan);
                    cmd.Parameters.Add("@status", pelanggan.Status);
                    ids = (int)cmd.ExecuteScalar();
                    cmd.Dispose();
                    return ids;
                }
            }
        }

        internal static void EditPelanggan(TPelanggan pelanggan)
        {
            using (var con = ConnectDB.Connetc())
            {
                var query = "update pelanggan " +
                    "set nama=@nama, alamat=@alamat, no_hp=@hp, email=@email, keterangan=@keterangan " +
                    "where id=@id;";
                using (var cmd = new FbCommand(query, con))
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.Add("@nama", pelanggan.Nama);
                    cmd.Parameters.Add("@alamat", pelanggan.Alamat);
                    cmd.Parameters.Add("@hp", pelanggan.NoHp);
                    cmd.Parameters.Add("@email", pelanggan.Email);
                    cmd.Parameters.Add("@keterangan", pelanggan.Keterangan);
                    cmd.Parameters.Add("@id", pelanggan.Id);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }

            }
        }
    }
}
