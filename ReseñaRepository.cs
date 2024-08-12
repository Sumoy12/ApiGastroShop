using Microsoft.EntityFrameworkCore;
using ProyectoDAW.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProyectoDAW.Repository
{
    public class ResenaRepository : IReseñaRepository
    {
        private readonly RestauranteDbContext _restauranteDbContext;

        public ResenaRepository(RestauranteDbContext restauranteDbContext)
        {
            _restauranteDbContext = restauranteDbContext;
        }

        public async Task<List<Resena>> GetResenas()
        {
            List<Resena> resList = new List<Resena>();
            List<Resena> resListResult = new List<Resena>();
            Usuario usuario = null;

            try
            {
                resList = await _restauranteDbContext.Set<Resena>().ToListAsync();

                foreach (Resena item in resList)
                {
                    usuario = new Usuario();
                    usuario = await _restauranteDbContext.usuarios.FindAsync(item.UsuarioId);
                    item.Usuario = usuario;
                    resListResult.Add(item);



                }

                

                return resListResult;

            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception("Error al obtener las reseñas de la base de datos", dbEx);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener las reseñas", ex);
            }
        }

        public async Task<Resena> GetResenaById(int id)
        {
            try
            {
                var resena = await _restauranteDbContext.Set<Resena>().FindAsync(id);
                if (resena == null)
                {
                    throw new InvalidOperationException("La reseña no existe");
                }
                return resena;
            }
            catch (InvalidOperationException invOpEx)
            {
                throw new Exception("Error al obtener la reseña: " + invOpEx.Message, invOpEx);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la reseña", ex);
            }
        }

        public async Task<Resena> AddResena(Resena resena)
        {
            try
            {
                await _restauranteDbContext.Set<Resena>().AddAsync(resena);
                await _restauranteDbContext.SaveChangesAsync();
                return resena;
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception("Error al agregar la reseña a la base de datos", dbEx);
            }
            catch (ArgumentNullException argEx)
            {
                throw new Exception("La reseña proporcionada es nula", argEx);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar la reseña", ex);
            }
        }

        public async Task<Resena> UpdateResena(Resena resena)
        {
            try
            {
                var resenaExistente = await _restauranteDbContext.Set<Resena>().FindAsync(resena.ResenaId);
                if (resenaExistente == null)
                {
                    throw new InvalidOperationException("La reseña no existe");
                }

                // Actualizamos los atributos de la reseña
                resenaExistente.Usuario = resena.Usuario;
                resenaExistente.resenaEnum = resena.resenaEnum;
                resenaExistente.Comentario = resena.Comentario;

                _restauranteDbContext.Set<Resena>().Update(resenaExistente);
                await _restauranteDbContext.SaveChangesAsync();
                return resenaExistente;
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception("Error al actualizar la reseña en la base de datos", dbEx);
            }
            catch (InvalidOperationException invOpEx)
            {
                throw new Exception("Error al actualizar la reseña: " + invOpEx.Message, invOpEx);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar la reseña", ex);
            }
        }

        public async Task<Resena> DeleteResena(int id)
        {
            try
            {
                var resena = await _restauranteDbContext.Set<Resena>().FindAsync(id);
                if (resena == null)
                {
                    throw new InvalidOperationException("La reseña no existe");
                }
                _restauranteDbContext.Set<Resena>().Remove(resena);
                await _restauranteDbContext.SaveChangesAsync();
                return resena;
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception("Error al eliminar la reseña de la base de datos", dbEx);
            }
            catch (InvalidOperationException invOpEx)
            {
                throw new Exception("Error al eliminar la reseña: " + invOpEx.Message, invOpEx);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar la reseña", ex);
            }
        }
    }
}