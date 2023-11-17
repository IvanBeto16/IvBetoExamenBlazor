using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Expressions;

namespace SL.Controllers
{
    [Route("api/Categoria")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {

        [HttpGet]
        [Route("")]
        public IActionResult GetAllCategorias()
        {
            ML.Result result = BL.Categoria.GetAllCategoria();
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
