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
            var query = "select sup.id, sup.nama, sup.alamat, sup.no_hp, sup.email, " +
                "coalesce(d_hutang.total_hutang, 0) as total_hutang, " +
                "coalesce(d_hutang.sudah_dibayar, 0) as sudah_bayar, " +
                "coalesce(d_hutang.belum_bayar, 0) as belum_bayar, " +
                "sup.keterangan " +
                "from supplier as sup " +
                "left join " +
                "(select hu.id_supplier, " +
                "sum(coalesce(hu.total, 0)) as total_hutang, " +
                "sum(coalesce(sudah_bayar.tot_sudah_pembayaran, 0)) as sudah_dibayar, " +
                "(sum(coalesce(hu.total, 0)) - sum(coalesce(sudah_bayar.tot_sudah_pembayaran, 0))) as belum_bayar " +
                "from hutang as hu " +
                "left join " +
                "(select bh.id_hutang, sum(bh.pembayaran) as tot_sudah_pembayaran " +
                "from bayar_hutang as bh " +
                "where not bh.is_dp='ya' " +
                "group by bh.id_hutang) as sudah_bayar " +
                "on hu.id=sudah_bayar.id_hutang " +
                "where hu.sudah_lunas='Belum' " +
                "group by hu.id_supplier) as d_hutang " +
                "on sup.id=d_hutang.id_supplier " +
                "order by sup.nama asc;";
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
