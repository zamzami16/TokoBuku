using System;
using FirebirdSql.Data.FirebirdClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Drawing;

namespace TokoBuku.DbUtility
{
    public static class Etc
    {
        
        public static string GetLastKodeBarang()
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
        }
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
    }
}
