using FirebirdSql.Data.FirebirdClient;
using System.Data;
using TokoBuku.BaseForm.TipeData.DataBase;

namespace TokoBuku.DbUtility
{
    /// <summary>
    /// Save Data to DB
    /// </summary>
    static public class DbSaveData
    {
        static public int Pelanggan(string nama, string alamat, string email, string no_hp, string keterangan, string status = "AKTIF")
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

        static public int Kategori(string nama, string keterangan, string status = "AKTIF")
        {
            using (var con = ConnectDB.Connetc())
            {
                int ids;
                var query = "insert into kategori (nama, keterangan, status) " +
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

        static public int Penerbit(string nama, string keterangan, string status = "AKTIF")
        {
            using (var con = ConnectDB.Connetc())
            {
                int ids;
                var query = "insert into penerbit (nama_penerbit, keterangan, status) " +
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

        static public int Barang(DbBarang dbBarang)
        {
            using (var con = ConnectDB.Connetc())
            {
                int ids;
                if (dbBarang.Kode == "--OTOMATIS--")
                {
                    dbBarang.Kode = TokoBuku.DbUtility.Etc.GenerateKodeBarang();
                }
                var strSql = "INSERT INTO BARANG (ID_KATEGORI, ID_PENERBIT, ID_RAK, KODE, NAMA_BARANG, STOCK, HARGA, beli," +
                    "ISBN, PENULIS, DISKON, STATUS, BARCODE, KETERANGAN) " +
                    "VALUES (@kategori, @penerbit, @rak, @kode, @nama, @stock, @harga, @beli, @isbn, @penulis, @diskon, @status, " +
                    "@barcode, @keterangan) returning ID_BARANG;";
                using (var cmd = new FbCommand(strSql, con))
                {/// TODO: cek variabel dio data base
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("@kategori", dbBarang.Kategori);
                    cmd.Parameters.Add("@penerbit", dbBarang.Penerbit);
                    cmd.Parameters.Add("@rak", dbBarang.IdRak);
                    cmd.Parameters.Add("@kode", dbBarang.Kode);
                    cmd.Parameters.Add("@nama", dbBarang.NamaBarang);
                    cmd.Parameters.Add("@stock", dbBarang.Stock);
                    cmd.Parameters.Add("@harga", dbBarang.HargaJual);
                    cmd.Parameters.Add("@beli", dbBarang.HargaBeli);
                    cmd.Parameters.Add("@isbn", dbBarang.ISBN);
                    cmd.Parameters.Add("@penulis", dbBarang.Penulis);
                    cmd.Parameters.Add("@diskon", dbBarang.Diskon);
                    cmd.Parameters.Add("@status", dbBarang.Status);
                    cmd.Parameters.Add("@barcode", dbBarang.BarCode);
                    cmd.Parameters.Add("@keterangan", dbBarang.Keterangan);
                    ids = (int)cmd.ExecuteScalar();
                    cmd.Dispose();
                }
                return ids;
            }
        }
    }
}
