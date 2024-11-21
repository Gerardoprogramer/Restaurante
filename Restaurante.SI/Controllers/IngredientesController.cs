using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Restaurante.SI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientesController : ControllerBase
    {
        private readonly BS.IRepositorioDeRestaurante ElRepositorio;

        public IngredientesController(BS.IRepositorioDeRestaurante repositorio)
        {
            ElRepositorio = repositorio;
        }

        // GET: api/<IngredientesController>
        [HttpGet("ObtengaLaListaDeIngredientes")]
        public IEnumerable<Model.Ingredientes> ObtengaLaListaDeIngredientes()
        {
            List<Model.Ingredientes> elResultado;
            elResultado = ElRepositorio.ObtengaLaListaDeIngredientes();
            return elResultado;
        }

        // GET: api/<IngredientesController>
        [HttpGet("ObTengaLosIngredientePorNombre")]
        public IEnumerable<Model.Ingredientes> ObTengaLosIngredientePorNombre(string nombre)
        {
            List<Model.Ingredientes> elResultado;
            elResultado = ElRepositorio.ObTengaLosIngredientePorNombre(nombre);
            return elResultado;
        }


        // GET api/<IngredientesController>/5
        [HttpGet("ObtenerPorIdElIngrediente")]
        public Model.DetalleIngrediente ObtenerPorIdElIngrediente(int id)
        {
            Model.DetalleIngrediente elResultado;
            elResultado = ElRepositorio.ObtenerPorIdElIngredienteDetalle(id);
            return elResultado;
        }
        // GET api/<IngredientesController>/5
        [HttpGet("ObTengaElIngredientePorNombre")]
        public Model.Ingredientes ObTengaElIngredientePorNombre(string nombre)
        {
            Model.Ingredientes elResultado;
            elResultado = ElRepositorio.ObTengaElIngredientePorNombre(nombre);
            return elResultado;
        }

        // POST api/<IngredientesController>
        [HttpPost]
        public IActionResult Post([FromBody] Restaurante.Model.Ingredientes ingredientes)
        {
            if (ModelState.IsValid)
            {
                ElRepositorio.AgregueElIngrediente(ingredientes);
                return Ok(ingredientes);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        // PUT api/<IngredientesController>/5
        [HttpPut("EditarElIngrediente")]
        public ActionResult Put([FromBody] Model.Ingredientes ingrediente)
        {
            if (ModelState.IsValid)
            {
                ElRepositorio.EditarElIngrediente(ingrediente);
                return Ok(ingrediente);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // DELETE api/<IngredientesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
