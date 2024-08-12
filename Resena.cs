using ProyectoDAW.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoDAW.Models
{
    public class Resena
    {
        [Key]
        public int ResenaId { get; set; }

        [ForeignKey("UsuarioId")]
        public Usuario Usuario { get; set; }  
        public int UsuarioId { get; set; }
        public ResenaEnum resenaEnum { get; set; }
        public String Comentario { get; set; }
    }
}
