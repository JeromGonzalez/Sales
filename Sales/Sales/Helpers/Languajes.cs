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
    }
}
