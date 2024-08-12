using System.ComponentModel.DataAnnotations;

namespace ProyectoDAW.Models
{
    public class Usuario
    {
        [Key]
        public int UsuarioId { get; set; }
        [MaxLength(10)]
        public string UsuarioCedula { get; set; }
        public string UsuarioNombre { get; set; }
        public string UsuarioApellido { get; set; }
        [Required]
        [EmailAddress]
        [MaxLength(254)]
        public string UsuarioEmail { get; set; }
        public string UsuarioUP { get; set; }
        public string UsuarioPassword { get; set; }
        public bool UsuarioEstado { get; set; }
    }
}
