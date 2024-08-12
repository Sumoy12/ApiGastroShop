using Microsoft.EntityFrameworkCore;
using ProyectoDAW.Models;

namespace ProyectoDAW.Repository
{
    public class ProveedorRepository : IProveedorRepository
    {
        private readonly RestauranteDbContext _restauranteDbContext;

        public ProveedorRepository(RestauranteDbContext restauranteDbContext)
        {
            _restauranteDbContext = restauranteDbContext;
        }

        public async Task<int> Crear(Proveedor Proveedor)
        {

            //context.Proveedor.Add(Proveedor);
            _restauranteDbContext.proveedor.Add(Proveedor);
            await _restauranteDbContext.SaveChangesAsync();

            return Proveedor.ProveedorId;
        }

        /*public async Task<int> CambiarEstadoProveedor(int ProveedorId)
        {
            Proveedor Proveedor = await context.Proveedor.FindAsync(ProveedorId);

            // Alternar el estado entre 1 y 0
            Proveedor.Estado = Proveedor.Estado == 0 ? 1 : 0;

            await context.SaveChangesAsync();
            return Proveedor.Id;
        }
       */

        public async Task<int> CambiarEstadoProveedor(int ProveedorId)
        {
            Proveedor Proveedor = await _restauranteDbContext.proveedor.FindAsync(ProveedorId);
            if (Proveedor == null)
            {
                // Maneja el caso donde no se encuentra el Proveedor
                throw new Exception("Proveedor no encontrado");
            }

            // Alternar el estado booleano
            Proveedor.Estado = !Proveedor.Estado;

            await _restauranteDbContext.SaveChangesAsync();

            return Proveedor.ProveedorId;
        }


        public async Task<Proveedor?> ObtenerPorId(int ProveedorId)
        {
            return await _restauranteDbContext.proveedor.FindAsync(ProveedorId);

        }

        public async Task<Proveedor> ModificarProveedor(Proveedor Proveedor)
        {
            //obtener el objeto de la BD
            Proveedor ProveedorMod = await _restauranteDbContext.proveedor.FindAsync(Proveedor.ProveedorId);
            //cambiar los valores del objeto consultado
            ProveedorMod.Nombre = Proveedor.Nombre;
            ProveedorMod.Email = Proveedor.Email;
            ProveedorMod.Telefono = Proveedor.Telefono;
            //Guardar los cambios
            await _restauranteDbContext.SaveChangesAsync();
            return ProveedorMod;
        }

        public async Task<List<Proveedor>> ObtenerTodos()
        {
            return await _restauranteDbContext.proveedor.ToListAsync();
        }


    }
}
