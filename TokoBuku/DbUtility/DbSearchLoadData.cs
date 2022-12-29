using FirebirdSql.Data.FirebirdClient;
using System.Data;

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
            var query = "select id_barang, nama_barang, kode, harga_jual, stock from barang;";
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

        public static DataTable Supplier()
        {
            DataTable dt = new DataTable();
            var query = "select id, nama from supplier;";
            FbDataAdapter da = new FbDataAdapter(query, ConnectDB.Connetc());
            //da.SelectCommand.Parameters.Add("@id", 123);
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
