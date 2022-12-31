using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using TokoBuku.BaseForm.TipeData.DataBase;

namespace TokoBuku.DbUtility.Transactions
{
    internal static class Pembelian
    {
        public static int SavePembelian(TPembelian pembelian)
        {
            int ids = 0;
            using (var con = ConnectDB.Connetc())
            {
                var query = "insert into pembelian " +
                    "(id_supplier, tanggal_beli, no_nota, " +
                    "total, status_pembelian, id_kas) " +
                    "values (@id_supplier, @tanggal_beli, @no_nota, " +
                    "@total, @status_pembayaran, @id_kas) " +
                    "returning id_pembelian;";
                using (var cmd = new FbCommand(query, con))
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.Add("@id_supplier", pembelian.IdSupplier);
                    cmd.Parameters.Add("@tanggal_beli", pembelian.TanggalBeli);
                    cmd.Parameters.Add("@no_nota", pembelian.NoNota);
                    cmd.Parameters.Add("@total", pembelian.Total);
                    cmd.Parameters.Add("@status_pembayaran", pembelian.JenisPembayaran);
                    cmd.Parameters.Add("id_kas", pembelian.IdKas);
                    ids = (int)cmd.ExecuteScalar();
                    cmd.Dispose();
                }
            }
            return ids;
        }

        /*public static int SavePembelian_kredit(int id_supplier, DateTime tanggal_beli,
            string no_nota, double total, string id_kas, string status_pembayaran = "CASH")
        {
            int ids = 0;
            using (var con = ConnectDB.Connetc())
            {
                var query = "insert into pembelian " +
                    "(id_supplier, tanggal_beli, no_nota, " +
                    "total, status_pembelian, id_kas) " +
                    "values (@id_supplier, @tanggal_beli, @no_nota, " +
                    "@total, @status_pembayaran, @id_kas) " +
                    "returning id_pembelian;";
                using (var cmd = new FbCommand(query, con))
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.Add("@id_supplier", id_supplier);
                    cmd.Parameters.Add("@tanggal_beli", tanggal_beli);
                    cmd.Parameters.Add("@no_nota", no_nota);
                    cmd.Parameters.Add("@total", total);
                    cmd.Parameters.Add("@status_pembayaran", status_pembayaran);
                    cmd.Parameters.Add("id_kas", id_kas);
                    ids = (int)cmd.ExecuteScalar();
                    cmd.Dispose();
                }
            }
            return ids;
        }*/

        internal static int SavePembelianCash(TPembelian pembelian, List<TDetailPembelian> detailPembelian)
        {
            var ids = SavePembelian(pembelian);
            try
            {
                using (var con = ConnectDB.Connetc())
                {
                    var query = "insert into detail_pembelian " +
                        "(id_barang, id_pembelian, jumlah, harga, satuan) " +
                        "values (@id_barang, @id_pembelian, @jumlah, @harga, @satuan);";
                    foreach (TDetailPembelian detail in detailPembelian)
                    {
                        using (var cmd = new FbCommand(query, con))
                        {
                            cmd.CommandType = System.Data.CommandType.Text;
                            cmd.Parameters.Add("@id_barang", detail.IdBarang);
                            cmd.Parameters.Add("@id_pembelian", ids);
                            cmd.Parameters.Add("@jumlah", detail.Jumlah);
                            cmd.Parameters.Add("@harga", detail.Harga);
                            cmd.Parameters.Add("@satuan", detail.Satuan);
                            cmd.ExecuteNonQuery();
                            cmd.Dispose();
                        }
                        UpdateStockBarangPembelian(detail);
                        UpdateHargaBeli(detail);
                    }
                }
            }
            catch (Exception ex)
            {
                DeletePembelian(ids);
                throw ex;
            }
            return ids;
        }

