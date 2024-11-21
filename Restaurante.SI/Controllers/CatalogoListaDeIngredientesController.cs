using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Restaurante.SI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogoListaDeIngredientesController : ControllerBase
    {
        private readonly BS.IRepositorioDeRestaurante ElRepositorio;

        public CatalogoListaDeIngredientesController(BS.IRepositorioDeRestaurante repositorio)
        {
            ElRepositorio = repositorio;
        }
        // GET: api/<CatalogoListaDeIngredientesController>
        [HttpGet("ObTengaElCatalogoDeLaListaDeIngredientes")]
        public IEnumerable<Model.CatalogoListaDeIngredientes> ObTengaElCatalogoDeLaListaDeIngredientes(int id)
        {
            if (ElRepositorio.AgregueALaListaDeIngrediente() != null)
            {
                ElRepositorio.AgregueALaListaDeIngrediente().Clear();
            }
            ElRepositorio.AgregueALaListaDeIngrediente();


            List<Model.CatalogoListaDeIngredientes> elResultado;
            elResultado = ElRepositorio.ObtengaLaListaFiltradaPorId(id);
            return elResultado;
            
        }

        // GET api/<CatalogoListaDeIngredientesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CatalogoListaDeIngredientesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CatalogoListaDeIngredientesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }


    }
}
