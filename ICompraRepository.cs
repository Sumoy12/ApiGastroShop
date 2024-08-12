using ProyectoDAW.Models;

namespace ProyectoDAW.Repository
{
    public interface ICompraRepository
    {
        Task<IEnumerable<Compra>> GetCompras();
        Task<Compra> GetCompraId(int id);
        Task<Compra> AddCompra(Compra compra);
        Task<Compra> UpdateCompra(Compra compra);
        Task<Compra> DeleteCompra(int id);
    }
}
