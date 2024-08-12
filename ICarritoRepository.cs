using ProyectoDAW.Models;

namespace ProyectoDAW.Repository
{
    public interface ICarritoRepository
    {
        Task<IEnumerable<Carrito>> GetCarritos();
        Task<Carrito> GetCarritoId(int id);
        Task<Carrito> AddCarrito(Carrito carrito);
        Task<Carrito> UpdateCarrito(Carrito carrito);
        Task<Carrito> DeleteCarrito(int id);
    }
}
