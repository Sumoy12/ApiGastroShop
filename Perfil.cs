using System.ComponentModel.DataAnnotations;

namespace ProyectoDAW.Models
{
    public class Perfil
    {
        [Key]
        public int PerfilId { get; set; }
        public string NombrePerfil { get; set;}
        public string ApellidosPerfil { get; set;}

        [Required]
        [EmailAddress]
        [MaxLength(254)]
        public string EmailPerfil { get; set; }
        public string UsuarioPerfil { get; set; }
        public string PasswordPerfil { get; set; }
        public bool EstadoPerfil { get; set; }
    }
}
