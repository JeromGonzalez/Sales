
namespace Sales.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using Common.Models;
    using GalaSoft.MvvmLight.Command;
    using Helpers;
    using Services;
    using Xamarin.Forms;

    public class ArticulosVieModel : BaseViewModel
    {
        #region Atributos
        private ApiService apiService;
        private bool isRefreshing;
        #endregion

        #region Propiedades
        private ObservableCollection<Articulos> priArticulo;
        public ObservableCollection<Articulos> Articulo
        {
            get { return this.priArticulo; }
            set { this.SetValue(ref this.priArticulo, value); }
        }

        public bool IsRefreshing
        {
            get { return this.isRefreshing ; }
            set { this.SetValue(ref this.isRefreshing, value); }
        }
        #endregion

        #region Constructores
        public ArticulosVieModel()
        {
            instance = this;
            this.apiService = new ApiService();
            this.CargarArticulos();
        }
        #endregion

        #region Metodos
        private async void CargarArticulos()
        {
            this.IsRefreshing = true;
            var conexionInternet = await this.apiService.CheckConnection();
            if (!conexionInternet.Correcto)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(Languages.Error, conexionInternet.Texto, Languages.Aceptar);
                return;
            }

            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controlador = Application.Current.Resources["UrlArticulosControlador"].ToString();
            var response = await apiService.GetList<Articulos>(url, prefix, controlador);
            if (!response.Correcto)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(Languages.Error, response.Texto, Languages.Aceptar);
                return;
            }

            var list = (List<Articulos>)response.Resultado;
            this.Articulo = new ObservableCollection<Articulos>(list);
            this.IsRefreshing = false;
        }
        #endregion

        #region Singleton
        private static ArticulosVieModel instance;

        public static ArticulosVieModel GetInstance()
        {
            if (instance == null)
            {
                return new ArticulosVieModel();
            }

            return instance;
        }
        #endregion

        #region Comandos
        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(CargarArticulos);
            }
        }
        #endregion
    }
}
 