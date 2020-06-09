namespace Sales.Common.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Articulos
    {
        [Key]
        public int CodProducto { get; set; }

        [Required]
        [StringLength(100)]
        public string Descripcion { get; set; }

        [DataType(DataType.MultilineText)]
        public string Comentario { get; set; }

        [Display(Name ="Image")]
        public string RutaImagen { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public Decimal PVP { get; set; }

        public bool Activo { get; set; }

        [Display(Name = "Fecha de alta")]
        [DataType(DataType.Date)]
        public DateTime FechaAlata { get; set; }

        public override string ToString()
        {
            return this.Descripcion;
        }

    }
}
