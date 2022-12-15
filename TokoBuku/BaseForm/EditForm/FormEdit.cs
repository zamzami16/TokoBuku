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
            /*form.NamaForm = "EDIT DATA BARANG";
            form.TitleForm = "EDIT DATA BARANG";
            form.NamaBarang = barang.NamaBarang;
            form.Kategori = barang.Kategori;
            form.Penerbit = barang.Penerbit;
            form.Rak = barang.Rak;
            form.Stock = barang.Stock;
            form.Harga = barang.Harga;
            form.ISBN = barang.ISBN;
            form.Penulis = barang.Penulis;
            form.Diskon = barang.Diskon;
            form.Status = barang.Status;
            form.BarCode = barang.BarCode;*/

            // "b.id_barang, b.nama_barang, p.nama_penerbit, k.nama, rak.nama, b.stock, " +
            //"b.harga, b.isbn, b.penulis, b.diskon, b.status, b.barcode, b.keterangan "

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
    }
}
