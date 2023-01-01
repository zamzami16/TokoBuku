using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TokoBuku.BaseForm.TipeData.DataBase;

namespace TokoBuku.DbUtility.Transactions
{
    internal static class UpdateKas
    {
        internal static void TambahKasPenjualan(TPenjualan penjualan)
        {
            if (penjualan.UangPembayaran > 0)
            {
                double newSaldo = 0;
                if (penjualan.StatusPembayaran == TJenisPembayaran.Cash)
                {
                    newSaldo = GetKasSekarang((int)penjualan.IdKas) + penjualan.Total;
                }
                else if (penjualan.StatusPembayaran == TJenisPembayaran.Kredit)
                {
                    newSaldo = GetKasSekarang((int)penjualan.IdKas) + penjualan.UangPembayaran;
                }
                UpdateNominalKas((int)penjualan.IdKas, newSaldo);
            }
        }

        internal static void KurangKasPembelianCash(TPembelian pembelian)
        {
            if (pembelian.JenisPembayaran == TJenisPembayaran.Cash)
            {
                double newSaldo = GetKasSekarang(pembelian.IdKas) - pembelian.Total;
                UpdateNominalKas(pembelian.IdKas, newSaldo);
            }
        }

        internal static void KurangKasPembelianKredit(TBayarHutang bayarHutang)
        {

            double newSaldo = GetKasSekarang(bayarHutang.IdKas) - bayarHutang.Pembayaran;
            UpdateNominalKas(bayarHutang.IdKas, newSaldo);
        }

        internal static void UpdateNominalKas(int idKas, double newSaldo)
        {
            using (var con = ConnectDB.Connetc())
            {
                using (var cmd = new FbCommand())
                {
                    cmd.CommandText = "update kas_master set saldo=@NewSaldo where id=@idKas;";
                    cmd.Connection= con;
                    cmd.Parameters.Add("@NewSaldo", newSaldo);
                    cmd.Parameters.Add("@idKas", idKas);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
            }
        }

        internal static double GetKasSekarang(int idKas)
        {
            double kasSekarang = 0;
            using (var con = ConnectDB.Connetc())
            {
                var _query = "select saldo from kas_master where id=@idKas";
                using (var cmd = new FbCommand(_query, con))
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.Add("@idKas", idKas);
                    var _x = cmd.ExecuteReader();
                    if (_x.FieldCount > 0 && _x.HasRows)
                    {
                        while (_x.Read())
                        {
                            kasSekarang = Convert.ToDouble(_x[_x.FieldCount - 1].ToString());
                        }
                    }
                }
            }
            return kasSekarang;
        }
    }
}
