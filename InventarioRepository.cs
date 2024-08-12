using Microsoft.EntityFrameworkCore;
using ProyectoDAW.Models;

namespace ProyectoDAW.Repository
{
    public class InventarioRepository : IInventarioRepository
    {
        private readonly RestauranteDbContext _restauranteDbContext;

        public InventarioRepository(RestauranteDbContext restauranteDbContext)
        {
            _restauranteDbContext = restauranteDbContext;
        }

        public async Task<int> Crear(Inventario Inventario)
        {
            _restauranteDbContext.inventario.Add(Inventario);
            await _restauranteDbContext.SaveChangesAsync();

            return Inventario.InventarioId;
        }

        /*public async Task<int> CambiarEstadoInventario(int InventarioId)
        {
            Inventario Inventario = await context.Inventario.FindAsync(InventarioId);

            // Alternar el estado entre 1 y 0
            Inventario.Estado = Inventario.Estado == 0 ? 1 : 0;

            await context.SaveChangesAsync();
            return Inventario.Id;
        }
       */

        public async Task<int> CambiarEstadoInventario(int InventarioId)
        {
            Inventario inventario = await _restauranteDbContext.inventario.FindAsync(InventarioId);
            if (inventario == null)
            {
                // Maneja el caso donde no se encuentra el inventario
                throw new Exception("Inventario no encontrado");
            }

            // Alternar el estado booleano
            inventario.Estado = !inventario.Estado;

            await _restauranteDbContext.SaveChangesAsync();

            return inventario.InventarioId;
        }


        public async Task<Inventario?> ObtenerPorId(int InventarioId)
        {
            return await _restauranteDbContext.inventario.FindAsync(InventarioId);

        }

        public async Task<Inventario> ModificarInventario(Inventario Inventario)
        {
            //obtener el objeto de la BD
            Inventario InventarioMod = await _restauranteDbContext.inventario.FindAsync(Inventario.InventarioId);
            //cambiar los valores del objeto consultado
            InventarioMod.ProductoId = Inventario.ProductoId;
            InventarioMod.ProveedorId = Inventario.ProveedorId;
            InventarioMod.Cantidad = Inventario.Cantidad;
            InventarioMod.Precio = Inventario.Precio;
            InventarioMod.Tipo = Inventario.Tipo;
            //Guardar los cambios
            await _restauranteDbContext.SaveChangesAsync();
            return InventarioMod;
        }

        public async Task<List<Inventario>> ObtenerTodos()
        {    
            List<Inventario> lista = new List<Inventario>();
            List<Inventario> listaResult = new List<Inventario>();
            Producto producto = null;
            Proveedor proveedor = null;
                
               
          

            lista  = await _restauranteDbContext.inventario.ToListAsync();
            foreach (Inventario inv in lista)
            {
                producto = new Producto();
                producto = await _restauranteDbContext.productos.FindAsync(inv.ProductoId);
                inv.Producto = producto;

                proveedor = new Proveedor();
                proveedor = await _restauranteDbContext.proveedor.FindAsync(inv.ProveedorId);

                inv.Proveedor = proveedor;

                listaResult.Add(inv);


            }

            return listaResult;

        }


    }
}
