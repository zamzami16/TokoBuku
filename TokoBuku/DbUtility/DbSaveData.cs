﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FirebirdSql.Data.FirebirdClient;

namespace TokoBuku.DbUtility
{
    /// <summary>
    /// Save Data to DB
    /// </summary>
    static public class DbSaveData
    {
        static public int Pelanggan(string nama, string alamat, string email, string no_hp, string keterangan, string status="AKTIF")
        {
            using (var con = ConnectDB.Connetc())
            {
                int ids;
                var query = "insert into pelanggan (nama, alamat, no_hp, email, keterangan, status) " +
                    "values (@nama, @alamat, @no_hp, @email, @keterangan, @status) " +
                    "returning Id;";
                using (var cmd = new FbCommand(query, con))
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.Add("@nama", nama);
                    cmd.Parameters.Add("@alamat", alamat);
                    cmd.Parameters.Add("@no_hp", no_hp);
                    cmd.Parameters.Add("@email", email);
                    cmd.Parameters.Add("@keterangan", keterangan);
                    cmd.Parameters.Add("@status", status);
                    ids = (int)cmd.ExecuteScalar();
                    cmd.Dispose();
                    return ids;
                }
            }
        }

        static public int Supplier(string nama, string alamat, string email, string no_hp, string keterangan, string status = "AKTIF")
        {
            using (var con = ConnectDB.Connetc())
            {
                int ids;
                var query = "insert into supplier (nama, alamat, no_hp, email, keterangan, status) " +
                    "values (@nama, @alamat, @no_hp, @email, @keterangan, @status) " +
                    "returning Id;";
                using (var cmd = new FbCommand(query, con))
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.Add("@nama", nama);
                    cmd.Parameters.Add("@alamat", alamat);
                    cmd.Parameters.Add("@no_hp", no_hp);
                    cmd.Parameters.Add("@email", email);
                    cmd.Parameters.Add("@keterangan", keterangan);
                    cmd.Parameters.Add("@status", status);
                    ids = (int)cmd.ExecuteScalar();
                    cmd.Dispose();
                    return ids;
                }
            }
        }

        static public int Kas(string nama, string keterangan, string status = "AKTIF")
        {
            using (var con = ConnectDB.Connetc())
            {
                int ids;
                var query = "insert into kas_master (nama, keterangan, status) " +
                    "values (@nama, @keterangan, @status) " +
                    "returning Id;";
                using (var cmd = new FbCommand(query, con))
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.Add("@nama", nama);
                    cmd.Parameters.Add("@keterangan", keterangan);
                    cmd.Parameters.Add("@status", status);
                    ids = (int)cmd.ExecuteScalar();
                    cmd.Dispose();
                    return ids;
                }
            }
        }

        static public int Kasir(string nama, string username, string password, string alamat, string noHp, string keterangan, string status = "AKTIF")
        {
            using (var con = ConnectDB.Connetc())
            {
                int ids;
                var query = "insert into kasir (nama, alamat, username, pasword, no_hp, keterangan, status) " +
                    "values (@nama, @alamat, @username, @pasword, @noHp, @keterangan, @status) " +
                    "returning Id;";
                using (var cmd = new FbCommand(query, con))
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.Add("@nama", nama);
                    cmd.Parameters.Add("@alamat", alamat);
                    cmd.Parameters.Add("@username", username);
                    cmd.Parameters.Add("@pasword", password);
                    cmd.Parameters.Add("@noHp", noHp);
                    cmd.Parameters.Add("@keterangan", keterangan);
                    cmd.Parameters.Add("@status", status);
                    ids = (int)cmd.ExecuteScalar();
                    cmd.Dispose();
                    return ids;
                }
            }
        }

        static public int Rak(string nama, string keterangan, string status = "AKTIF")
        {
            using (var con = ConnectDB.Connetc())
            {
                int ids;
                var query = "insert into rak (nama, keterangan, status) " +
                    "values (@nama, @keterangan, @status) " +
                    "returning Id;";
                using (var cmd = new FbCommand(query, con))
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.Add("@nama", nama);
                    cmd.Parameters.Add("@keterangan", keterangan);
                    cmd.Parameters.Add("@status", status);
                    ids = (int)cmd.ExecuteScalar();
                    cmd.Dispose();
                    return ids;
                }
            }
        }
        /*static public int Rak(string nama, string keterangan, string status = "AKTIF")
        {
            using (var con = ConnectDB.Connetc())
            {
                int ids;
                var query = "insert into rak (nama, keterangan, status) " +
                    "values (@nama, @keterangan, @status) " +
                    "returning Id;";
                using (var cmd = new FbCommand(query, con))
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.Add("@nama", nama);
                    cmd.Parameters.Add("@keterangan", keterangan);
                    cmd.Parameters.Add("@status", status);
                    ids = (int)cmd.ExecuteScalar();
                    cmd.Dispose();
                    return ids;
                }
            }
        }*/
    }
}
