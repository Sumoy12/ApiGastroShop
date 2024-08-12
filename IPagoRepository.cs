using ProyectoDAW.Models;

namespace ProyectoDAW.Repository
{
    public interface IPagoRepository
    {
        Task<IEnumerable<Pago>> GetPagos();
        Task<Pago> GetPagoById(int id);
        Task<Pago> AddPago(Pago pago);
        Task<Pago> UpdatePago(Pago pago);
        Task<Pago> DeletePago(int id);
    }
}