        internal static void SavePembelianKredit(TPembelian pembelian, List<TDetailPembelian> detailPembelian, THutang hutang, TBayarHutang bayarHutang)
        {
            try
            {
                var ids = SavePembelianCash(pembelian, detailPembelian);
                hutang.IdPembelian = ids;
                try
                {
                    var id_hutang = SaveHutang(hutang: hutang, bayarHutang: bayarHutang);
                }
                catch (Exception ex)
                {
                    DeletePembelian(ids);
                    throw ex;
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        internal static int SaveHutang(THutang hutang, TBayarHutang bayarHutang)
        {
            using (var con = ConnectDB.Connetc())
            {
                int id = 0;
                var query = "insert into hutang " +
                    "(id_pembelian, id_supplier, tgl_tenggat_bayar, total, sudah_lunas) " +
                    "values (@id_pembelian, @id_supplier, @tgl_tenggat_bayar, @total, @sudah_lunas) " +
                    "returning id;";
                using (var cmd = new FbCommand(query, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("@id_pembelian", hutang.IdPembelian);
                    cmd.Parameters.Add("@id_supplier", hutang.IdSupplier);
                    cmd.Parameters.Add("@tgl_tenggat_bayar", hutang.TanggalTenggatBayar);
                    cmd.Parameters.Add("@total", hutang.Total);
                    cmd.Parameters.Add("@sudah_lunas", hutang.Lunas);
                    id = (int)cmd.ExecuteScalar();
                    cmd.Dispose();
                }
                // Simpan pembayaran awal
                try
                {
                    bayarHutang.IdHutang = id;
                    BayarHutang(bayarHutang);
                }
                catch (Exception)
                {

                    throw;
                }
                return id;
            }
        }

        internal static void BayarHutang(TBayarHutang bayarHutang)
        {
            using (var con = ConnectDB.Connetc())
            {
                var query = "insert into bayar_hutang " +
                    "(id_hutang, pembayaran, tgl_bayar, id_kas, is_dp) " +
                    "values (@id_hutang, @pembayaran, @tgl_bayar, @id_kas, @is_dp);";
                using (var cmd = new FbCommand(query, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("@id_hutang", bayarHutang.IdHutang);
                    cmd.Parameters.Add("@pembayaran", bayarHutang.Pembayaran);
                    cmd.Parameters.Add("@tgl_bayar", bayarHutang.TglBayar);
                    cmd.Parameters.Add("@id_kas", bayarHutang.IdKas);
                    cmd.Parameters.Add("@is_dp", bayarHutang.isDP);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
            }
        }

        internal static void DeleteHutang(int id_hutang)
        {
            using (var con = ConnectDB.Connetc())
            {
                var query = "delete hutang where id=@id";
                using (var cmd = new FbCommand(query, con))
                {
                    cmd.Parameters.Add("@id", id_hutang);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
            }
        }

        internal static void DeletePembelian(int id)
        {
            try
            {
                DeleteDetailPembelian(id);
                using (var con = ConnectDB.Connetc())
                {
                    var query = "delete pembelian where id=@id";
                    using (var cmd = new FbCommand(query, con))
                    {
                        cmd.Parameters.Add("@id", id);
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                    }
                }
            }
            catch (Exception ex) { throw ex; } 
        }
        private static void DeleteDetailPembelian(int id_pembelian)
        {
            using (var con = ConnectDB.Connetc())
            {
                var query = "delete detail_pembelian where id_pembelian=@id_pembelian;";
                using (var cmd = new FbCommand(query, con))
                {
                    cmd.Parameters.Add("@id_pembelian", id_pembelian);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
            }
        }


        internal static void UpdateStockBarangPembelian(TDetailPembelian detail)
        {
            int id_barang = detail.IdBarang;
            var stock_db = GetStockBarang(id_barang);
            double stock_minus = detail.Jumlah;
            /*if (row["satuan"].ToString().ToLower() == "packs")
            {
                stock_minus *= 10;
            }*/
            var stock_ = stock_db + stock_minus;
            using (var con = ConnectDB.Connetc())
            {
                var query = "update barang " +
                    "set stock=@stock_ " +
                    "where id_barang=@id_barang;";
                using (var cmd = new FbCommand(query, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("@stock_", stock_);
                    cmd.Parameters.Add("@id_barang", id_barang);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
            }
        }

        internal static double GetStockBarang(int id_barang)
        {
            double stock = 0;
            using (var con = ConnectDB.Connetc())
            {
                var query = "select stock from barang where id_barang=@id_barang";
                using (var cmd = new FbCommand(query, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("@id_barang", id_barang);
                    var x = cmd.ExecuteReader();
                    if (x.FieldCount > 0)
                    {
                        while (x.Read())
                        {
                            string dump_ = x[x.FieldCount - 1].ToString();
                            stock = Convert.ToDouble(dump_);
                        }
                    }
                    cmd.Dispose();
                }
            }
            return stock;
        }

        internal static void UpdateHargaBeli(TDetailPembelian detail)
        {
            int id_barang = detail.IdBarang;
            double harga_beli = detail.Harga;
            using (var con = ConnectDB.Connetc())
            {
                var query = "update barang " +
                    "set harga_beli=@harga_beli " +
                    "where id_barang=@id_barang;";
                using (var cmd = new FbCommand(query, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("@harga_beli", harga_beli);
                    cmd.Parameters.Add("@id_barang", id_barang);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
            }
        }

        internal static DataTable HistoriPembelian()
        {
            DataTable data = new DataTable();
            using (var con = ConnectDB.Connetc())
            {
                var query = "select pem.id_pembelian, sup.nama as suplier, " +
                    "pem.tanggal_beli, pem.no_nota as nota, pem.total, " +
                    "pem.status_pembelian as pembayaran, k.nama as kas " +
                    "from pembelian as pem " +
                    "left join supplier as sup " +
                    "on pem.id_supplier=sup.id " +
                    "left join kas_master as k " +
                    "on pem.id_kas=k.id " +
                    "order by pem.tanggal_beli desc;";
                using (var cmd = new FbCommand(query, con))
                {
                    cmd.CommandType = CommandType.Text;
                    FbDataAdapter da = new FbDataAdapter(cmd);
                    da.Fill(data);
                    da.Dispose();
                }
            }
            return data;
        }

        internal static void UbahHargaJualBarang(int id_barang, double hargaBaru)
        {
            using (var con = ConnectDB.Connetc())
            {
                var query = "update barang " +
                    "set harga_jual=@harga_baru " +
                    "where id_barang=@id_barang;";
                using (var cmd = new FbCommand(query, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("@harga_baru", hargaBaru);
                    cmd.Parameters.Add("@id_barang", id_barang);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
            }
        }
    }
}
