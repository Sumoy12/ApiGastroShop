using Microsoft.AspNetCore.Mvc;
using ProyectoDAW.Models;
using ProyectoDAW.Repository;

namespace ProyectoDAW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturacionController : ControllerBase
    {
        private readonly IFacturacionRepository _repositorioFacturacion;

        public FacturacionController(IFacturacionRepository repositorioFacturacion)
        {
            _repositorioFacturacion = repositorioFacturacion;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var lista = await _repositorioFacturacion.GetFacturaciones();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpGet("{facturaId}")]
        public async Task<IActionResult> Get(int facturaId)
        {
            try
            {
                var facturacion = await _repositorioFacturacion.GetFacturacionById(facturaId);
                if (facturacion == null)
                {
                    return NotFound("Facturación no encontrada.");
                }
                return Ok(facturacion);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Facturacion facturacion)
        {
            try
            {
                var facturaId = await _repositorioFacturacion.AddFacturacion(facturacion);
                return Ok(facturaId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPut("{facturaId}")]
        public async Task<IActionResult> Put(int facturaId, [FromBody] Facturacion facturacion)
        {
            try
            {
                Facturacion facturacionExistente = await _repositorioFacturacion.GetFacturacionById(facturaId);
                if (facturacionExistente == null || facturacionExistente.estadoFactura != false)
                {
                    return NotFound("Facturación no encontrada.");
                }

                facturacionExistente.CompraId = facturacion.CompraId;
                facturacionExistente.PagoId = facturacion.PagoId;
                facturacionExistente.IvaFactura = facturacion.IvaFactura;
                facturacionExistente.TotalFactura = facturacion.TotalFactura;
                

                await _repositorioFacturacion.UpdateFacturacion(facturacionExistente);
                return Ok(facturacionExistente);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpDelete("{facturaId}")]
        public async Task<IActionResult> Delete(int facturaId)
        {
            try
            {
                await _repositorioFacturacion.DeleteFacturacion(facturaId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
    }
}
