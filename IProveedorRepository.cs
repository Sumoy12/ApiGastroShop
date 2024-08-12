using ProyectoDAW.Models;

namespace ProyectoDAW.Repository
{
    public interface IProveedorRepository
    {
        Task<List<Proveedor>> ObtenerTodos();
        Task<int> Crear(Proveedor Proveedor);
        Task<Proveedor> ModificarProveedor(Proveedor Proveedor);
        Task<int> CambiarEstadoProveedor(int ProveedorId);
        Task<Proveedor?> ObtenerPorId(int ProveedorId);
    }
}
