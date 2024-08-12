using ProyectoDAW.Models;

namespace ProyectoDAW.Repository
{
    public interface IInventarioRepository
    {
        Task<List<Inventario>> ObtenerTodos();
        Task<int> Crear(Inventario Inventario);
        Task<Inventario> ModificarInventario(Inventario Inventario);
        Task<int> CambiarEstadoInventario(int InventarioId);
        Task<Inventario?> ObtenerPorId(int InventarioId);
    }
}
