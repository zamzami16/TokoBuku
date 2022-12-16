using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FirebirdSql.Data.FirebirdClient;

namespace TokoBuku.DbUtility
{
    public static class DbLoadData
    {
        public static DataTable Kategori(FbConnection conn)
        {
            DataTable dt = new DataTable();
            FbDataAdapter da = new FbDataAdapter("select * from kategori", conn);
            //da.SelectCommand.Parameters.Add("@id", 123);
            da.Fill(dt);
            da.Dispose();
            return dt;
        }

        public static DataTable Penerbit(FbConnection conn)
        {
            DataTable dt = new DataTable();
            FbDataAdapter da = new FbDataAdapter("select * from penerbit", conn);
            //da.SelectCommand.Parameters.Add("@id", 123);
            da.Fill(dt);
            da.Dispose();
            return dt;
        }

        public static DataTable Rak(FbConnection conn)
        {
            DataTable dt = new DataTable();
            FbDataAdapter da = new FbDataAdapter("select * from rak", conn);
            //da.SelectCommand.Parameters.Add("@id", 123);
            da.Fill(dt);
            da.Dispose();
            return dt;
        }

        public static DataTable Barang(FbConnection conn)
        {
            DataTable dt = new DataTable();
            var query = "select "+
                "b.id_barang, b.nama_barang, p.nama_penerbit, k.nama, rak.nama, b.stock, " +
                "b.harga, b.isbn, b.penulis, b.diskon, b.status, b.barcode, b.keterangan " +
                "from barang b " +
                "INNER JOIN kategori k ON b.id_kategori = k.id " +
                "INNER JOIN penerbit p ON b.id_penerbit = p.id " +
                "INNER JOIN rak ON b.id_rak = rak.id;";
            FbDataAdapter da = new FbDataAdapter(query, conn);
            da.Fill(dt);
            da.Dispose();
            return dt;
        }

        public static DataTable Pelanggan(FbConnection fbConnection)
        {
            DataTable dt = new DataTable();
            var query = "select * from pelanggan";
            FbDataAdapter da = new FbDataAdapter(query, fbConnection);
            da.Fill(dt);
            da.Dispose();
            return dt;
        }
        public static DataTable Supplier()
        {
            DataTable dt = new DataTable();
            var query = "select * from supplier";
            FbDataAdapter da = new FbDataAdapter(query, ConnectDB.Connetc());
            da.Fill(dt);
            da.Dispose();
            return dt;
        }

        public static DataTable Kas()
        {
            DataTable dt = new DataTable();
            FbDataAdapter da = new FbDataAdapter("select * from kas_master", ConnectDB.Connetc());
            da.Fill(dt);
            da.Dispose();
            return dt;
        }
    }
}
