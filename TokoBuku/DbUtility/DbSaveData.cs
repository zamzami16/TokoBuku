using FirebirdSql.Data.FirebirdClient;
using System.Data;

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

        static public int Barang(int inIdKategori, int inIdPenerbit, int inIdRak, string inKode,
            string inNama, int inStock, double inHargaBeli, double inHarga, string inIsbn, string inPenulis,
            double inDiskon, string inStatus, string inBarCode, string inKeterngan)
        {
            using (var con = ConnectDB.Connetc())
            {
                int ids;
                if (inKode == "--OTOMATIS--")
                {
                    inKode = TokoBuku.DbUtility.Etc.GenerateKodeBarang();
                }
                var strSql = "INSERT INTO BARANG (ID_KATEGORI, ID_PENERBIT, ID_RAK, KODE, NAMA_BARANG, STOCK, HARGA, beli," +
                    "ISBN, PENULIS, DISKON, STATUS, BARCODE, KETERANGAN) " +
                    "VALUES (@kategori, @penerbit, @rak, @kode, @nama, @stock, @harga, @beli, @isbn, @penulis, @diskon, @status, " +
                    "@barcode, @keterangan) returning ID_BARANG;";
                using (var cmd = new FbCommand(strSql, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("@kategori", inIdKategori);
                    cmd.Parameters.Add("@penerbit", inIdPenerbit);
                    cmd.Parameters.Add("@rak", inIdRak);
                    cmd.Parameters.Add("@kode", inKode);
                    cmd.Parameters.Add("@nama", inNama);
                    cmd.Parameters.Add("@stock", inStock);
                    cmd.Parameters.Add("@harga", inHarga);
                    cmd.Parameters.Add("@beli", inHargaBeli);
                    cmd.Parameters.Add("@isbn", inIsbn);
                    cmd.Parameters.Add("@penulis", inPenulis);
                    cmd.Parameters.Add("@diskon", inDiskon);
                    cmd.Parameters.Add("@status", "AKTIF");
                    cmd.Parameters.Add("@barcode", inBarCode);
                    cmd.Parameters.Add("@keterangan", inKeterngan);
                    ids = (int)cmd.ExecuteScalar();
                    cmd.Dispose();
                }
                return ids;
            }
        }
    }
}
