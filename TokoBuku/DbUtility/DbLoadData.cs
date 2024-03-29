﻿using FirebirdSql.Data.FirebirdClient;
using System.Data;

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
            var query = "select " +
                "b.id_barang, " +
                "kode, " +
                "b.nama_barang as Nama_Barang, " +
                "p.nama_penerbit as Penerbit, " +
                "k.nama as Kategori, " +
                "rak.nama as Rak, " +
                "b.stock, b.harga as harga_jual, b.beli as harga_beli, b.isbn, " +
                "b.penulis, b.diskon, b.status, b.barcode, b.keterangan " +
                "from barang as b " +
                "INNER JOIN kategori as k ON b.id_kategori = k.id " +
                "INNER JOIN penerbit as p ON b.id_penerbit = p.id " +
                "INNER JOIN rak ON b.id_rak = rak.id " +
                "order by b.kode asc;";
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
                var query = "select pel.id as id_pelanggan, " +
                    "pel.nama, pel.alamat, pel.no_hp, pel.email, " +
                    "coalesce(hut_pel.total_hutang, 0) as total_hutang, " +
                    "coalesce(hut_pel.piutang_sudah_dibayar, 0) as piutang_sudah_dibayar," +
                    "(coalesce(hut_pel.total_hutang, 0) - coalesce(hut_pel.piutang_sudah_dibayar, 0)) as piutang_belum_dibayar, " +
                    "hut_pel.tenggat_bayar as tenggat_bayar_terdekat, " +
                    "pel.keterangan " +
                    "from pelanggan as pel " +
                    "left join " +
                    "(select pi.id_pelanggan, min(pi.tgl_tenggat_bayar) as tenggat_bayar, " +
                    "sum(pi.total) as total_hutang, " +
                    "sum(pembayaran_piutang.piutang_terbayar) as piutang_sudah_dibayar " +
                    "from piutang as pi " +
                    "left join " +
                    "(select bp.id_piutang, sum(bp.pembayaran) as piutang_terbayar " +
                    "from bayar_piutang as bp " +
                    "where bp.is_dp='bukan' " +
                    "group by bp.id_piutang) as pembayaran_piutang " +
                    "on pi.id=pembayaran_piutang.id_piutang " +
                    "where pi.sudah_lunas='Belum' " +
                    "group by pi.id_pelanggan) as hut_pel " +
                    "on pel.id=hut_pel.id_pelanggan " +
                    "where not pel.nama='UMUM';";
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
