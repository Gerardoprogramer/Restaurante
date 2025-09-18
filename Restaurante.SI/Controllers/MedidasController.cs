using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Restaurante.SI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedidasController : ControllerBase
    {

        private readonly BS.IRepositorioDeRestaurante ElRepositorio;

        public MedidasController(BS.IRepositorioDeRestaurante repositorio)
        {
            ElRepositorio = repositorio;
        }

        // GET: api/<MedidasController>
        [HttpGet("ObtengaLaListaDeMedidas")]
        public IEnumerable<Restaurante.Model.Medidas> ObtengaLaListaDeMedidas()
        {
            List<Model.Medidas> elResultado;
            elResultado = ElRepositorio.ObtengaLaListaDeMedidas();
            return elResultado;
        }


        // GET: api/<MedidasController>
        [HttpGet("ObTengaLasMedidasPorNombre")]
        public IEnumerable<Restaurante.Model.Medidas> ObTengaLasMedidasPorNombre(string nombre)
        {
            List<Model.Medidas> elResultado;
            elResultado = ElRepositorio.ObTengaLasMedidasPorNombre(nombre);
            return elResultado;
        }

        // GET api/<MedidasController>/5
        [HttpGet("ObtenerPorIdLaMedida")]
        public Restaurante.Model.Medidas ObtenerPorIdLaMedida(int id)
        {
            Model.Medidas elResultado;
            elResultado = ElRepositorio.ObtenerPorIdLaMedida(id);
            return elResultado;
        }

        // GET api/<MedidasController>/5
        [HttpGet("ObtengaLaMedidaPorNombre")]
        public Restaurante.Model.Medidas ObtengaLaMedidaPorNombre(string nombre)
        {
            Model.Medidas elResultado;
            elResultado = ElRepositorio.ObTengalaMedida(nombre);
            return elResultado;
        }

        // POST api/<MedidasController>
        [HttpPost]
        public IActionResult Post([FromBody] Restaurante.Model.Medidas medida)
        {
            if (ModelState.IsValid)
            {
                ElRepositorio.AgregueLaMedida(medida);
                return Ok(medida);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // PUT api/<MedidasController>/5
        [HttpPut("Editar")]
        public IActionResult Put([FromBody] Restaurante.Model.Medidas medida)
        {
            if (ModelState.IsValid)
            {
                ElRepositorio.EditarLaMedida(medida);
                return Ok(medida);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // DELETE api/<MedidasController>/5
        [HttpDelete("Deshabilitar")]
        public IActionResult Deshabilitar([FromBody] Restaurante.Model.Medidas medida)
        {

            medida = ElRepositorio.ObtenerPorIdLaMedida(medida.Id);
            ElRepositorio.EditarLaMedida(medida);
            return Ok(medida);
        }
    }
}
