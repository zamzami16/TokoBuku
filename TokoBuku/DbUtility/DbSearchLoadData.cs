using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using FirebirdSql.Data.FirebirdClient;

namespace TokoBuku.DbUtility
{
    static public class DbSearchLoadData
    {
        public static DataTable Pelanggan()
        {
            DataTable dt = new DataTable();
            var query = "select id, nama from pelanggan;";
            FbDataAdapter da = new FbDataAdapter(query, ConnectDB.Connetc());
            //da.SelectCommand.Parameters.Add("@id", 123);
            da.Fill(dt);
            da.Dispose();
            return dt;
        }

        public static DataTable Barang()
        {
            DataTable dt = new DataTable();
            var query = "select id_barang, nama_barang, kode from barang;";
            FbDataAdapter da = new FbDataAdapter(query, ConnectDB.Connetc());
            //da.SelectCommand.Parameters.Add("@id", 123);
            da.Fill(dt);
            da.Dispose();
            return dt;
        }

        public static DataTable Kas()
        {
            DataTable dt = new DataTable();
            var query = "select id, nama from kas_master;";
            FbDataAdapter da = new FbDataAdapter(query, ConnectDB.Connetc());
            da.Fill(dt);
            da.Dispose();
            return dt;
        }

        public static DataTable ListPelanggan()
        {
            DataTable dt = new DataTable();
            var query = "select nama";
            return dt;
        }

    }

}
