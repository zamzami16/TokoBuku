﻿using System;

namespace TokoBuku.BaseForm.TipeData.DataBase
{
    internal class THutang
    {
        public int Id { get; set; }
        public int IdPembelian { get; set; }
        public int IdSupplier { get; set; }
        public DateTime TanggalTenggatBayar { get; set; }
        public double Total { get; set; }
        public TLunas Lunas { get; set; }
        public THutang() { this.Lunas = TLunas.Belum; }

        public THutang(int id, int idPembelian, int idSupplier, DateTime tanggalTenggatBayar, double total, TLunas lunas)
        {
            Id = id;
            IdPembelian = idPembelian;
            IdSupplier = idSupplier;
            TanggalTenggatBayar = tanggalTenggatBayar;
            Total = total;
            Lunas = lunas;
        }
    }
}
