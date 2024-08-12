using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoDAW.Models
{
    public class Facturacion
    {
        [Key]
        public int FacturaId { get; set; }

        [ForeignKey("UsuarioId")]
        public Usuario Usuario { get; set; }
        public int UsuarioId { get; set; }

        [ForeignKey("CompraId")]
        public Compra Compra { get; set; }
        public int CompraId { get; set; }

        [ForeignKey("PagoId")]
        public Pago Pago { get; set; }
        public int PagoId { get; set; }

        [Precision(18, 2)]
        public decimal IvaFactura { get; set; }
        [Precision(18, 2)]
        public decimal TotalFactura { get; set; }

        public bool estadoFactura { get; set; }
    }
}
