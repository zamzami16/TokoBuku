using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FirebirdSql.Data.FirebirdClient;
using System.Data.Common;

namespace TokoBuku.DbUtility
{
    public static class DbLoadData
    {
        public static DataTable Kategori()
        {
            DataTable dt = new DataTable();
            FbDataAdapter da = new FbDataAdapter("select * from kategori", ConnectDB.Connetc());
            //da.SelectCommand.Parameters.Add("@id", 123);
            da.Fill(dt);
            da.Dispose();
            return dt;
        }

        public static DataTable Penerbit()
        {
            DataTable dt = new DataTable();
            FbDataAdapter da = new FbDataAdapter("select * from penerbit", ConnectDB.Connetc());
            //da.SelectCommand.Parameters.Add("@id", 123);
            da.Fill(dt);
            da.Dispose();
            return dt;
        }

        public static DataTable Rak()
        {
            DataTable dt = new DataTable();
            FbDataAdapter da = new FbDataAdapter("select * from rak", ConnectDB.Connetc());
            //da.SelectCommand.Parameters.Add("@id", 123);
            da.Fill(dt);
            da.Dispose();
            return dt;
        }

        public static DataTable Barang()
        {
            DataTable dt = new DataTable();
            var query = "select "+
                "b.id_barang, " +
                "kode, " +
                "b.nama_barang as Nama_Barang, " +
                "p.nama_penerbit as Penerbit, " +
                "k.nama as Kategori, " +
                "rak.nama as Rak, " +
                "b.stock, b.harga, b.isbn, b.penulis, b.diskon, b.status, b.barcode, b.keterangan " +
                "from barang as b " +
                "INNER JOIN kategori as k ON b.id_kategori = k.id " +
                "INNER JOIN penerbit as p ON b.id_penerbit = p.id " +
                "INNER JOIN rak ON b.id_rak = rak.id;";
            FbDataAdapter da = new FbDataAdapter(query, ConnectDB.Connetc());
            da.Fill(dt);
            da.Dispose();
            return dt;
        }

        public static DataTable Pelanggan()
        {
            DataTable dt = new DataTable();
            using (var con = ConnectDB.Connetc())
            {
                var query = "select pel.id as id_pelanggan, pel.nama as nama_pelanggan, pel.alamat, pel.no_hp, pel.email, total_hutang.tot_hutang as total_hutang, total_hutang.tenggat_bayar, pel.keterangan from pelanggan as pel left join (select pi.id_pelanggan, sum(pi.total - jumbayar.terbayar) as tot_hutang, min(pi.tgl_tenggat_bayar) as tenggat_bayar from piutang as pi inner join (select bp.id_piutang, sum(bp.pembayaran) as terbayar from bayar_piutang as bp group by bp.id_piutang) as jumbayar on jumbayar.id_piutang=pi.id where pi.sudah_lunas='belum' group by pi.id_pelanggan) as total_hutang on pel.id=total_hutang.id_pelanggan";
                using (var cmd = new FbCommand(query, con))
                {
                    FbDataAdapter da = new FbDataAdapter(cmd);
                    da.Fill(dt);
                    da.Dispose();
                }
            }
            return dt;
        }
        internal static DataTable Supplier()
        {
            DataTable dt = new DataTable();
            var query = "select su.id as id_supplier, su.nama as nama_supplier, " +
                "su.alamat, su.no_hp, su.email, hu.id as id_hutang, " +
                "hu.total as total_hutang, hu.tgl_tenggat_bayar as tenggat_bayar, " +
                "hu.sudah_lunas as lunas " +
                "from hutang as hu " +
                "left join " +
                "(select bh.id_hutang, sum(bh.pembayaran) as pembayaran " +
                "from bayar_hutang as bh " +
                "group by bh.id_hutang) as bhut " +
                "on bhut.id_hutang=hu.id " +
                "left join pembelian as pem " +
                "on hu.id_pembelian=pem.id_pembelian " +
                "right join supplier as su " +
                "on hu.id_supplier=su.id";
            FbDataAdapter da = new FbDataAdapter(query, ConnectDB.Connetc());
            da.Fill(dt);
            da.Dispose();
            return dt;
        }

        internal static DataTable Kas()
        {
            DataTable dt = new DataTable();
            FbDataAdapter da = new FbDataAdapter("select * from kas_master", ConnectDB.Connetc());
            da.Fill(dt);
            da.Dispose();
            return dt;
        }
        public static DataTable Kasir()
        {
            DataTable dt = new DataTable();
            FbDataAdapter da = new FbDataAdapter("select * from kasir", ConnectDB.Connetc());
            da.Fill(dt);
            da.Dispose();
            return dt;
        }
    }
}
