using FirebirdSql.Data.FirebirdClient;
using System.Data;
using TokoBuku.BaseForm.TipeData.DataBase;

namespace TokoBuku.DbUtility.Master
{
    internal static class Supplier
    {
        internal static DataTable GetDataSupplier()
        {
            DataTable dt = new DataTable();
            var query = "select supplier.id as id_supplier, supplier.nama as nama_supplier, supplier.alamat as alamat, supplier.no_hp as no_hp, supplier.email as email, hutsup.nominal_hutang as total_hutang, hutsup.tgl_tenggat_bayar as tenggat_bayar, supplier.keterangan from supplier left join (select hut.id_supplier, sum(hut.nominal_hutang) as nominal_hutang, min(hut.tgl_tenggat_bayar) as tgl_tenggat_bayar from (select hu.id_supplier, (hu.total - bhu.pembayaran) as nominal_hutang, hu.tgl_tenggat_bayar from hutang as hu left join (select bh.id_hutang, sum(bh.pembayaran) as pembayaran from bayar_hutang as bh group by bh.id_hutang) as bhu on hu.id=bhu.id_hutang) as hut group by hut.id_supplier) as hutsup on supplier.id=hutsup.id_supplier";
            FbDataAdapter da = new FbDataAdapter(query, ConnectDB.Connetc());
            da.Fill(dt);
            da.Dispose();
            return dt;
        }

        internal static void EditSupplier(TSupplier supplier)
        {
            using (var con = ConnectDB.Connetc())
            {
                var query = "update supplier " +
                    "set nama=@nama, alamat=@alamat, no_hp=@hp, email=@email, keterangan=@keterangan " +
                    "where id=@id;";
                using (var cmd = new FbCommand(query, con))
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.Add("@nama", supplier.Nama);
                    cmd.Parameters.Add("@alamat", supplier.Alamat);
                    cmd.Parameters.Add("@hp", supplier.NoHp);
                    cmd.Parameters.Add("@email", supplier.Email);
                    cmd.Parameters.Add("@keterangan", supplier.Keterangan);
                    cmd.Parameters.Add("@id", supplier.Id);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }

            }
        }

        internal static int SaveSupplier(TSupplier supplier)
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
                    cmd.Parameters.Add("@nama", supplier.Nama);
                    cmd.Parameters.Add("@alamat", supplier.Alamat);
                    cmd.Parameters.Add("@no_hp", supplier.NoHp);
                    cmd.Parameters.Add("@email", supplier.Email);
                    cmd.Parameters.Add("@keterangan", supplier.Keterangan);
                    cmd.Parameters.Add("@status", supplier.Status);
                    ids = (int)cmd.ExecuteScalar();
                    cmd.Dispose();
                    return ids;
                }
            }
        }
    }
}
