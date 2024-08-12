using Microsoft.EntityFrameworkCore;
using ProyectoDAW.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace ProyectoDAW.Repository
{
    public class FacturacionRepository : IFacturacionRepository
    {
        private readonly RestauranteDbContext _restauranteDbContext;

        public FacturacionRepository(RestauranteDbContext restauranteDbContext)
        {
            _restauranteDbContext = restauranteDbContext;
        }

        public async Task<IEnumerable<Facturacion>> GetFacturaciones()
        {
            try
            {
                return await _restauranteDbContext.Set<Facturacion>()
                    .Where(f => f.estadoFactura == true) // filtro para mostrar solo los activos
                    .ToListAsync();
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception("Error al obtener las facturaciones de la base de datos", dbEx);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener las facturaciones", ex);
            }
        }

        public async Task<Facturacion> GetFacturacionById(int id)
        {
            try
            {
                var facturacion = await _restauranteDbContext.Set<Facturacion>().FindAsync(id);
                if (facturacion == null || facturacion.estadoFactura == false)
                {
                    throw new InvalidOperationException("La facturación no existe o está inactiva");
                }
                return facturacion;
            }
            catch (InvalidOperationException invOpEx)
            {
                throw new Exception("Error al obtener la facturación: " + invOpEx.Message, invOpEx);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la facturación", ex);
            }
        }

        public async Task<Facturacion> AddFacturacion(Facturacion facturacion)
        {
            try
            {
                await _restauranteDbContext.Set<Facturacion>().AddAsync(facturacion);
                await _restauranteDbContext.SaveChangesAsync();
                return facturacion;
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception("Error al agregar la facturación a la base de datos", dbEx);
            }
            catch (ArgumentNullException argEx)
            {
                throw new Exception("La facturación proporcionada es nula", argEx);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar la facturación", ex);
            }
        }

        public async Task<Facturacion> UpdateFacturacion(Facturacion facturacion)
        {
            try
            {
                var facturacionExistente = await _restauranteDbContext.Set<Facturacion>().FindAsync(facturacion.FacturaId);
                if (facturacionExistente == null || facturacionExistente.estadoFactura == false)
                {
                    throw new InvalidOperationException("La facturación no existe o está inactiva");
                }

                // Actualizamos los atributos de la facturación
                facturacionExistente.UsuarioId = facturacion.UsuarioId;
                facturacionExistente.CompraId = facturacion.CompraId;
                facturacionExistente.PagoId = facturacion.PagoId;
                facturacionExistente.IvaFactura = facturacion.IvaFactura;
                facturacionExistente.TotalFactura = facturacion.TotalFactura;

                _restauranteDbContext.Set<Facturacion>().Update(facturacionExistente);
                await _restauranteDbContext.SaveChangesAsync();
                return facturacionExistente;
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception("Error al actualizar la facturación en la base de datos", dbEx);
            }
            catch (InvalidOperationException invOpEx)
            {
                throw new Exception("Error al actualizar la facturación: " + invOpEx.Message, invOpEx);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar la facturación", ex);
            }
        }

        public async Task<Facturacion> DeleteFacturacion(int id)
        {
            try
            {
                var facturacion = await _restauranteDbContext.Set<Facturacion>().FindAsync(id);
                if (facturacion == null)
                {
                    throw new InvalidOperationException("La facturación no existe");
                }
                _restauranteDbContext.Set<Facturacion>().Remove(facturacion);
                await _restauranteDbContext.SaveChangesAsync();
                return facturacion;
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception("Error al eliminar la facturación de la base de datos", dbEx);
            }
            catch (InvalidOperationException invOpEx)
            {
                throw new Exception("Error al eliminar la facturación: " + invOpEx.Message, invOpEx);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar la facturación", ex);
            }
        }
    }
}