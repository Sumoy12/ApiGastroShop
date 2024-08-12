using Microsoft.AspNetCore.Mvc;
using ProyectoDAW.Models;
using ProyectoDAW.Repository;

namespace ProyectoDAW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _repositorioUsuario;

        //Constructor

        public UsuarioController(IUsuarioRepository repositorioUsuario)
        {
            _repositorioUsuario = repositorioUsuario;
        }



        // GET: api/<UsuarioesController>
        [HttpGet]

        public async Task<IActionResult> Get()
        {
            try
            {
                var lista = await _repositorioUsuario.GetUsuarios();
                return Ok(lista);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.ToString());

            }
        }


        // POST api/<UsuarioesController>
        [HttpPost("{UsuarioNombre}/{UsuarioApellido}/{UsuarioEmail}/{UsuarioPassword}/{UsuarioDireccion}/{UsuarioTelefono}/{UsuarioEstado}")]
        public async Task<IActionResult> Post(string UsuarioNombre, string UsuarioApellido, string UsuarioEmail, string UsuarioPassword, string UsuarioDireccion, string UsuarioTelefono, bool UsuarioEstado)
        {
            try
            {

                var Usuario = new Usuario
                {
                    UsuarioNombre = UsuarioNombre,
                    UsuarioApellido = UsuarioApellido,
                    UsuarioEmail = UsuarioEmail,
                    UsuarioPassword = UsuarioPassword,
                    UsuarioEstado = UsuarioEstado
                };

                var UsuarioN = await _repositorioUsuario.AddUsuario(Usuario);
                return Ok(UsuarioN);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }


        // PUT api/<UsuarioesController>
        [HttpPut("{UsuarioId}/{UsuarioNombre}/{UsuarioApellido}/{UsuarioEmail}/{UsuarioPassword}/{UsuarioDireccion}/{UsuarioTelefono}/{UsuarioEstado}")]
        public async Task<IActionResult> Put(int UsuarioId, string UsuarioNombre, string UsuarioEmail, string UsuarioPassword, string UsuarioDireccion, string UsuarioTelefono, bool UsuarioEstado)
        {
            try
            {

                Usuario UsuarioExistente = await _repositorioUsuario.GetUsuarioById(UsuarioId);
                if (UsuarioExistente == null)
                {
                    return NotFound("Usuario no encontrado.");
                }

                UsuarioExistente.UsuarioId = UsuarioId;
                UsuarioExistente.UsuarioNombre = UsuarioNombre;
                UsuarioExistente.UsuarioEmail = UsuarioEmail;
                UsuarioExistente.UsuarioPassword = UsuarioPassword;
                UsuarioExistente.UsuarioEstado = UsuarioEstado;

                // Modificar el Usuario en la base de datos
                var UsuarioModificado = await _repositorioUsuario.UpdateUsuario(UsuarioExistente);
                return Ok(UsuarioModificado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }


        [HttpDelete("{UsuarioId}")]
        public async Task<IActionResult> Delete(int UsuarioId)
        {
            try
            {
                await _repositorioUsuario.DeleteUsuario(UsuarioId);
                return NoContent();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.ToString());
            }
        }

    }
}
