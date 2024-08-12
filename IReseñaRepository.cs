using ProyectoDAW.Models;

namespace ProyectoDAW.Repository
{
    public interface IReseñaRepository
    {
        Task<List<Resena>> GetResenas();
        Task<Resena> GetResenaById(int id);
        Task<Resena> AddResena(Resena resena);
        Task<Resena> UpdateResena(Resena resena);
        Task<Resena> DeleteResena(int id);
    }
}
