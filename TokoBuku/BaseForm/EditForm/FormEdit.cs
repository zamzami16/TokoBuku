using System;
using System.Windows.Forms;
using TokoBuku.BaseForm.Master.Input;
using TokoBuku.DbUtility.Transactions;

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
        public static FormEditKategori Rak(string OrigialName)
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
            form.NamaBarang = row.Cells[1].Value.ToString();
            form.Penerbit = row.Cells[2].Value.ToString();
            form.Kategori = row.Cells[3].Value.ToString();
            form.Rak = row.Cells[4].Value.ToString();
            form.Stock = Convert.ToInt32(row.Cells[5].Value.ToString());
            form.Harga = Convert.ToDouble(row.Cells[6].Value.ToString());
            form.ISBN = row.Cells[7].Value.ToString();
            form.Penulis = row.Cells[8].Value.ToString();
            form.Diskon = Convert.ToDouble(row.Cells[9].Value.ToString());
            form.Status = row.Cells[10].Value.ToString();
            form.BarCode = row.Cells[11].Value.ToString();
            form.SetToEditForm();
            return form;
        }

        public static FormEditDataPelangganSupplier Pelanggan(DataGridViewRow row)
        {
            FormEditDataPelangganSupplier form = new FormEditDataPelangganSupplier();
            form.SetToEditForm(row);
            return form;
        }
        public static FormEditDataPelangganSupplier Supplier(DataGridViewRow row)
        {
            FormEditDataPelangganSupplier form = new FormEditDataPelangganSupplier();
            form.SetToEditForm(row);
            return form;
        }

        public static FormEditKategori Kas(string OrigialName, string keterangan)
        {
            FormEditKategori form = new FormEditKategori(OrigialName, keterangan);
            form.FormTitle = "Edit Data Kas";
            form.FormTitle = "EDIT DATA KAS";
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
