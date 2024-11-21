using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Restaurante.SI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MesasController : ControllerBase
    {
        private readonly BS.IRepositorioDeRestaurante ElRepositorio;

        public MesasController(BS.IRepositorioDeRestaurante repositorio)
        {
            ElRepositorio = repositorio;
        }
        // GET: api/<MesasController>
        [HttpGet("ObtengaLaListaDeMesas")]
        public IEnumerable<Model.Mesas> ObtengaLaListaDeMesas()
        {
            List<Model.Mesas> elResultado;
            elResultado = ElRepositorio.ObtengaLaListaDeMesas();

            return elResultado;
        }

        // GET api/<MesasController>/5
        [HttpGet("ObtenerPorIdLaMesa")]
        public Restaurante.Model.Mesas ObtenerPorIdLaMesa(int id)
        {
            Model.Mesas elResultado;
            elResultado = ElRepositorio.ObtenerPorIdLaMesa(id);
            return elResultado;
        }

        // GET api/<MesasController>/5
        [HttpGet("ObtengaLaMesaPorNombre")]
        public Restaurante.Model.Mesas ObtengaLaMesaPorNombre(string Nombre)
        {
            Model.Mesas elResultado;
            elResultado = ElRepositorio.ObtengaLaMesa(Nombre);
            return elResultado;
        }

        // POST api/<MesasController>
        [HttpPost("AgregarMesa")]
        public IActionResult Post([FromBody] Restaurante.Model.Mesas mesa)
        {
            if (ModelState.IsValid)
            {
                ElRepositorio.AgregueLaMesa(mesa);
                return Ok(mesa);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // PUT api/<MesasController>/5
        [HttpPut("EditarlaMesa")]
        public IActionResult Put([FromBody] Restaurante.Model.Mesas mesa)
        {
            if (ModelState.IsValid)
            {
                ElRepositorio.EditarLaMesa(mesa);
                return Ok(mesa);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // PUT api/<MesasController>/5
        [HttpPut("ReservarMesa")]
        public IActionResult ReservarMesa([FromBody] int id)
        {
            Model.Mesas mesa;
            mesa = ElRepositorio.ObtenerPorIdLaMesa(id);
            mesa.Estado = Model.EstadoMesas.Reservado;
            ElRepositorio.EditarLaMesa(mesa);
            return Ok(mesa);
        }

        // PUT api/<ValuesController>/5
        [HttpPut("DisponibilidadDeMesa")]
        public IActionResult DisponibilidadDeMesa([FromBody] int id)
        {
            Model.Mesas mesa;
            mesa = ElRepositorio.ObtenerPorIdLaMesa(id);
            mesa.Estado = Model.EstadoMesas.Disponible;
            ElRepositorio.EditarLaMesa(mesa);
            return Ok(mesa);
        }

        // DELETE api/<MesasController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
