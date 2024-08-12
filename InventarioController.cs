using Microsoft.AspNetCore.Mvc;
using ProyectoDAW.Models;
using ProyectoDAW.Repository;

namespace ProyectoDAW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventarioController : ControllerBase
    {
        private readonly IInventarioRepository _repositorioInventario;

        //Constructor

        public InventarioController(IInventarioRepository repositorioInventario)
        {
            _repositorioInventario = repositorioInventario;
        }



        // GET: api/<InventarioesController>
        [HttpGet]

        public async Task<IActionResult> Get()
        {
            try
            {
                var lista = await _repositorioInventario.ObtenerTodos();
                return Ok(lista);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.ToString());

            }
        }


        // POST api/<InventarioesController>
        [HttpPost("{ProductoId}/{Cantidad}/{Precio}/{Tipo}/{Estado}/{ProveedorId}")]
        public async Task<IActionResult> Post(int ProductoId, int Cantidad, double Precio, string Tipo, bool Estado, int ProveedorId)
        {
            try
            {

                var Inventario = new Inventario
                {
                    ProductoId = ProductoId,
                    Cantidad = Cantidad,
                    Precio = Precio,
                    Tipo = Tipo,
                    Estado = Estado,
                    ProveedorId = ProveedorId
                };

                int InventarioN = await _repositorioInventario.Crear(Inventario);
                return Ok(InventarioN);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }


        // PUT api/<InventarioesController>
        [HttpPut("{InventarioId}/{ProductoId}/{Cantidad}/{Precio}/{Tipo}/{Estado}/{ProveedorId}")]
        public async Task<IActionResult> Put(int InventarioId, int ProductoId, int Cantidad, double Precio, string Tipo, bool Estado, int ProveedorId)
        {
            try
            {

                Inventario InventarioExistente = await _repositorioInventario.ObtenerPorId(InventarioId);
                if (InventarioExistente == null)
                {
                    return NotFound("Inventario no encontrado.");
                }
                InventarioExistente.ProductoId = ProductoId;
                InventarioExistente.Cantidad = Cantidad;
                InventarioExistente.Precio = Precio;
                InventarioExistente.Tipo = Tipo;
                InventarioExistente.Estado = Estado;
                InventarioExistente.ProveedorId = ProveedorId;

                // Modificar el Inventario en la base de datos
                var InventarioModificado = await _repositorioInventario.ModificarInventario(InventarioExistente);
                return Ok(InventarioModificado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }


        [HttpDelete("{InventarioId}")]
        public async Task<IActionResult> Delete(int InventarioId)
        {
            try
            {
                await _repositorioInventario.CambiarEstadoInventario(InventarioId);
                return NoContent();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.ToString());
            }
        }
    }
}
