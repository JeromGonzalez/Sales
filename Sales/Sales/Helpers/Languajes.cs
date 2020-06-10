namespace Sales.Helpers
{
    using Xamarin.Forms;
    using Interfaces;
    using Resources;

    public static class Languages
    {
        static Languages()
        {
            var ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
            Resource.Culture = ci;
            DependencyService.Get<ILocalize>().SetLocale(ci);
        }

        public static string Aceptar
        {
            get { return Resource.Aceptar; }
        }
        public static string Error
        {
            get { return Resource.Error; }
        }
        public static string ActivarConexion
        {
            get { return Resource.ActivarConexion; }
        }
        public static string SinConexion
        {
            get { return Resource.SinConexion; }
        }
        public static string Articulos
        {
            get { return Resource.Articulos; }
        }

        public static string BuscarArticulos
        {
            get { return Resource.BuscarArticulos ; }
        }

        public static string AñadirArticulo
        {
            get { return Resource.AñadirArticulo ; }
        }

        public static string Descripcion
        {
            get { return Resource.Descripcion  ; }
        }

        public static string DescripcionPlaceHolder
        {
            get { return Resource.DescripcionPlaceHolder ; }
        }

        public static string Precio
        {
            get { return Resource.Precio ; }
        }

        public static string PrecioPlaceHolder
        {
            get { return Resource.PrecioPlaceHolder ; }
        }

        public static string Guardar
        {
            get { return Resource.Guardar; }
        }

        public static string Comentario
        {
            get { return Resource.Comentario ; }
        }

        public static string CambiarImagen
        {
            get { return Resource.CambiarImagen; }
        }

        public static string ErrorDescripcion
        {
            get { return Resource.ErrorDescripcion ; }
        }

        public static string ErrorPrecio
        {
            get { return Resource.ErrorPrecio ; }
        }
    }
}
