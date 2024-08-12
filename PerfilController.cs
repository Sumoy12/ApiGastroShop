using Microsoft.AspNetCore.Mvc;
using ProyectoDAW.Models;
using ProyectoDAW.Repository;

namespace ProyectoDAW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerfilController : ControllerBase
    {
        private readonly IPerfilRepository _repositorioPerfil;

        //Constructor

        public PerfilController(IPerfilRepository repositorioPerfil)
        {
            _repositorioPerfil = repositorioPerfil;
        }



        // GET: api/<PerfilesController>
        [HttpGet]

        public async Task<IActionResult> Get()
        {
            try
            {
                var lista = await _repositorioPerfil.GetPerfiles();
                return Ok(lista);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.ToString());

            }
        }


        // POST api/<PerfilesController>
        [HttpPost("{NombrePerfil}/{ApellidosPerfil}/{EmailPerfil}/{UsuarioPerfil}/{PasswordPerfil}/{EstadoPerfil}")]
        public async Task<IActionResult> Post(string NombrePerfil, string ApellidosPerfil, string EmailPerfil, string UsuarioPerfil, string PasswordPerfil, bool EstadoPerfil)
        {
            try
            {

                var Perfil = new Perfil
                {
                    NombrePerfil = NombrePerfil,
                    ApellidosPerfil = ApellidosPerfil,
                    EmailPerfil = EmailPerfil,
                    UsuarioPerfil = UsuarioPerfil,
                    PasswordPerfil = PasswordPerfil,
                    EstadoPerfil = EstadoPerfil
                };

                var PerfilN = await _repositorioPerfil.AddPerfil(Perfil);
                return Ok(PerfilN);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }


        // PUT api/<PerfilesController>
        [HttpPut("{PerfilId}/{NombrePerfil}/{ApellidosPerfil}/{EmailPerfil}/{UsuarioPerfil}/{PasswordPerfil}/{EstadoPerfil}")]
        public async Task<IActionResult> Put(int PerfilId, string NombrePerfil, string ApellidosPerfil, string EmailPerfil, string UsuarioPerfil, string PasswordPerfil, bool EstadoPerfil)
        {
            try
            {

                Perfil PerfilExistente = await _repositorioPerfil.GetPerfilById(PerfilId);
                if (PerfilExistente == null)
                {
                    return NotFound("Perfil no encontrado.");
                }

                PerfilExistente.PerfilId = PerfilId;
                PerfilExistente.NombrePerfil = NombrePerfil;
                PerfilExistente.ApellidosPerfil = ApellidosPerfil;
                PerfilExistente.EmailPerfil = EmailPerfil;
                PerfilExistente.UsuarioPerfil = UsuarioPerfil;
                PerfilExistente.PasswordPerfil = PasswordPerfil;
                PerfilExistente.EstadoPerfil = EstadoPerfil;

                // Modificar el Perfil en la base de datos
                var PerfilModificado = await _repositorioPerfil.UpdatePerfil(PerfilExistente);
                return Ok(PerfilModificado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }


        [HttpDelete("{PerfilId}")]
        public async Task<IActionResult> Delete(int PerfilId)
        {
            try
            {
                await _repositorioPerfil.DeletePerfil(PerfilId);
                return NoContent();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.ToString());
            }
        }

    }
}
