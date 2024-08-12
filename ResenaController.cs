using Microsoft.AspNetCore.Mvc;
using ProyectoDAW.Enums;
using ProyectoDAW.Models;
using ProyectoDAW.Repository;

namespace ProyectoDAW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResenaController : ControllerBase
    {
        private readonly IReseñaRepository _repositorioReseña;

        //Constructor

        public ResenaController(IReseñaRepository repositorioResena)
        {
            _repositorioReseña = repositorioResena;
        }



        // GET: api/<ResenaesController>
        [HttpGet]

        public async Task<IActionResult> Get()
        {
            try
            {
                var lista = await _repositorioReseña.GetResenas();
                return Ok(lista);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.ToString());

            }
        }


        // POST api/<ResenaesController>
        [HttpPost("{UsuarioId}/{resenaEnum}/{Comentario}")]
        public async Task<IActionResult> Post(int UsuarioId, ResenaEnum resenaEnum, [FromQuery] string comentario)
        {
            try
            {

                var Resena = new Resena
                {
                    UsuarioId = UsuarioId,
                    resenaEnum = resenaEnum,
                    Comentario = comentario
                };

                await _repositorioReseña.AddResena(Resena);
                return Ok(Resena);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }


        // PUT api/<ResenaesController>
        [HttpPut("{ResenaId}/{UsuarioId}/{resenaEnum}/{Comentario}")]
        public async Task<IActionResult> Put(int ResenaId, int UsuarioId, ResenaEnum resenaEnum, string comentario)
        {
            try
            {

                Resena ResenaExistente = await _repositorioReseña.GetResenaById(ResenaId);
                if (ResenaExistente == null)
                {
                    return NotFound("Resena no encontrado.");
                }

                ResenaExistente.ResenaId = ResenaId;
                ResenaExistente.UsuarioId = UsuarioId;
                ResenaExistente.resenaEnum = resenaEnum;
                ResenaExistente.Comentario = comentario;

                // Modificar el Resena en la base de datos
                var ResenaModificado = await _repositorioReseña.UpdateResena(ResenaExistente);
                return Ok(ResenaModificado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }


        [HttpDelete("{ResenaId}")]
        public async Task<IActionResult> Delete(int ResenaId)
        {
            try
            {
                await _repositorioReseña.DeleteResena(ResenaId);
                return NoContent();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.ToString());
            }
        }

    }
}
