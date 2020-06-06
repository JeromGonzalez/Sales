namespace Sales.Common.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Articulos
    {
        [Key]
        public int CodProducto { get; set; }

        [Required]
        public string Descripcion { get; set; }

        public Decimal PVP { get; set; }

        public bool Activo { get; set; }

        public DateTime FechaAlata { get; set; }

    }
}
