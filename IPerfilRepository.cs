using ProyectoDAW.Models;

namespace ProyectoDAW.Repository
{
    public interface IPerfilRepository
    {
        Task<IEnumerable<Perfil>> GetPerfiles();
        Task<Perfil> GetPerfilById(int id);
        Task<Perfil> AddPerfil(Perfil perfil);
        Task<Perfil> UpdatePerfil(Perfil perfil);
        Task<Perfil> DeletePerfil(int id);
    }
}
