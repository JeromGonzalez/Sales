using Sales.Common.Models;

namespace Sales.ViewModels
{
    public class MainViewModels
    {
        public ArticulosVieModel Articulo { get; set; }

        public MainViewModels()
        {
            this.Articulo = new ArticulosVieModel();
        }
    }
}
 