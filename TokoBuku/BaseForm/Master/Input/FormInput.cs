using TokoBuku.BaseForm.Master;

namespace TokoBuku.BaseForm.Master.Input
{
    public static class FormInput
    {
        public static FormDataRakKasKategoriPenerbitMaster Kategori()
        {
            FormDataRakKasKategoriPenerbitMaster form = new FormDataRakKasKategoriPenerbitMaster();
            form.type_of = form.type_of_kategori;
            return form;
        }

        public static FormDataRakKasKategoriPenerbitMaster Rak()
        {
            FormDataRakKasKategoriPenerbitMaster form = new FormDataRakKasKategoriPenerbitMaster();
            form.type_of = form.type_of_rak;
            return form;
        }
        public static FormDataRakKasKategoriPenerbitMaster Kas()
        {
            FormDataRakKasKategoriPenerbitMaster form = new FormDataRakKasKategoriPenerbitMaster();
            form.type_of = form.type_of_kas;
            return form;
        }
        public static FormDataRakKasKategoriPenerbitMaster Penerbit()
        {
            FormDataRakKasKategoriPenerbitMaster form = new FormDataRakKasKategoriPenerbitMaster();
            form.type_of = form.type_of_penerbit;
            return form;
        }

        /// <summary>
        /// Create New Form Input Pelanggan
        /// </summary>
        /// <returns>
        /// Form: input data pelanggan
        /// </returns>
        public static FormDataPelangganSupplier Pelanggan()
        {
            FormDataPelangganSupplier form = new FormDataPelangganSupplier();
            form.type_of = form.type_of_pelanggan;
            return form;
        }

        /// <summary>
        /// Create New Form Input Pelanggan
        /// </summary>
        /// <returns>
        /// Form: input data pelanggan
        /// </returns>
        public static FormDataPelangganSupplier Supplier()
        {
            FormDataPelangganSupplier form = new FormDataPelangganSupplier();
            form.type_of = form.type_of_supplier;
            return form;
        }
    }
}
