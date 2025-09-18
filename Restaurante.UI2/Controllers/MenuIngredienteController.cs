using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Restaurante.UI.Controllers
{
    [Authorize]
    public class MenuIngredienteController : Controller
    {

        private readonly string _apiBaseUrl;
        public MenuIngredienteController(IConfiguration configuration)
        {
            _apiBaseUrl = configuration.GetValue<string>("ApiBaseUrl");
        }
        // GET: MenuIngredienteController
        public ActionResult Index()
        {
            return View();
        }
        // GET: MenuIngredienteController
        public async Task<IActionResult> IndexDelMenu()
        {
            List<Model.CatalogoDeIngredienteDelMenu> laListaDeIngredientes;
            try
            {
                var httpClient = new HttpClient();

                var response = await httpClient.GetAsync($"{_apiBaseUrl}/api/CatalogoDeIngredientesDelMenu/ObTengaElCatalogoDeLosIngredienteDelMenu");
                string apiResponse = await response.Content.ReadAsStringAsync();
                laListaDeIngredientes = JsonConvert.DeserializeObject<List<Restaurante.Model.CatalogoDeIngredienteDelMenu>>(apiResponse);



            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View(laListaDeIngredientes);
        }


        // GET: CatalogoListaDeIngredientesController
        public async Task<IActionResult> IndexListaIngredientes(int id, string Nombre)
        {
            List<Model.CatalogoListaDeIngredientes> laListaDeIngredientes;
            try
            {
                var httpClient = new HttpClient();

                var query = new Dictionary<string, string>()
                {

                    ["id"] = id.ToString()
                };

                var uri = QueryHelpers.AddQueryString($"{_apiBaseUrl}/api/CatalogoListaDeIngredientes/ObTengaElCatalogoDeLaListaDeIngredientes", query);
                var response = await httpClient.GetAsync(uri);
                string apiResponse = await response.Content.ReadAsStringAsync();
                laListaDeIngredientes = JsonConvert.DeserializeObject<List<Restaurante.Model.CatalogoListaDeIngredientes>>(apiResponse);
                ViewBag.id = id;
                ViewBag.Nombre = Nombre;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View(laListaDeIngredientes);
        }

        // GET: MenuIngredienteController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MenuIngredienteController/Create
        public async Task<IActionResult> Asociar(int id)
        {
            Model.AsociarAlMenu AsociarAlMenu;
            try

            {


                var httpClient = new HttpClient();

                var response = await httpClient.GetAsync($"{_apiBaseUrl}/api/MenuIngrediente/ObtengaElMenu");
                string apiResponse = await response.Content.ReadAsStringAsync();
                AsociarAlMenu = JsonConvert.DeserializeObject<Restaurante.Model.AsociarAlMenu>(apiResponse);
                AsociarAlMenu.idMenu = id;
                ViewBag.ListaDeIngredientes = AsociarAlMenu.Ingrediente;
                ViewBag.ListaDeMedidas = AsociarAlMenu.Medida;
                ViewBag.id = id;
            }
            catch (Exception ex)
            {
                throw ex;

            }

            return View(AsociarAlMenu);
        }

        // POST: MenuIngredienteController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Asociar(string idIngrediente, string idMedidas, Model.AsociarAlMenu laAsociacion)
        {
            try
            {

                Model.MenuIngredientes elMenuDeIngredientes = new Model.MenuIngredientes();
                elMenuDeIngredientes.Id_Menu = laAsociacion.idMenu;
                elMenuDeIngredientes.Id_Ingredientes = int.Parse(idIngrediente);
                elMenuDeIngredientes.Id_Medidas = int.Parse(idMedidas);
                elMenuDeIngredientes.Cantidad = laAsociacion.Cantidad;
                elMenuDeIngredientes.ValorAproximado = laAsociacion.Valor;


                var httpClient = new HttpClient();

                string json = JsonConvert.SerializeObject(elMenuDeIngredientes);

                var buffer = System.Text.Encoding.UTF8.GetBytes(json);

                var byteContent = new ByteArrayContent(buffer);

                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                await httpClient.PostAsync($"{_apiBaseUrl}/api/MenuIngrediente/AgregueAlMenuDeIngredientes", byteContent);

                return RedirectToAction(nameof(IndexDelMenu));
            }
            catch
            {
                return View();
            }
        }

        // GET: MenuIngredienteController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MenuIngredienteController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [Route("Desasociar/{id}")]
        public async Task<IActionResult> Desasociar(int Id)
        {

            try
            {


                var httpClient = new HttpClient();
                string json = JsonConvert.SerializeObject(Id);

                var buffer = System.Text.Encoding.UTF8.GetBytes(json);

                var byteContent = new ByteArrayContent(buffer);

                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                HttpResponseMessage httpResponseMessage = await httpClient.PutAsync($"{_apiBaseUrl}/api/MenuIngrediente/Desasociar", byteContent);

            }


            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction(nameof(IndexDelMenu));
        }
    }
}
