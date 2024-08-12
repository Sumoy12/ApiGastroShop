using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace ProyectoDAW.Models
{
    public class Producto
    {
        [Key]
        public int ProductoId { get; set; }

        public int CategoriaId { get; set; }

        [ForeignKey("CategoriaId")]
        public Categoria categoria { get; set; }
        public string ProductoNombre { get; set; }
        public string ProductoDescripcion { get; set; }

        [Precision(18, 2)]
        public decimal ProductoPrecio { get; set; }
        public string  RutaImg { get; set; }
        public bool ProductoEstado { get; set; }

       



    }
}
