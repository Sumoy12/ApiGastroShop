using Microsoft.EntityFrameworkCore;
using ProyectoDAW.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace ProyectoDAW.Repository
{
    public class CompraRepository : ICompraRepository
    {
        private readonly RestauranteDbContext _restauranteDbContext;

        public CompraRepository(RestauranteDbContext restauranteDbContext)
        {
            _restauranteDbContext = restauranteDbContext;
        }

        public async Task<Compra> AddCompra(Compra compra)
        {
            try
            {
                await _restauranteDbContext.Set<Compra>().AddAsync(compra);
                await _restauranteDbContext.SaveChangesAsync();
                return compra;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar la compra", ex);
            }
        }

        public async Task<Compra> GetCompraId(int id)
        {
            try
            {
                var compra = await _restauranteDbContext.Set<Compra>().FindAsync(id);
                if ((compra == null) || (compra.estado == false)) // filtro para mostrar solo los activos
                {
                    throw new Exception("La compra no existe");
                }
                return compra;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la compra", ex);
            }
        }

        public async Task<IEnumerable<Compra>> GetCompras()
        {
            try
            {
                return await _restauranteDbContext.Set<Compra>()
                    .Where(c => c.estado == true) // filtro para mostrar solo los activos
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener las compras", ex);
            }
        }

        public async Task<Compra> UpdateCompra(Compra compra)
        {
            try
            {
                var compraExistente = await _restauranteDbContext.Set<Compra>().FindAsync(compra.CompraId);
                if (compraExistente == null || compraExistente.estado == false)
                {
                    throw new Exception("La compra no existe");
                }

                // Actualizamos los atributos de la compra - solo se edita lo que se debe editar, no todos los atributos
                compraExistente.carrito = compra.carrito;
                compraExistente.subtotal = compra.subtotal;

                _restauranteDbContext.Set<Compra>().Update(compraExistente);
                await _restauranteDbContext.SaveChangesAsync();
                return compraExistente; // Return la compra actualizada
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar la compra", ex);
            }
        }

        public async Task<Compra> DeleteCompra(int id)
        {
            try
            {
                var compra = await _restauranteDbContext.Set<Compra>().FindAsync(id);
                if (compra == null)
                {
                    throw new Exception("La compra no existe");
                }
                _restauranteDbContext.Set<Compra>().Remove(compra);
                await _restauranteDbContext.SaveChangesAsync();
                return compra;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar la compra", ex);
            }
        }
    }
}
