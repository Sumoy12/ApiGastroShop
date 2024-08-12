using Microsoft.EntityFrameworkCore;
using ProyectoDAW.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace ProyectoDAW.Repository
{
    public class PerfilRepository : IPerfilRepository
    {
        private readonly RestauranteDbContext _restauranteDbContext;

        public PerfilRepository(RestauranteDbContext restauranteDbContext)
        {
            _restauranteDbContext = restauranteDbContext;
        }

        public async Task<IEnumerable<Perfil>> GetPerfiles()
        {
            try
            {
                return await _restauranteDbContext.Set<Perfil>()
                    .Where(p => p.EstadoPerfil == true) // filtro para mostrar solo los activos
                    .ToListAsync();
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception("Error al obtener los perfiles de la base de datos", dbEx);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los perfiles", ex);
            }
        }

        public async Task<Perfil> GetPerfilById(int id)
        {
            try
            {
                var perfil = await _restauranteDbContext.Set<Perfil>().FindAsync(id);
                if (perfil == null || perfil.EstadoPerfil == false)
                {
                    throw new InvalidOperationException("El perfil no existe o está inactivo");
                }
                return perfil;
            }
            catch (InvalidOperationException invOpEx)
            {
                throw new Exception("Error al obtener el perfil: " + invOpEx.Message, invOpEx);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el perfil", ex);
            }
        }

        public async Task<Perfil> AddPerfil(Perfil perfil)
        {
            try
            {
                await _restauranteDbContext.Set<Perfil>().AddAsync(perfil);
                await _restauranteDbContext.SaveChangesAsync();
                return perfil;
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception("Error al agregar el perfil a la base de datos", dbEx);
            }
            catch (ArgumentNullException argEx)
            {
                throw new Exception("El perfil proporcionado es nulo", argEx);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar el perfil", ex);
            }
        }

        public async Task<Perfil> UpdatePerfil(Perfil perfil)
        {
            try
            {
                var perfilExistente = await _restauranteDbContext.Set<Perfil>().FindAsync(perfil.PerfilId);
                if (perfilExistente == null || perfilExistente.EstadoPerfil == false)
                {
                    throw new InvalidOperationException("El perfil no existe o está inactivo");
                }

                // Actualizamos los atributos del perfil
                perfilExistente.NombrePerfil = perfil.NombrePerfil;
                perfilExistente.ApellidosPerfil = perfil.ApellidosPerfil;
                perfilExistente.EmailPerfil = perfil.EmailPerfil;
                perfilExistente.UsuarioPerfil = perfil.UsuarioPerfil;
                perfilExistente.PasswordPerfil = perfil.PasswordPerfil;

                _restauranteDbContext.Set<Perfil>().Update(perfilExistente);
                await _restauranteDbContext.SaveChangesAsync();
                return perfilExistente;
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception("Error al actualizar el perfil en la base de datos", dbEx);
            }
            catch (InvalidOperationException invOpEx)
            {
                throw new Exception("Error al actualizar el perfil: " + invOpEx.Message, invOpEx);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el perfil", ex);
            }
        }

        public async Task<Perfil> DeletePerfil(int id)
        {
            try
            {
                var perfil = await _restauranteDbContext.Set<Perfil>().FindAsync(id);
                if (perfil == null)
                {
                    throw new InvalidOperationException("El perfil no existe");
                }
                _restauranteDbContext.Set<Perfil>().Remove(perfil);
                await _restauranteDbContext.SaveChangesAsync();
                return perfil;
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception("Error al eliminar el perfil de la base de datos", dbEx);
            }
            catch (InvalidOperationException invOpEx)
            {
                throw new Exception("Error al eliminar el perfil: " + invOpEx.Message, invOpEx);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el perfil", ex);
            }
        }
    }
}