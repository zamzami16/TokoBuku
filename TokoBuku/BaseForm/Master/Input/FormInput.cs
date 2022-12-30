namespace TokoBuku.BaseForm.Master.Input
{
    public static class FormInput
    {
        public static FormInputDataRakKategoriPenerbitMaster Kategori()
        {
            FormInputDataRakKategoriPenerbitMaster form = new FormInputDataRakKategoriPenerbitMaster();
            form.type_of = form.type_of_kategori;
            return form;
        }

        public static FormInputDataRakKategoriPenerbitMaster Rak()
        {
            FormInputDataRakKategoriPenerbitMaster form = new FormInputDataRakKategoriPenerbitMaster();
            form.type_of = form.type_of_rak;
            return form;
        }
        public static FormInputDataKas Kas()
        {
            FormInputDataKas form = new FormInputDataKas();
            return form;
        }
        public static FormInputDataRakKategoriPenerbitMaster Penerbit()
        {
            FormInputDataRakKategoriPenerbitMaster form = new FormInputDataRakKategoriPenerbitMaster();
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
        public static FormInputDataKasir Kasir()
        {
            FormInputDataKasir form = new FormInputDataKasir();
            return form;
        }
    }
}
