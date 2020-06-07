
namespace Sales.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using Common.Models;
    using GalaSoft.MvvmLight.Command;
    using Services;
    using Xamarin.Forms;

    public class ArticulosVieModel : BaseViewModel
    {
        private ApiService apiService;

        private bool isRefreshing;

        public bool IsRefreshing
        {
            get { return this.isRefreshing ; }
            set { this.SetValue(ref this.isRefreshing, value); }
        }

        private ObservableCollection<Articulos> priArticulo;
        public ObservableCollection<Articulos> Articulo 
        {
            get { return this.priArticulo;  }
            set { this.SetValue(ref this.priArticulo, value); }
        }

        public ArticulosVieModel()
        {
            this.apiService = new ApiService();
            this.CargarArticulos();
        }

        private async void CargarArticulos()
        {
            this.IsRefreshing = true;
            var conexionInternet = await this.apiService.CheckConnection();
            if (!conexionInternet.Correcto )
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert("Error", conexionInternet.Texto, "Accept");
                return;
            }

            var url = Application.Current.Resources["UrlAPI"].ToString();
            var response = await apiService.GetList<Articulos>(url, "/api", "/Articulos");
            if (!response.Correcto)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert("Error", response.Texto, "Accept");
                return;
            }

            var list = (List<Articulos>)response.Resultado;
            this.Articulo = new ObservableCollection<Articulos>(list);
            this.IsRefreshing = false;
        }

        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(CargarArticulos);
            }
        }
    }
}
 