namespace Sales.ViewModels
{
    using System.Diagnostics;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Views;
    using Xamarin.Forms;
    
    public class MainViewModels
    {
        public ArticulosVieModel Articulo { get; set; }

        public AddArticuloViewModel AddArticulo { get; set; }

        public MainViewModels()
        {
            this.Articulo = new ArticulosVieModel();
        }

        public ICommand AddArticuloCommand {
            get
            {
                return new RelayCommand(GotoAddArticulo);
            }
        }

        private async void GotoAddArticulo()
        {
            this.AddArticulo = new AddArticuloViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new AddArticulo());
        }
    }
}
 