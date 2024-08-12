using Microsoft.AspNetCore.Mvc;
using ProyectoDAW.Models;
using ProyectoDAW.Repository;

namespace ProyectoDAW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompraController : ControllerBase
    {
        private readonly ICompraRepository _repositorioCompra;

        public CompraController(ICompraRepository repositorioCompra)
        {
            _repositorioCompra = repositorioCompra;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var lista = await _repositorioCompra.GetCompras();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpGet("{compraId}")]
        public async Task<IActionResult> Get(int compraId)
        {
            try
            {
                var compra = await _repositorioCompra.GetCompraId(compraId);
                if (compra == null)
                {
                    return NotFound("Compra no encontrada.");
                }
                return Ok(compra);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Compra compra)
        {
            try
            {
                var compraId = await _repositorioCompra.AddCompra(compra);
                return Ok(compraId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPut("{compraId}")]
        public async Task<IActionResult> Put(int compraId, [FromBody] Compra compra)
        {
            try
            {
                Compra compraExistente = await _repositorioCompra.GetCompraId(compraId);
                if (compraExistente == null || compraExistente.estado != false)
                {
                    return NotFound("Compra no encontrada.");
                }

                compraExistente.carrito = compra.carrito;
                compraExistente.subtotal = compra.subtotal;

                await _repositorioCompra.UpdateCompra(compraExistente);
                return Ok(compraExistente);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpDelete("{compraId}")]
        public async Task<IActionResult> Delete(int compraId)
        {
            try
            {
                await _repositorioCompra.DeleteCompra(compraId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
    }
}
