using Microsoft.EntityFrameworkCore;
using ProyectoDAW.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace ProyectoDAW.Repository
{
    public class PagoRepository : IPagoRepository
    {
        private readonly RestauranteDbContext _restauranteDbContext;

        public PagoRepository(RestauranteDbContext restauranteDbContext)
        {
            _restauranteDbContext = restauranteDbContext;
        }

        public async Task<IEnumerable<Pago>> GetPagos()
        {
            try
            {
                return await _restauranteDbContext.Set<Pago>().ToListAsync();
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception("Error al obtener los pagos de la base de datos", dbEx);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los pagos", ex);
            }
        }

        public async Task<Pago> GetPagoById(int id)
        {
            try
            {
                var pago = await _restauranteDbContext.Set<Pago>().FindAsync(id);
                if (pago == null)
                {
                    throw new InvalidOperationException("El pago no existe");
                }
                return pago;
            }
            catch (InvalidOperationException invOpEx)
            {
                throw new Exception("Error al obtener el pago: " + invOpEx.Message, invOpEx);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el pago", ex);
            }
        }

        public async Task<Pago> AddPago(Pago pago)
        {
            try
            {
                await _restauranteDbContext.Set<Pago>().AddAsync(pago);
                await _restauranteDbContext.SaveChangesAsync();
                return pago;
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception("Error al agregar el pago a la base de datos", dbEx);
            }
            catch (ArgumentNullException argEx)
            {
                throw new Exception("El pago proporcionado es nulo", argEx);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar el pago", ex);
            }
        }

        public async Task<Pago> UpdatePago(Pago pago)
        {
            try
            {
                var pagoExistente = await _restauranteDbContext.Set<Pago>().FindAsync(pago.PagoId);
                if (pagoExistente == null)
                {
                    throw new InvalidOperationException("El pago no existe");
                }

                // Actualizamos los atributos del pago
                pagoExistente.metodoPago = pago.metodoPago;
                pagoExistente.MontoPago = pago.MontoPago;

                _restauranteDbContext.Set<Pago>().Update(pagoExistente);
                await _restauranteDbContext.SaveChangesAsync();
                return pagoExistente;
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception("Error al actualizar el pago en la base de datos", dbEx);
            }
            catch (InvalidOperationException invOpEx)
            {
                throw new Exception("Error al actualizar el pago: " + invOpEx.Message, invOpEx);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el pago", ex);
            }
        }

        public async Task<Pago> DeletePago(int id)
        {
            try
            {
                var pago = await _restauranteDbContext.Set<Pago>().FindAsync(id);
                if (pago == null)
                {
                    throw new InvalidOperationException("El pago no existe");
                }
                _restauranteDbContext.Set<Pago>().Remove(pago);
                await _restauranteDbContext.SaveChangesAsync();
                return pago;
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception("Error al eliminar el pago de la base de datos", dbEx);
            }
            catch (InvalidOperationException invOpEx)
            {
                throw new Exception("Error al eliminar el pago: " + invOpEx.Message, invOpEx);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el pago", ex);
            }
        }
    }
}