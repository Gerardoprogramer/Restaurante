using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Restaurante.SI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuIngredienteController : ControllerBase
    {
        private readonly BS.IRepositorioDeRestaurante ElRepositorio;

        public MenuIngredienteController(BS.IRepositorioDeRestaurante repositorio)
        {
            ElRepositorio = repositorio;
        }
        // GET: api/<MenuIngredienteController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<MenuIngredienteController>/5
        [HttpGet("ObtengaElMenu")]
        public Model.AsociarAlMenu ObtengaElMenu()
        {
            Model.AsociarAlMenu menu = new Model.AsociarAlMenu();
            menu.Medida = ElRepositorio.ObtengaLaListaDeMedidas();
            menu.Ingrediente = ElRepositorio.ObtengaLaListaDeIngredientes();
            
            return menu;
        }

        // POST api/<MenuIngredienteController>
        [HttpPost("AgregueAlMenuDeIngredientes")]
        public IActionResult Post([FromBody] Model.MenuIngredientes menuIngredientes)
        {
            if (ModelState.IsValid)
            {
                ElRepositorio.AgregueAlMenuDeIngredientes(menuIngredientes);
                return Ok(menuIngredientes);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // PUT api/<MenuIngredienteController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<MenuIngredienteController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        // PUT api/<ValuesController>/5
        [HttpPut("Desasociar")]
        public IActionResult DisponibilidadDeMesa([FromBody] int Id)
        {
            ElRepositorio.DesasociarMenuDeIngrediente(Id);
            return Ok(Id);
        }
    }
}
