using ProyectoDAW.Models;

namespace ProyectoDAW.Repository
{
    public interface IFacturacionRepository
    {
        Task<IEnumerable<Facturacion>> GetFacturaciones();
        Task<Facturacion> GetFacturacionById(int id);
        Task<Facturacion> AddFacturacion(Facturacion facturacion);
        Task<Facturacion> UpdateFacturacion(Facturacion facturacion);
        Task<Facturacion> DeleteFacturacion(int id);
    }
}
