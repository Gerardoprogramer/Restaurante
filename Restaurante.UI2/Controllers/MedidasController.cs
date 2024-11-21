using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Restaurante.UI.Controllers
{
    [Authorize]
    public class MedidasController : Controller
    {
        // GET: MedidasController
        public async Task<IActionResult> Index(string nombre)
        {
            List<Model.Medidas> laListaDeMedidas;
            Model.Medidas laMedida;
            try
            {
                var httpClient = new HttpClient();
                if (nombre is null)
                {
                    var Taza = new Dictionary<string, string>()
                    {

                        ["nombre"] = Model.CargaDeDatosMedidas.Taza.ToString()
                    };
                    var urlTaza = QueryHelpers.AddQueryString("https://restaurantesi.azurewebsites.net/api/Medidas/ObtengaLaMedidaPorNombre", Taza);
                    var responderTaza = await httpClient.GetAsync(urlTaza);
                    string apiResponderTaza = await responderTaza.Content.ReadAsStringAsync();
                    laMedida = JsonConvert.DeserializeObject<Restaurante.Model.Medidas>(apiResponderTaza);

                    if (laMedida == null)
                    {
                        Model.Medidas Medida = new Model.Medidas();
                        Medida.Nombre = Model.CargaDeDatosMedidas.Taza.ToString();

                        Create(Medida);
                    }

                    var Pizca = new Dictionary<string, string>()
                    {

                        ["nombre"] = Model.CargaDeDatosMedidas.Pizca.ToString()
                    };
                    var urlPizca = QueryHelpers.AddQueryString("https://restaurantesi.azurewebsites.net/api/Medidas/ObtengaLaMedidaPorNombre", Pizca);
                    var responderPizca = await httpClient.GetAsync(urlPizca);
                    string apiResponderPizca = await responderPizca.Content.ReadAsStringAsync();
                    laMedida = JsonConvert.DeserializeObject<Restaurante.Model.Medidas>(apiResponderPizca);

                    if (laMedida == null)
                    {
                        Model.Medidas Medida = new Model.Medidas();
                        Medida.Nombre = Model.CargaDeDatosMedidas.Pizca.ToString();

                        Create(Medida);
                    }

                    var Cuchara = new Dictionary<string, string>()
                    {

                        ["nombre"] = Model.CargaDeDatosMedidas.Cuchara.ToString()
                    };
                    var urlCuchara = QueryHelpers.AddQueryString("https://restaurantesi.azurewebsites.net/api/Medidas/ObtengaLaMedidaPorNombre", Cuchara);
                    var responderCuchara = await httpClient.GetAsync(urlCuchara);
                    string apiResponderCuchara = await responderCuchara.Content.ReadAsStringAsync();
                    laMedida = JsonConvert.DeserializeObject<Restaurante.Model.Medidas>(apiResponderCuchara);

                    if (laMedida == null)
                    {
                        Model.Medidas Medida = new Model.Medidas();
                        Medida.Nombre = Model.CargaDeDatosMedidas.Cuchara.ToString();

                        Create(Medida);
                    }

                    var Litro = new Dictionary<string, string>()
                    {

                        ["nombre"] = Model.CargaDeDatosMedidas.Litro.ToString()
                    };
                    var urlLitro = QueryHelpers.AddQueryString("https://restaurantesi.azurewebsites.net/api/Medidas/ObtengaLaMedidaPorNombre", Litro);
                    var responderLitro = await httpClient.GetAsync(urlLitro);
                    string apiResponderLitro = await responderLitro.Content.ReadAsStringAsync();
                    laMedida = JsonConvert.DeserializeObject<Restaurante.Model.Medidas>(apiResponderLitro);

                    if (laMedida == null)
                    {
                        Model.Medidas Medida = new Model.Medidas();
                        Medida.Nombre = Model.CargaDeDatosMedidas.Litro.ToString();

                        Create(Medida);
                    }

                    var Mililitros = new Dictionary<string, string>()
                    {

                        ["nombre"] = Model.CargaDeDatosMedidas.Mililitros.ToString()
                    };
                    var urlMililitros = QueryHelpers.AddQueryString("https://restaurantesi.azurewebsites.net/api/Medidas/ObtengaLaMedidaPorNombre", Mililitros);
                    var responderMililitros = await httpClient.GetAsync(urlMililitros);
                    string apiResponderMililitros = await responderMililitros.Content.ReadAsStringAsync();
                    laMedida = JsonConvert.DeserializeObject<Restaurante.Model.Medidas>(apiResponderMililitros);

                    if (laMedida == null)
                    {
                        Model.Medidas Medida = new Model.Medidas();
                        Medida.Nombre = Model.CargaDeDatosMedidas.Mililitros.ToString();

                        Create(Medida);
                    }

                    var response = await httpClient.GetAsync("https://restaurantesi.azurewebsites.net/api/Medidas/ObtengaLaListaDeMedidas");
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    laListaDeMedidas = JsonConvert.DeserializeObject<List<Restaurante.Model.Medidas>>(apiResponse);
                }
                else
                {
                    var query = new Dictionary<string, string>()
                    {

                        ["nombre"] = nombre
                    };

                    var uri = QueryHelpers.AddQueryString("https://restaurantesi.azurewebsites.net/api/Medidas/ObTengaLasMedidasPorNombre", query);
                    var response = await httpClient.GetAsync(uri);
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    laListaDeMedidas = JsonConvert.DeserializeObject<List<Restaurante.Model.Medidas>>(apiResponse);

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View(laListaDeMedidas);
        }

        // GET: MedidasController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            Model.Medidas laMedida;

            try

            {
                var httpClient = new HttpClient();
                var query = new Dictionary<string, string>()
                {

                    ["id"] = id.ToString()
                };

                var uri = QueryHelpers.AddQueryString("https://restaurantesi.azurewebsites.net/api/Medidas/ObtenerPorIdLaMedida", query);
                var response = await httpClient.GetAsync(uri);
                string apiResponse = await response.Content.ReadAsStringAsync();
                laMedida = JsonConvert.DeserializeObject<Restaurante.Model.Medidas>(apiResponse);
            }

            catch (Exception ex)
            {
                throw ex;

            }

            return View(laMedida);
        }

        // GET: MedidasController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MedidasController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Model.Medidas medida)
        {
            try
            {

                Restaurante.Model.Medidas laMedida = new Model.Medidas();

                laMedida.Nombre = medida.Nombre;



                var httpClient = new HttpClient();

                string json = JsonConvert.SerializeObject(medida);

                var buffer = System.Text.Encoding.UTF8.GetBytes(json);

                var byteContent = new ByteArrayContent(buffer);

                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                await httpClient.PostAsync("https://restaurantesi.azurewebsites.net/api/Medidas", byteContent);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MedidasController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            Model.Medidas laMedida;

            try

            {
                var httpClient = new HttpClient();

                var query = new Dictionary<string, string>()
                {

                    ["id"] = id.ToString()
                };

                var uri = QueryHelpers.AddQueryString("https://restaurantesi.azurewebsites.net/api/Medidas/ObtenerPorIdLaMedida", query);
                var response = await httpClient.GetAsync(uri);
                string apiResponse = await response.Content.ReadAsStringAsync();

                laMedida = JsonConvert.DeserializeObject<Restaurante.Model.Medidas>(apiResponse);

            }
            catch (Exception ex)
            {
                throw ex;

            }

            return View(laMedida);
        }

        // POST: MedidasController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Model.Medidas medida)
        {
            try
            {

                var httpClient = new HttpClient();

                string json = JsonConvert.SerializeObject(medida);

                var buffer = System.Text.Encoding.UTF8.GetBytes(json);

                var byteContent = new ByteArrayContent(buffer);

                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                await httpClient.PutAsync("https://restaurantesi.azurewebsites.net/api/Medidas/Editar", byteContent);


                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MedidasController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MedidasController/Delete/5
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
