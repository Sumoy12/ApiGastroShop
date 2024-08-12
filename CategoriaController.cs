using Microsoft.AspNetCore.Mvc;
using ProyectoDAW.Models;
using ProyectoDAW.Repository;

namespace ProyectoDAW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaController(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        // GET: api/Categoria
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categoria>>> GetCategorias()
        {
            var categorias = await _categoriaRepository.GetCategorias();
            return Ok(categorias);
        }

        // GET: api/Categoria/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Categoria>> GetCategoria(int id)
        {
            var categoria = await _categoriaRepository.GetCategoriaId(id);
            if (categoria == null)
            {
                return NotFound("La categoría no existe o está inactiva.");
            }
            return Ok(categoria);
        }

        // POST: api/Categoria
        [HttpPost]
        public async Task<ActionResult<Categoria>> AddCategoria(Categoria categoria)
        {
            var nuevaCategoria = await _categoriaRepository.AddCategoria(categoria);
            return CreatedAtAction(nameof(GetCategoria), new { id = nuevaCategoria.CategoriaId }, nuevaCategoria);
        }

        // PUT: api/Categoria/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Categoria>> UpdateCategoria(int id, Categoria categoria)
        {
            if (id != categoria.CategoriaId)
            {
                return BadRequest("El ID de la categoría no coincide.");
            }

            var categoriaActualizada = await _categoriaRepository.UpdateCategoria(categoria);
            if (categoriaActualizada == null)
            {
                return NotFound("La categoría no existe o está inactiva.");
            }

            return Ok(categoriaActualizada);
        }

        // DELETE: api/Categoria/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Categoria>> DeleteCategoria(int id)
        {
            var categoriaEliminada = await _categoriaRepository.DeleteCategoria(id);
            if (categoriaEliminada == null)
            {
                return NotFound("La categoría no existe.");
            }

            return Ok(categoriaEliminada);
        }
    }
}
