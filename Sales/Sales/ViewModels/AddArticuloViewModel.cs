namespace Sales.ViewModels
{
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Helpers;
    using Common.Models;
    using Services;
    using Xamarin.Forms;
    using System.Linq;

    public class AddArticuloViewModel : BaseViewModel
    {
        #region Atributos
        private bool isRunning;
        private bool botonEnabled;
        private ApiService apiService;
        #endregion 

        #region Propiedades
        public string  Descripcion { get; set; }

        public string Precio { get; set; }

        public string Comentario { get; set; }

        public bool IsRunning
        {
            get { return this.isRunning; }
            set { this.SetValue(ref this.isRunning , value); }
        }

        public bool BtnEnabled
        {
            get { return this.botonEnabled; }
            set { this.SetValue(ref this.botonEnabled, value); }
        }
        #endregion 

        #region Constructores
        public AddArticuloViewModel()
        {
            this.BtnEnabled = true;
            this.isRunning = false;
            this.apiService = new ApiService();
        }
        #endregion 

        #region Comandos
        public ICommand GuardarCommand
        {
            get
            {
                return new RelayCommand(Guardar);
            }
        }

        private async void Guardar()
        {
            this.IsRunning = true;
            this.BtnEnabled = false;

            if (string.IsNullOrEmpty(this.Descripcion))
            {
                this.IsRunning = false;
                this.BtnEnabled = true;
                await Application.Current.MainPage.DisplayAlert(Languages.Error , Languages.ErrorDescripcion , Languages.Aceptar);
                return;
            }
            if (string.IsNullOrEmpty(this.Precio))
            {
                this.IsRunning = false;
                this.BtnEnabled = true;
                await Application.Current.MainPage.DisplayAlert(Languages.Error, Languages.ErrorPrecio , Languages.Aceptar);
                return;
            }

            var precio = decimal.Parse(this.Precio);
            if (precio <= 0)
            {
                this.IsRunning = false;
                this.BtnEnabled = true;
                await Application.Current.MainPage.DisplayAlert(Languages.Error, Languages.ErrorPrecio, Languages.Aceptar);
                return;
            }

            var conexionInternet = await this.apiService.CheckConnection();
            if (!conexionInternet.Correcto)
            {
                this.IsRunning = false;
                this.BtnEnabled = true;
                await Application.Current.MainPage.DisplayAlert(Languages.Error, conexionInternet.Texto, Languages.Aceptar);
                return;
            }

            var articulo = new Articulos 
            {
                Descripcion = this.Descripcion ,
                PVP = precio,
                Comentario = this.Comentario ,
            };

            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controlador = Application.Current.Resources["UrlArticulosControlador"].ToString();
            var response = await apiService.Post(url, prefix, controlador, articulo);
            if (!response.Correcto)
            {
                this.IsRunning = false;
                this.BtnEnabled = true;
                await Application.Current.MainPage.DisplayAlert(Languages.Error, response.Texto, Languages.Aceptar);
                return;
            }

            var nuevoArticulo = (Articulos)response.Resultado;
            var viewModelArt = ArticulosVieModel.GetInstance();
            viewModelArt.Articulo.Add(nuevoArticulo);

            //this.IsRunning = false;
            //this.BtnEnabled = true;
            await Application.Current.MainPage.Navigation.PopAsync();
        }
        #endregion
    }
}
