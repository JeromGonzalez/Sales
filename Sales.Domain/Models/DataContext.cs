namespace Sales.Domain.Models
{
    using System.Data.Entity;

    public class DataContext : DbContext
    {
        public DataContext() : base("DefaultConnection")
        {

        }

        public System.Data.Entity.DbSet<Sales.Common.Models.Articulos> Articulos { get; set; }
    }
}
 