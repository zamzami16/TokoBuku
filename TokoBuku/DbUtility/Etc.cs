using System;
using FirebirdSql.Data.FirebirdClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Drawing;
using System.Runtime.Remoting.Messaging;

namespace TokoBuku.DbUtility
{
    public static class Etc
    {
        
        /*public static string GetLastKodeBarang()
        {
            using (var con = ConnectDB.Connetc())
            {
                string _kode_ = null;
                var query = "SELECT kode FROM barang order by kode desc;";
                using (var cmd = new FbCommand(query, con))
                {
                    var kode_ = cmd.ExecuteReader();
                    while (kode_.Read())
                    {
                        _kode_ = kode_.GetString(0);
                    }
                    return _kode_;
                }
            }
        }*/
        public static DataTable GenerateBarCode()
        {
            using (var con = ConnectDB.Connetc())
            {
                var dt = new DataTable();
                var query = "SELECT barcode FROM barang;";
                var da = new FbDataAdapter(query, ConnectDB.Connetc());
                da.Fill(dt);
                return dt;
            }
        }
        public static int GetLastKodeBarang()
        {
            string num = "";
            int num_ = 0;
            var dt = new DataTable();
            using (var con = ConnectDB.Connetc())
            {
                /*var query = "select substring(kode from 2 for 8), count(*) from barang group by substring(kode from 2 for 8);";*/
                //var query = "select count(*) from (select substring(kode from 2 for 8) as kode from barang) kode;";
                var query = "select substring(kode from 2 for 8) as kode from barang order by kode asc;";

                using (var cmd = new FbCommand(query, con))
                {
                    var x = cmd.ExecuteReader();
                    if (x.FieldCount > 0)
                    {
                        while (x.Read())
                        {
                            num = x[x.FieldCount - 1].ToString();
                            num_ = Convert.ToInt32(num);
                        }
                    }
                    else
                    {
                        num_ = 0;
                    }
                }
            }
            return num_;
        }

        public static string GenerateKodeBarang()
        {
            string dump = "B";
            var last_kode_db = GetLastKodeBarang();
            if (last_kode_db.ToString().Length >= 1)
            {
                var len_ = last_kode_db.ToString().Length;
                for (int i = len_; i < 8; i++)
                {
                    if (i == 7)
                    {
                        dump = string.Concat(dump, (last_kode_db + 1).ToString());
                    }
                    else
                    {
                        dump = string.Concat(dump, "0");
                    }
                }
            }
            return dump;
        }

        
    }
}
