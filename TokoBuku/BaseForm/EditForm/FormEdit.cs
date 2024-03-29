﻿using System;
using System.Windows.Forms;
using TokoBuku.BaseForm.Master.Input;
using TokoBuku.BaseForm.TipeData.DataBase;

namespace TokoBuku.BaseForm.EditForm
{
    public static class FormEdit
    {
        public static FormEditKategori Kategori(string OrigialName)
        {
            FormEditKategori form = new FormEditKategori(OrigialName);
            form.FormTitle = "Data Kategori";
            form.FormTitle = "DATA KATEGORI";
            return form;
        }
        public static FormEditKategori Penerbit(string OrigialName)
        {
            FormEditKategori form = new FormEditKategori(OrigialName);
            form.FormTitle = "Data Penerbit";
            form.FormTitle = "DATA PENERBIT";
            return form;
        }
        public static FormEditKategori Rak(string OrigialName, string keterangan)
        {
            FormEditKategori form = new FormEditKategori(OrigialName);
            form.FormTitle = "Data Rak";
            form.FormTitle = "DATA RAK";
            return form;
        }
        public static FormInputDataBarang Barang(DataGridViewRow row)
        {
            FormInputDataBarang form = new FormInputDataBarang();
            form.NamaForm = "EDIT DATA BARANG";
            form.TitleForm = "EDIT DATA BARANG";
            form.NamaBarang = row.Cells["nama_barang"].Value.ToString();
            form.KodeBarang = row.Cells["kode"].Value.ToString();
            form.Penerbit = row.Cells["penerbit"].Value.ToString();
            form.Kategori = row.Cells["kategori"].Value.ToString();
            form.Rak = row.Cells["rak"].Value.ToString();
            form.Stock = Convert.ToInt32(row.Cells["stock"].Value.ToString());
            form.Harga = Convert.ToDouble(row.Cells["harga_jual"].Value.ToString());
            form.HargaBeli = Convert.ToDouble(row.Cells["harga_beli"].Value.ToString());
            form.ISBN = row.Cells["isbn"].Value.ToString();
            form.Penulis = row.Cells["penulis"].Value.ToString();
            form.Diskon = Convert.ToDouble(row.Cells["diskon"].Value.ToString());
            form.Status = row.Cells["status"].Value.ToString();
            form.BarCode = row.Cells["barcode"].Value.ToString();
            form.Keterangan = row.Cells["keterangan"].Value.ToString();
            form.SetToEditForm();
            return form;
        }

        public static FormEditDataPelangganSupplier Pelanggan(TPelanggan pelanggan)
        {
            FormEditDataPelangganSupplier form = new FormEditDataPelangganSupplier();
            form.SetToEditFormPelanggan(pelanggan);
            return form;
        }
        public static FormEditDataPelangganSupplier Supplier(TSupplier supplier)
        {
            FormEditDataPelangganSupplier form = new FormEditDataPelangganSupplier();
            form.SetToEditFormSupplier(supplier);
            return form;
        }

        public static FormEditKas Kas(TKas kas)
        {
            FormEditKas form = new FormEditKas(kas: kas);
            return form;
        }

        public static FormEditDataKasir Kasir(DataGridViewRow row)
        {
            FormEditDataKasir form = new FormEditDataKasir();
            form.SetParameterEdit(row);
            return form;
        }
    }
}
