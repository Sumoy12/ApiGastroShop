using ProyectoDAW.Models;

namespace ProyectoDAW.Repository
{
    public interface IProductoRepository
    {
        Task<IEnumerable<Producto>> GetProductos();
        Task<Producto> GetProductoById(int id);
        Task<Producto> AddProducto(Producto producto);
        Task<Producto> UpdateProducto(Producto producto);
        Task<Producto> DeleteProducto(int id);
    }
}
