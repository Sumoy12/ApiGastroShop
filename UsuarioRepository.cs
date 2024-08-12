using Microsoft.EntityFrameworkCore;
using ProyectoDAW.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Collections;

namespace ProyectoDAW.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly RestauranteDbContext _restauranteDbContext;

        public UsuarioRepository(RestauranteDbContext restauranteDbContext)
        {
            _restauranteDbContext = restauranteDbContext;
        }

        public async Task<List<Usuario>> GetUsuarios()
        {
            List<Usuario> resList = new List<Usuario>();
            List<Usuario> resListResult = new List<Usuario>();
            try
            {
                resList = await _restauranteDbContext.Set<Usuario>().ToListAsync();
                



                return resListResult;

            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception("Error al obtener el usuario de la base de datos", dbEx);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el usuario", ex);
            }
        }

        public async Task<Usuario> GetUsuarioById(int id)
        {
            try
            {
                var usuario = await _restauranteDbContext.Set<Usuario>().FindAsync(id);
                if (usuario == null || usuario.UsuarioEstado == false)
                {
                    throw new InvalidOperationException("El usuario no existe o está inactivo");
                }
                return usuario;
            }
            catch (InvalidOperationException invOpEx)
            {
                throw new Exception("Error al obtener el usuario: " + invOpEx.Message, invOpEx);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el usuario", ex);
            }
        }

        public async Task<Usuario> AddUsuario(Usuario usuario)
        {
            try
            {
                await _restauranteDbContext.Set<Usuario>().AddAsync(usuario);
                await _restauranteDbContext.SaveChangesAsync();
                return usuario;
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception("Error al agregar el usuario a la base de datos", dbEx);
            }
            catch (ArgumentNullException argEx)
            {
                throw new Exception("El usuario proporcionado es nulo", argEx);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar el usuario", ex);
            }
        }

        public async Task<Usuario> UpdateUsuario(Usuario usuario)
        {
            try
            {
                var usuarioExistente = await _restauranteDbContext.Set<Usuario>().FindAsync(usuario.UsuarioId);
                if (usuarioExistente == null || usuarioExistente.UsuarioEstado == false)
                {
                    throw new InvalidOperationException("El usuario no existe o está inactivo");
                }

                // Actualizamos los atributos del usuario
                usuarioExistente.UsuarioNombre = usuario.UsuarioNombre;
                usuarioExistente.UsuarioApellido = usuario.UsuarioApellido;
                usuarioExistente.UsuarioEmail = usuario.UsuarioEmail;
                usuarioExistente.UsuarioPassword = usuario.UsuarioPassword;


                _restauranteDbContext.Set<Usuario>().Update(usuarioExistente);
                await _restauranteDbContext.SaveChangesAsync();
                return usuarioExistente;
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception("Error al actualizar el usuario en la base de datos", dbEx);
            }
            catch (InvalidOperationException invOpEx)
            {
                throw new Exception("Error al actualizar el usuario: " + invOpEx.Message, invOpEx);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el usuario", ex);
            }
        }

        public async Task<Usuario> DeleteUsuario(int id)
        {
            try
            {
                var usuario = await _restauranteDbContext.Set<Usuario>().FindAsync(id);
                if (usuario == null)
                {
                    throw new InvalidOperationException("El usuario no existe");
                }
                _restauranteDbContext.Set<Usuario>().Remove(usuario);
                await _restauranteDbContext.SaveChangesAsync();
                return usuario;
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception("Error al eliminar el usuario de la base de datos", dbEx);
            }
            catch (InvalidOperationException invOpEx)
            {
                throw new Exception("Error al eliminar el usuario: " + invOpEx.Message, invOpEx);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el usuario", ex);
            }
        }
    }
}