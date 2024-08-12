using Microsoft.AspNetCore.Mvc;
using ProyectoDAW.Models;
using ProyectoDAW.Repository;

namespace ProyectoDAW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedorController : ControllerBase
    {
        private readonly IProveedorRepository _repositorioProveedor;

        //Constructor

        public ProveedorController(IProveedorRepository repositorioProveedor)
        {
            _repositorioProveedor = repositorioProveedor;
        }



        // GET: api/<ProveedoresController>
        [HttpGet]

        public async Task<IActionResult> Get()
        {
            try
            {
                var lista = await _repositorioProveedor.ObtenerTodos();
                return Ok(lista);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.ToString());

            }
        }


        // POST api/<ProveedoresController>
        [HttpPost("{Nombre}/{Email}/{Telefono}/{Estado}")]
        public async Task<IActionResult> Post(string Nombre, string Email, string Telefono, bool Estado)
        {
            try
            {

                var Proveedor = new Proveedor
                {

                    Nombre = Nombre,
                    Email = Email,
                    Telefono = Telefono,
                    Estado = Estado

                };

                var ProveedorN = await _repositorioProveedor.Crear(Proveedor);
                return Ok(ProveedorN);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }


        // PUT api/<ProveedoresController>
        [HttpPut("{ProveedorId}/{Nombre}/{Cantidad}/{Precio}/{Tipo}/{Estado}")]
        public async Task<IActionResult> Put(int ProveedorId, string Nombre, string Email, string Telefono, bool Estado)
        {
            try
            {

                Proveedor ProveedorExistente = await _repositorioProveedor.ObtenerPorId(ProveedorId);
                if (ProveedorExistente == null)
                {
                    return NotFound("Proveedor no encontrado.");
                }
                ProveedorExistente.Nombre = Nombre;
                ProveedorExistente.Email = Email;
                ProveedorExistente.Telefono = Telefono;
                ProveedorExistente.Estado = Estado;

                // Modificar el Proveedor en la base de datos
                var ProveedorModificado = await _repositorioProveedor.ModificarProveedor(ProveedorExistente);
                return Ok(ProveedorModificado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }


        [HttpDelete("{ProveedorId}")]
        public async Task<IActionResult> Delete(int ProveedorId)
        {
            try
            {
                await _repositorioProveedor.CambiarEstadoProveedor(ProveedorId);
                return NoContent();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.ToString());
            }
        }

    }
}
