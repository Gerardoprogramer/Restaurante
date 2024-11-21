using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Restaurante.SI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MesaOrdenController : ControllerBase
    {
        private readonly BS.IRepositorioDeRestaurante ElRepositorio;

        public MesaOrdenController(BS.IRepositorioDeRestaurante repositorio)
        {
            ElRepositorio = repositorio;
        }
        // GET: api/<MesaOrdenController>
        [HttpGet("ObtengaLaListaDelOrdenDeLasMesas")]
        public IEnumerable<Model.CatalogoDeOrdenes> ObtengaLaListaDelOrdenDeLasMesas()
        {
            ElRepositorio.AgregueElCatalogoDeOrdenALaMesa().Clear();
            ElRepositorio.AgregueElCatalogoDeOrdenALaMesa();
            List<Model.CatalogoDeOrdenes> elResultado;
            elResultado = ElRepositorio.ObtengaLaListaDelCatalogoOrdenDeLasMesas();

            return elResultado;
        }


        // GET api/<MesaOrdenController>/5
        [HttpGet("ObtengaElMenuParaOrdenar")]
        public Model.OrdenarMesa ObtengaElMenuParaOrdenar(int id)
        {
            Model.OrdenarMesa OrdenarMesa;
            OrdenarMesa = ElRepositorio.AgregueUnaOrdenAlaMesa(id);

            return OrdenarMesa;
        }

        // GET api/<MesaOrdenController>/5
        [HttpGet("FiltrarPorNombreLaListaAServir")]
        public Model.ServirOrden FiltrarPorNombreLaListaAServir(int id)
        {
            Model.ServirOrden OrdenarMesa;
            OrdenarMesa = ElRepositorio.FiltrarPorNombreLaListaAServir(id);

            return OrdenarMesa;
        }

        // POST api/<MesaOrdenController>
        [HttpPost("AgregueLaOrdenALaMesa")]
        public IActionResult Post([FromBody] Model.MesaOrden mesaOrden)
        {
            if (ModelState.IsValid)
            {
                Model.CatalogoDeOrdenes elCatalogo;
                elCatalogo = ElRepositorio.ObtenerPorIdElCatalogoOrdenDeLaMesa(mesaOrden.Id_Mesa);
                elCatalogo.Estado = Model.EstadoMesas.Ocupada;
                ElRepositorio.AgregueLaOrdenALaMesa(mesaOrden);
                return Ok(mesaOrden);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // POST api/<MesaOrdenController>
        [HttpPost("EliminarElPlatilloServido")]
        public IActionResult EliminarElPlatilloServido([FromBody] Model.MesaOrden mesaOrden)
        {
            if (ModelState.IsValid)
            {
                ElRepositorio.EliminarElPlatilloServido(mesaOrden.Id_Mesa, mesaOrden.Id_Menu);
                return Ok(mesaOrden);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }


        // DELETE api/<MesaOrdenController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
