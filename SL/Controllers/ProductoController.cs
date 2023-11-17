using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SL.Controllers
{
    [Route("api/Producto")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        [HttpGet]
        [Route("GetProductos/")]
        [Route("GetProductos/{nombre?}")]
        public IActionResult GetAll(string? nombre)
        {
            //if (nombre == "null" || nombre = "") nombre = null;
            ML.Result result = BL.Producto.GetAllProducto(nombre);
            if (result.Correct)
            {
                return StatusCode(200, result);
            }
            else
            {
                return StatusCode(400, result);
            }
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult Add([FromBody]ML.Producto producto)
        {
            ML.Result result = BL.Producto.Add(producto);
            if (result.Correct)
            {
                return StatusCode(200, result);
            }
            else
            {
                return StatusCode(400, result);
            }
        }

        [HttpPut]
        [Route("Update")]
        public IActionResult Update([FromBody]ML.Producto producto)
        {
            ML.Result result = BL.Producto.Update(producto);
            if (result.Correct)
            {
                return StatusCode(200, result);
            }
            else
            {
                return StatusCode(400, result);
            }
        }

        [HttpDelete]
        [Route("Delete/{idProducto}")]
        public IActionResult Delete(int idProducto)
        {
            ML.Result result = BL.Producto.Delete(idProducto);
            if (result.Correct)
            {
                return StatusCode(200, result);
            }
            else
            {
                return StatusCode(400, result);
            }
        }

        [HttpGet]
        [Route("GetBy/{idProducto}")]
        public IActionResult GetById(int idProducto)
        {
            ML.Result result = BL.Producto.GetById(idProducto);
            if (result.Correct)
            {
                return StatusCode(200, result);
            }
            else
            {
                return StatusCode(400, result);
            }
        }
    }
}
