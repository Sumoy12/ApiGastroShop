using Microsoft.AspNetCore.Mvc;
using ProyectoDAW.Models;
using ProyectoDAW.Repository;

namespace ProyectoDAW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagoController : ControllerBase
    {
        private readonly IPagoRepository _repositorioPago;

        public PagoController(IPagoRepository repositorioPago)
        {
            _repositorioPago = repositorioPago;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var lista = await _repositorioPago.GetPagos();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpGet("{pagoId}")]
        public async Task<IActionResult> Get(int pagoId)
        {
            try
            {
                var pago = await _repositorioPago.GetPagoById(pagoId);
                if (pago == null)
                {
                    return NotFound("Pago no encontrado.");
                }
                return Ok(pago);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Pago pago)
        {
            try
            {
                var pagoId = await _repositorioPago.AddPago(pago);
                return Ok(pagoId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPut("{pagoId}")]
        public async Task<IActionResult> Put(int pagoId, [FromBody] Pago pago)
        {
            try
            {
                Pago pagoExistente = await _repositorioPago.GetPagoById(pagoId);
                if (pagoExistente == null)
                {
                    return NotFound("Pago no encontrado.");
                }

                pagoExistente.metodoPago = pago.metodoPago;
                pagoExistente.MontoPago = pago.MontoPago;

                await _repositorioPago.UpdatePago(pagoExistente);
                return Ok(pagoExistente);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpDelete("{pagoId}")]
        public async Task<IActionResult> Delete(int pagoId)
        {
            try
            {
                await _repositorioPago.DeletePago(pagoId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
    }
}
