using Microsoft.EntityFrameworkCore;
using ProyectoDAW.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoDAW.Models
{
    public class Carrito
    {
        [Key]
        public int CarritoId { get; set; }

        [ForeignKey("ProductoId")]
        public Producto producto { get; set; }

        [Precision(18, 2)]
        public decimal precioCarrito { get; set; }
        public int cantidadCarrito { get; set; }
        public EstadoCarritoEnum estadoCarritoEnum { get; set; }
    }
}
