using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace TokoBuku.DbUtility.Master
{
    internal static class Barang
    {
        internal static DataTable GetDataBarang()
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

        internal static void UpdateDataBarang(int idBarang, string namaBarang, string kode, int idKategori, int idPenerbit, int idRak, double stock, double harga, double hargaBeli, string isbn, string penulis, double diskon, string barcode, string keterangan)
        {
            using (var con = ConnectDB.Connetc())
            {
                var strSql = "UPDATE barang " +
                    "SET NAMA_BARANG=@nama, " +
                    "Kode=@kode, " +
                    "ID_KATEGORI=@kategori, " +
                    "id_penerbit=@penerbit, " +
                    "id_rak=@rak, " +
                    "stock=@Stock, " +
                    "harga=@Harga, " +
                    "beli=@hargaBeli, " +
                    "isbn=@Isbn, " +
                    "penulis=@Penulis, " +
                    "diskon=@Diskon, " +
                    "barcode=@Barcode, " +
                    "keterangan=@Keterangan " +
                    "where id_barang=@Id";
                using (var cmd = new FbCommand(strSql, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("@nama", namaBarang);
                    cmd.Parameters.Add("@kode", kode);
                    cmd.Parameters.Add("@kategori", idKategori);
                    cmd.Parameters.Add("@penerbit", idPenerbit);
                    cmd.Parameters.Add("@rak", idRak);
                    cmd.Parameters.Add("@Stock", stock);
                    cmd.Parameters.Add("@Harga", harga);
                    cmd.Parameters.Add("@hargaBeli", hargaBeli);
                    cmd.Parameters.Add("@Isbn", isbn);
                    cmd.Parameters.Add("@Penulis", penulis);
                    cmd.Parameters.Add("@Diskon", diskon);
                    cmd.Parameters.Add("@Barcode", barcode);
                    cmd.Parameters.Add("@Keterangan", keterangan);
                    cmd.Parameters.Add("@Id", idBarang);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
            }
        }
    }
}
