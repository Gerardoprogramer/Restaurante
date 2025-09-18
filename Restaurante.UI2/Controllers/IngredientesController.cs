using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Restaurante.UI.Controllers
{
    [Authorize]
    public class IngredientesController : Controller
    {

        private readonly string _apiBaseUrl;

        public IngredientesController(IConfiguration configuration)
        {
            _apiBaseUrl = configuration.GetValue<string>("ApiBaseUrl");
        }
        // GET: IngredientesController
        public async Task<IActionResult> Index()
        {
            List<Model.Ingredientes> laListaDeIngredientes;
            try
            {
                var httpClient = new HttpClient();

                var response = await httpClient.GetAsync($"{_apiBaseUrl}/api/Ingredientes/ObtengaLaListaDeIngredientes");
                string apiResponse = await response.Content.ReadAsStringAsync();
                laListaDeIngredientes = JsonConvert.DeserializeObject<List<Restaurante.Model.Ingredientes>>(apiResponse);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View(laListaDeIngredientes);
        }

        // GET: IngredientesController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            Model.DetalleIngrediente elIngrediente;

            try

            {
                var httpClient = new HttpClient();
                var query = new Dictionary<string, string>()
                {

                    ["id"] = id.ToString()
                };

                var uri = QueryHelpers.AddQueryString($"{_apiBaseUrl}/api/Ingredientes/ObtenerPorIdElIngrediente", query);
                var response = await httpClient.GetAsync(uri);
                string apiResponse = await response.Content.ReadAsStringAsync();
                elIngrediente = JsonConvert.DeserializeObject<Restaurante.Model.DetalleIngrediente>(apiResponse);
            }

            catch (Exception ex)
            {
                throw ex;

            }

            return View(elIngrediente);
        }

        // GET: IngredientesController/Create
        public ActionResult Create()
        {
            return View();
        }


        // POST: IngredientesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Model.Ingredientes ingredientes)
        {
            try
            {

                Restaurante.Model.Ingredientes elIngrediente = new Model.Ingredientes();

                elIngrediente.Nombre = ingredientes.Nombre;




                var httpClient = new HttpClient();

                string json = JsonConvert.SerializeObject(ingredientes);

                var buffer = System.Text.Encoding.UTF8.GetBytes(json);

                var byteContent = new ByteArrayContent(buffer);

                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                await httpClient.PostAsync($"{_apiBaseUrl}/api/Ingredientes", byteContent);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: IngredientesController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            Model.Ingredientes elIngrediente;

            try

            {
                var httpClient = new HttpClient();

                var query = new Dictionary<string, string>()
                {

                    ["id"] = id.ToString()
                };

                var uri = QueryHelpers.AddQueryString($"{_apiBaseUrl}/api/Ingredientes/ObtenerPorIdElIngrediente", query);
                var response = await httpClient.GetAsync(uri);
                string apiResponse = await response.Content.ReadAsStringAsync();

                elIngrediente = JsonConvert.DeserializeObject<Restaurante.Model.Ingredientes>(apiResponse);

            }
            catch (Exception ex)
            {
                throw ex;

            }

            return View(elIngrediente);
        }

        // POST: IngredientesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Model.Ingredientes ingredientes)
        {
            try
            {

                var httpClient = new HttpClient();

                string json = JsonConvert.SerializeObject(ingredientes);

                var buffer = System.Text.Encoding.UTF8.GetBytes(json);

                var byteContent = new ByteArrayContent(buffer);

                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                await httpClient.PutAsync($"{_apiBaseUrl}/api/Ingredientes/EditarElIngrediente", byteContent);


                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: IngredientesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: IngredientesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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
    }
}
