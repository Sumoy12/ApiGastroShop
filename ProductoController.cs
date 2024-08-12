using Microsoft.AspNetCore.Mvc;
using ProyectoDAW.Models;
using ProyectoDAW.Repository;

namespace ProyectoDAW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoRepository _repositorioProducto;

        //Constructor

        public ProductoController(IProductoRepository repositorioProducto)
        {
            _repositorioProducto = repositorioProducto;
        }



        // GET: api/<ProductoesController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var lista = await _repositorioProducto.GetProductos();
                return Ok(lista);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.ToString());

            }
        }


        // POST api/<ProductoesController>
        [HttpPost("{CategoriaId}/{ProductoNombre}/{ProductoDescripcion}/{ProductoPrecio}/{ProductoEstado}")]
        public async Task<IActionResult> Post(int CategoriaId, string ProductoNombre, string ProductoDescripcion, decimal ProductoPrecio, bool ProductoEstado)
        {
            try
            {

                var Producto = new Producto
                {
                    CategoriaId = CategoriaId,
                    ProductoNombre = ProductoNombre,
                    ProductoDescripcion = ProductoDescripcion,
                    ProductoPrecio = ProductoPrecio,
                    ProductoEstado = ProductoEstado,
                };

                var ProductoN = await _repositorioProducto.AddProducto(Producto);
                return Ok(ProductoN);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }


        // PUT api/<ProductoesControllemofir>
        [HttpPut("{ProductoId}/{CategoriaId}/{ProductoNombre}/{ProductoDescripcion}/{ProductoPrecio}/{ProductoEstado}")]
        public async Task<IActionResult> Put(int ProductoId, int CategoriaId, string ProductoNombre, string ProductoDescripcion, decimal  ProductoPrecio, bool ProductoEstado)
        {
            try
            {

                Producto ProductoExistente = await _repositorioProducto.GetProductoById(ProductoId);
                if (ProductoExistente == null)
                {
                    return NotFound("Producto no encontrado.");
                }

                ProductoExistente.ProductoId = ProductoId;
                ProductoExistente.CategoriaId = CategoriaId;
                ProductoExistente.ProductoNombre = ProductoNombre;
                ProductoExistente.ProductoDescripcion = ProductoDescripcion;
                ProductoExistente.ProductoPrecio = ProductoPrecio;
                ProductoExistente.ProductoEstado = ProductoEstado;
               


                // Modificar el Producto en la base de datos
                var ProductoModificado = await _repositorioProducto.UpdateProducto(ProductoExistente);
                return Ok(ProductoModificado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }


        [HttpDelete("{ProductoId}")]
        public async Task<IActionResult> Delete(int ProductoId)
        {
            try
            {
                await _repositorioProducto.DeleteProducto(ProductoId);
                return NoContent();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.ToString());
            }
        }

    }
}
