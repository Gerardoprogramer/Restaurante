using Azure.Core;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Restaurante.SI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly BS.IRepositorioDeRestaurante ElRepositorio;

        public MenuController(BS.IRepositorioDeRestaurante repositorio)
        {
            ElRepositorio = repositorio;
        }
        // GET: api/<MenuController>
        [HttpGet("ObtengaLaListaDelMenu")]
        public IEnumerable<Restaurante.Model.Menu> ObtengaLaListaDelMenu()
        {
            List<Model.Menu> elResultado;
            elResultado = ElRepositorio.ObtengaLaListaDelMenu();
            return elResultado;
        }

        // GET: api/<MenuController>
        [HttpGet("ObTengaLosMenuPorNombre")]
        public IEnumerable<Restaurante.Model.Menu> ObTengaLosMenuPorNombre(string nombre)
        {
            List<Model.Menu> elResultado;
            elResultado = ElRepositorio.ObTengaLosMenuPorNombre(nombre);
            return elResultado;
        }

        // GET api/<MenuController>/5
        [HttpGet("ObtenerPorIdElMenu")]
        public Restaurante.Model.Menu ObtenerPorIdElMenu(int id)
        {
            Model.Menu elResultado;
            elResultado = ElRepositorio.ObtenerPorIdElMenu(id);
            return elResultado;
        }

        // GET api/<MenuController>/5
        [HttpGet("ObTengaElMenuPorNombre")]
        public Restaurante.Model.Menu ObTengaElMenuPorNombre(string nombre)
        {
            Model.Menu elResultado;
            elResultado = ElRepositorio.ObTengaElMenuPorNombre(nombre);
            return elResultado;
        }

        // POST api/<MenuController>
        [HttpPost("AgregueElMenu")]
        public IActionResult Post([FromBody] Restaurante.Model.Menu menu)
        {
            if (ModelState.IsValid)
            {
                ElRepositorio.AgregueElMenu(menu);
                return Ok(menu);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // PUT api/<MenuController>/5
        [HttpPut("EditarElMenu")]
        public IActionResult Put( [FromBody] Restaurante.Model.Menu menu)
        {
            if (ModelState.IsValid)
            {
                ElRepositorio.EditarElMenu(menu);
                return Ok(menu);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // DELETE api/<MenuController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }


    }
}
