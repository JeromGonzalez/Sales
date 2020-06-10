namespace Sales.Backend.Models
{
    
    using System.Web;
    using Common.Models;

    public class ArticuloVista : Articulos
    {
        public HttpPostedFileBase ArchivoImagen { get; set; }
    }
}