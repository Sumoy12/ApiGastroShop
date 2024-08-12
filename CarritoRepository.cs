using Microsoft.EntityFrameworkCore;
using ProyectoDAW.Models;

namespace ProyectoDAW.Repository
{
    public class CarritoRepository : ICarritoRepository
    {
        private readonly RestauranteDbContext _restauranteDbContext;

        public CarritoRepository(RestauranteDbContext restauranteDbContext)
        {
            _restauranteDbContext = restauranteDbContext;
        }

        //CRUD
        //CREATE CARRITO
        public async Task<Carrito> AddCarrito(Carrito carrito)
        {
            try
            {
                await _restauranteDbContext.Set<Carrito>().AddAsync(carrito);
                await _restauranteDbContext.SaveChangesAsync();
                return carrito; // Return the added carrito
            }
            catch (Exception ex)
            {
                // Manejar la excepción (por ejemplo, loguearla)
                throw new Exception("Error al agregar el carrito", ex);
            }
        }

        // obtener el carrito por id
        public async Task<Carrito> GetCarritoId(int id)
        {
            try
            {
                var carrito = await _restauranteDbContext.Set<Carrito>().FindAsync(id);
                if (carrito == null)
                {
                    throw new Exception("El carrito no existe");
                }
                return carrito;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el carrito", ex);
            }
        }
        // retorna todos los carritos
        public async Task<IEnumerable<Carrito>> GetCarritos()
        {
            try
            {
                return await _restauranteDbContext.Set<Carrito>().ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los carritos", ex);
            }
        }

        

       
        //editar carrito por id
        public async Task<Carrito> UpdateCarrito(Carrito carrito)
        {
            try
            {
                var carritoExistente = await  _restauranteDbContext.Set<Carrito>().FindAsync(carrito.CarritoId);
                if (carritoExistente == null)
                {
                    throw new Exception("El carrito no existe");
                }

                //actualizamos los atributos del carrito
                carritoExistente.producto = carrito.producto;
                carritoExistente.precioCarrito = carrito.precioCarrito;
                carritoExistente.CarritoId = carrito.CarritoId;
                carritoExistente.estadoCarritoEnum = carrito.estadoCarritoEnum;

                _restauranteDbContext.Set<Carrito>().Update(carritoExistente);
                await _restauranteDbContext.SaveChangesAsync();
                return carritoExistente; // Return el updated carrito
            }catch
            (Exception ex)
            {
                throw new Exception("Error al actualizar el carrito", ex);
            }
        }

        //eliminar CARRITO
        public async Task<Carrito> DeleteCarrito(int id)
        {
            try
            {
                var carrito = await _restauranteDbContext.Set<Carrito>().FindAsync(id);
                if (carrito == null)
                {
                    throw new Exception("El carrito no existe");
                }
                _restauranteDbContext.Set<Carrito>().Remove(carrito);
                await _restauranteDbContext.SaveChangesAsync();
                return carrito; // Return el deleted carrito
            }
            catch (Exception ex)
            {

                throw new Exception("Error al eliminar el carrito", ex);
            }
        }
    }
}
