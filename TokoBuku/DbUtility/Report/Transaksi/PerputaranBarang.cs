using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace TokoBuku.DbUtility.Report.Transaksi
{
    internal static class PerputaranBarang
    {
        internal static DataTable GetStockSebelumnya(DateTime dateMulai)
        {
            DataTable data = new DataTable();
            using (var con = ConnectDB.Connetc())
            {
                var query = "select dpe.id_barang, dpe.nama_barang, sum(dpe.jumlah) as jumlah from penjualan as p left join (select dpen.id_penjualan, b.id_barang, b.nama_barang, dpen.jumlah, dpen.harga_jual from detail_penjualan as dpen left join barang as b on dpen.id_barang=b.id_barang) as dpe on p.id=dpe.id_penjualan where p.tanggal < @dateMulai group by dpe.id_barang, dpe.nama_barang;";
                using (var cmd = new FbCommand(query, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = query;
                    cmd.Parameters.Add("@dateMulai", dateMulai);
                    FbDataAdapter da = new FbDataAdapter(cmd);
                    da.Fill(data);
                    da.Dispose();
                }
            }
            return data;
        }
        internal static DataTable GetStockPeriode(DateTime dateMulai, DateTime dateSampai)
        {
            DataTable data = new DataTable();
            using (var con = ConnectDB.Connetc())
            {
                var query = "select dpe.id_barang, dpe.nama_barang, sum(dpe.jumlah) as jumlah from penjualan as p left join (select dpen.id_penjualan, b.id_barang, b.nama_barang, dpen.jumlah, dpen.harga_jual from detail_penjualan as dpen left join barang as b on dpen.id_barang=b.id_barang) as dpe on p.id=dpe.id_penjualan where p.tanggal between @dateMulai and @dateSampai group by dpe.id_barang, dpe.nama_barang;";
                using (var cmd = new FbCommand(query, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = query;
                    cmd.Parameters.Add("@dateMulai", dateMulai);
                    cmd.Parameters.Add("@dateSampai", dateSampai);
                    FbDataAdapter da = new FbDataAdapter(cmd);
                    da.Fill(data);
                    da.Dispose();
                }
            }
            return data;
        }

        internal static DataTable GetPerputaranBarang(DateTime dateMulai, DateTime dateSampai)
        {
            DataTable data= new DataTable();
            using (var con = ConnectDB.Connetc())
            {
                var query = "select  period.id_barang, period.nama_barang, period.jumlah, ba.stock from (select dpe.id_barang, dpe.nama_barang, sum(dpe.jumlah) as jumlah from penjualan as p left join (select dpen.id_penjualan, b.id_barang, b.nama_barang, dpen.jumlah from detail_penjualan as dpen left join barang as b on dpen.id_barang=b.id_barang) as dpe on p.id=dpe.id_penjualan where p.tanggal between @dateMulai and @dateSampai group by dpe.id_barang, dpe.nama_barang) as period left join barang as ba on period.id_barang=ba.id_barang;";
                using (var cmd = new FbCommand(query, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("@dateMulai", dateMulai);
                    cmd.Parameters.Add("@dateSampai", dateSampai);
                    var da = new FbDataAdapter(cmd);
                    da.Fill(data);
                }
            }
            data.Columns.Add("BELI", typeof(double), "IIF((jumlah - stock) < 0, 0, jumlah - stock)");
            return data;
        }

       /* internal static DataTable GetPerputaranBarang(DateTime dateMulai, DateTime dateSampai)
        {
            DataTable periode = GetStockPeriode(dateMulai, dateSampai);
            DataTable prev = GetStockSebelumnya(dateMulai);
            DataTable data = new DataTable();
            data.Columns.Add("id_barang", typeof(int));
            data.Columns.Add("nama_barang", typeof(string));
            data.Columns.Add("jumlah", typeof(int));
            data.Columns.Add("JUMLAH_SEBELUM", typeof(int));
            prev = (prev.Rows.Count < 1) ? onPrevNull(periode) : prev;
            var results = from table1 in periode.AsEnumerable()
                          join table2 in prev.AsEnumerable()
                          on table1.Field<int>("id_barang") equals table2.Field<int>("id_barang")
                          select new
                          {
                              id_barang = table1.Field<int>("id_barang"),
                              nama_barang = table1.Field<string>("nama_barang"),
                              jumlah = table1.Field<int>("jumlah"),
                              JUMLAH_SEBELUM = table2.Field<int>("JUMLAH_SEBELUM")
                          };
            foreach (var item in results)
            {
                DataRow dr = data.NewRow();
                dr["nama_barang"] = item.nama_barang;
                dr["jumlah"] = item.jumlah;
                dr["JUMLAH_SEBELUM"] = item.JUMLAH_SEBELUM;
                data.Rows.Add(dr);
            }
            return data;
        }
*/
        private static DataTable onPrevNull(DataTable data)
        {
            DataTable dt = data.Copy();
            dt.Columns.Remove("jumlah");
            dt.Columns.Add("JUMLAH_SEBELUM", typeof(int));
            foreach (DataRow row in dt.Rows)
            {
                row["JUMLAH_SEBELUM"] = 0;
            }
            return dt;
        }

        private static DataTable ToDataTable<T>(this List<T> items)
        {
            var tb = new DataTable(typeof(T).Name);

            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var prop in props)
            {
                tb.Columns.Add(prop.Name, prop.PropertyType);
            }

            foreach (var item in items)
            {
                var values = new object[props.Length];
                for (var i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null);
                }

                tb.Rows.Add(values);
            }

            return tb;
        }
    }
}
