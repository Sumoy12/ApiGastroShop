using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using ProyectoDAW.Enums;
using ProyectoDAW.Models;
using ProyectoDAW.Repository;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProyectoDAW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarritoController : ControllerBase
    {
        private readonly ICarritoRepository _repositorioCarrito;

        public CarritoController(ICarritoRepository repositorioCarrito)
        {
            _repositorioCarrito = repositorioCarrito;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var lista = await _repositorioCarrito.GetCarritos();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpGet("{carritoId}")]
        public async Task<IActionResult> Get(int carritoId)
        {
            try
            {
                var carrito = await _repositorioCarrito.GetCarritoId(carritoId);
                if (carrito == null)
                {
                    return NotFound("Carrito no encontrado.");
                }
                return Ok(carrito);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPost("{productoId}/{precioCarrito}/{cantidadCarrito}/{estadoCarritoEnum}")]
        public async Task<IActionResult> Post(Producto producto, int precioCarrito,int cantidadCarrito, EstadoCarritoEnum estadoCarritoEnum)
        {
            try
            {

                var Carrito = new Carrito
                {
                    producto = producto,
                    precioCarrito = precioCarrito,
                    cantidadCarrito = cantidadCarrito,
                    estadoCarritoEnum = estadoCarritoEnum
                };
                

                await _repositorioCarrito.AddCarrito(Carrito);
                return Ok(Carrito);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPut("{carritoId}")]
        public async Task<IActionResult> Put(int carritoId, [FromBody] Carrito carrito)
        {
            try
            {
                Carrito carritoExistente = await _repositorioCarrito.GetCarritoId(carritoId);
                if (carritoExistente == null || carritoExistente.estadoCarritoEnum == Enums.EstadoCarritoEnum.Cancelado)
                {
                    return NotFound("Carrito no encontrado o cancelado.");
                }

                carritoExistente.precioCarrito = carrito.precioCarrito;
                carritoExistente.cantidadCarrito = carrito.cantidadCarrito;
                carritoExistente.producto = carrito.producto;
                

                await _repositorioCarrito.UpdateCarrito(carritoExistente);
                return Ok(carritoExistente);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpDelete("{carritoId}")]
        public async Task<IActionResult> Delete(int carritoId)
        {
            try
            {
                await _repositorioCarrito.DeleteCarrito(carritoId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
    }
}
