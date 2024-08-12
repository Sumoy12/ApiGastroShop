using Microsoft.EntityFrameworkCore;
using ProyectoDAW.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace ProyectoDAW.Repository
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly RestauranteDbContext _restauranteDbContext;

        public ProductoRepository(RestauranteDbContext restauranteDbContext)
        {
            _restauranteDbContext = restauranteDbContext;
        }

        public async Task<IEnumerable<Producto>> GetProductos()
        {
            try
            {
                return await _restauranteDbContext.Set<Producto>()
                    .Where(p => p.ProductoEstado == true) // filtro para mostrar solo los activos
                    .ToListAsync();
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception("Error al obtener los productos de la base de datos", dbEx);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los productos", ex);
            }
        }

        public async Task<Producto> GetProductoById(int id)
        {
            try
            {
                var producto = await _restauranteDbContext.Set<Producto>().FindAsync(id);
                if (producto == null || producto.ProductoEstado == false)
                {
                    throw new InvalidOperationException("El producto no existe o está inactivo");
                }
                return producto;
            }
            catch (InvalidOperationException invOpEx)
            {
                throw new Exception("Error al obtener el producto: " + invOpEx.Message, invOpEx);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el producto", ex);
            }
        }

        public async Task<Producto> AddProducto(Producto producto)
        {
            try
            {
                await _restauranteDbContext.Set<Producto>().AddAsync(producto);
                await _restauranteDbContext.SaveChangesAsync();
                return producto;
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception("Error al agregar el producto a la base de datos", dbEx);
            }
            catch (ArgumentNullException argEx)
            {
                throw new Exception("El producto proporcionado es nulo", argEx);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar el producto", ex);
            }
        }

        public async Task<Producto> UpdateProducto(Producto producto)
        {
            try
            {
                var productoExistente = await _restauranteDbContext.Set<Producto>().FindAsync(producto.ProductoId);
                if (productoExistente == null || productoExistente.ProductoEstado == false)
                {
                    throw new InvalidOperationException("El producto no existe o está inactivo");
                }

                // Actualizamos los atributos del producto
                productoExistente.ProductoNombre = producto.ProductoNombre;
                productoExistente.ProductoDescripcion = producto.ProductoDescripcion;
                productoExistente.ProductoPrecio = producto.ProductoPrecio;
              

                _restauranteDbContext.Set<Producto>().Update(productoExistente);
                await _restauranteDbContext.SaveChangesAsync();
                return productoExistente;
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception("Error al actualizar el producto en la base de datos", dbEx);
            }
            catch (InvalidOperationException invOpEx)
            {
                throw new Exception("Error al actualizar el producto: " + invOpEx.Message, invOpEx);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el producto", ex);
            }
        }

        public async Task<Producto> DeleteProducto(int id)
        {
            try
            {
                var producto = await _restauranteDbContext.Set<Producto>().FindAsync(id);
                if (producto == null)
                {
                    throw new InvalidOperationException("El producto no existe");
                }
                _restauranteDbContext.Set<Producto>().Remove(producto);
                await _restauranteDbContext.SaveChangesAsync();
                return producto;
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception("Error al eliminar el producto de la base de datos", dbEx);
            }
            catch (InvalidOperationException invOpEx)
            {
                throw new Exception("Error al eliminar el producto: " + invOpEx.Message, invOpEx);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el producto", ex);
            }
        }
    }
}