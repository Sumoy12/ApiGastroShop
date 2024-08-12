using Microsoft.EntityFrameworkCore;
using ProyectoDAW.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoDAW.Models
{
    public class Pago
    {
        [Key]
        public int PagoId { get; set; }
        public MetodoPagoEnum metodoPago { get; set; }
        [Precision(18, 2)]
        public decimal MontoPago { get; set; }
    }
}
