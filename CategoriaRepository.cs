using Microsoft.EntityFrameworkCore;
using ProyectoDAW.Models;

namespace ProyectoDAW.Repository
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly RestauranteDbContext  _restauranteDbContext;

        public CategoriaRepository(RestauranteDbContext restauranteDbContext)
        {
            _restauranteDbContext = restauranteDbContext;
        }

        //crud
        //CREATE CATEGORIA
        public async Task<Categoria> AddCategoria(Categoria categoria)
        {
            try
            {
                await _restauranteDbContext.Set<Categoria>().AddAsync(categoria);
                await _restauranteDbContext.SaveChangesAsync();
                return categoria; // Return the added carrito
            }
            catch (Exception ex)
            {
                // Manejar la excepción (por ejemplo, loguearla)
                throw new Exception("Error al agregar la categoria", ex);
            }
        }
        //read categoria por id
        public async Task<Categoria> GetCategoriaId(int id)
        {
            try
            {
                var categoria = await _restauranteDbContext.Set<Categoria>().FindAsync(id);
                if ((categoria == null) && (categoria.CategoriaEstado == false))//filtro para mostrar solo los activos
                {
                    throw new Exception("El carrito no existe");
                }
                return categoria;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la categoria", ex);
            }
        }

        public async Task<IEnumerable<Categoria>> GetCategorias()
        {
            try
            {
                return await _restauranteDbContext.Set<Categoria>()
                    .Where(c  => c.CategoriaEstado == true) //filtro para mostrar solo los activos
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener las categorias", ex);
            }
        }

        public async Task<Categoria> UpdateCategoria(Categoria categoria)
        {
            try
            {
                var categoriaExistente = await _restauranteDbContext.Set<Categoria>().FindAsync(categoria.CategoriaId);
                if (categoriaExistente == null && categoriaExistente.CategoriaEstado == false)
                {
                    throw new Exception("El carrito no existe");
                }

                //actualizamos los atributos de la categoria
                categoriaExistente.CategoriaNombre = categoria.CategoriaNombre;
                categoriaExistente.Descripcion = categoria.Descripcion;
                categoriaExistente.CategoriaEstado = categoria.CategoriaEstado;

                _restauranteDbContext.Set<Categoria>().Update(categoriaExistente);
                await _restauranteDbContext.SaveChangesAsync();
                return categoriaExistente; // Return el updated de la categoria
            }
            catch
            (Exception ex)
            {
                throw new Exception("Error al actualizar la categoria", ex);
            }
        }

        public async Task<Categoria> DeleteCategoria(int id)
        {
            try
            {
                var categoria = await _restauranteDbContext.Set<Categoria>().FindAsync(id);
                if (categoria == null)
                {
                    throw new Exception("El carrito no existe");
                }
                _restauranteDbContext.Set<Categoria>().Remove(categoria);
                await _restauranteDbContext.SaveChangesAsync();
                return categoria; // Return el deleted carrito
            }
            catch (Exception ex)
            {

                throw new Exception("Error al eliminar el carrito", ex);
            }
        }
    }
}
