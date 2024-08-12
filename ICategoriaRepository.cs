using ProyectoDAW.Models;

namespace ProyectoDAW.Repository
{
    public interface ICategoriaRepository
    {
        Task<IEnumerable<Categoria>> GetCategorias();
        Task<Categoria> GetCategoriaId(int id);
        Task<Categoria> AddCategoria(Categoria categoria);
        Task<Categoria> UpdateCategoria(Categoria categoria);
        Task<Categoria> DeleteCategoria(int id);
    }
}
