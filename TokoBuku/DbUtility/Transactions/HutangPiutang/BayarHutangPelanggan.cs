using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace TokoBuku.DbUtility.Transactions.HutangPiutang
{
    internal static class BayarHutangPelanggan
    {
        internal static DataTable DataHutangPelanggan(int id_pelanggan)
        {
            DataTable table= new DataTable();
            using (var con = ConnectDB.Connetc())
            {
                var query = "select pi.id as id_piutang, pen.nama_kasir, " +
                    "pi.tgl_tenggat_bayar as tenggat_bayar, " +
                    "pi.pembayaran_awal, pen.total, " +
                    "pen.keterangan as keterangan_pembelian, " +
                    "pi.id_penjualan as id_penjualan, " +
                    "pen.kode_transaksi " +
                    "from piutang as pi " +
                    "left join " +
                    "(select penj.id as id_penjualan, k.nama as nama_kasir, " +
                    "penj.total, penj.keterangan, penj.kode_transaksi " +
                    "from penjualan as penj " +
                    "inner join kasir as k " +
                    "on penj.id_kasir=k.id " +
                    "where penj.status_pembayaran='KREDIT') as pen " +
                    "on pi.id_penjualan=pen.id_penjualan " +
                    "where pi.id_pelanggan=7;";
                using (var cmd = new FbCommand(query, con))
                {
                    cmd.CommandType= CommandType.Text;
                    cmd.Parameters.Add("@id_pelanggan", id_pelanggan);
                    FbDataAdapter da = new FbDataAdapter(cmd);
                    da.Fill(table);
                    da.Dispose();
                }
            }
            return table;
        }

        internal static void BayarHutang(int id_pelanggan, int id_penjualan, DateTime tanggal_bayar, double pembayaran_awal, int posisi = -1, int sudah_lunas = 1)
        {
            using (var con = ConnectDB.Connetc())
            {
                var query = "insert into piutang " +
                    "(id_pelanggan, id_penjualan, tgl_bayar, posisi, pembayaran_awal, sudah_lunas) " +
                    "values (@id_pelanggan, @id_penjualan, @tgl_bayar, @pembayaran_awal, @posisi, @sudah_lunas);";
                using (var cmd = new FbCommand(query, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("@id_pelanggan", id_pelanggan);
                    cmd.Parameters.Add("@id_penjualan", id_penjualan);
                    cmd.Parameters.Add("@tgl_bayar", tanggal_bayar);
                    cmd.Parameters.Add("@posisi", posisi);
                    cmd.Parameters.Add("@pembayaran_awal", pembayaran_awal);
                    cmd.Parameters.Add("@sudah_lunas", sudah_lunas);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
            }
        }

    }
}
