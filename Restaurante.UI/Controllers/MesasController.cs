using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace Restaurante.UI.Controllers
{
    public class MesasController : Controller
    {
        // GET: MesasController
        public async Task<IActionResult> Index()
        {
            List<Model.Mesas> LaListaDeMesas;
            try
            {
                var httpClient = new HttpClient();

                var response = await httpClient.GetAsync("https://localhost:7295/api/Mesas/ObtengaLaListaDeMesas");
                string apiResponse = await response.Content.ReadAsStringAsync();
                LaListaDeMesas = JsonConvert.DeserializeObject<List<Restaurante.Model.Mesas>>(apiResponse);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View(LaListaDeMesas);
        }

        // GET: MesasController/Details/5
        public async Task<IActionResult> Details(int Id)
        {
            Model.Mesas laMesa;

            try

            {
                var httpClient = new HttpClient();
                var query = new Dictionary<string, string>()
                {

                    ["id"] = Id.ToString()
                };

                var uri = QueryHelpers.AddQueryString("https://localhost:7295/api/Mesas/ObtenerPorIdLaMesa", query);
                var response = await httpClient.GetAsync(uri);
                string apiResponse = await response.Content.ReadAsStringAsync();
                laMesa = JsonConvert.DeserializeObject<Restaurante.Model.Mesas>(apiResponse);
                
            }
            catch (Exception ex)
            {
                throw ex;

            }

            return View(laMesa);
        }

        // GET: MesasController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MesasController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Model.Mesas mesa)
        {
            try
            {
                Restaurante.Model.Mesas laMesa = new Model.Mesas();

                laMesa.Id = mesa.Id;
                laMesa.Nombre = mesa.Nombre;
                laMesa.Estado = mesa.Estado;


                var httpClient = new HttpClient();

                string json = JsonConvert.SerializeObject(mesa);

                var buffer = System.Text.Encoding.UTF8.GetBytes(json);

                var byteContent = new ByteArrayContent(buffer);

                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                await httpClient.PostAsync("https://localhost:7295/api/Mesas/AgregarMesa", byteContent);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MesasController/Edit/5
        public async Task<IActionResult> Edit(int Id)
        {
            Model.Mesas laMesa;

            try

            {
                var httpClient = new HttpClient();

                var query = new Dictionary<string, string>()
                {

                    ["Id"] = Id.ToString()
                };

                var uri = QueryHelpers.AddQueryString("https://localhost:7295/api/Mesas/ObtenerPorIdLaMesa", query);
                var response = await httpClient.GetAsync(uri);
                string apiResponse = await response.Content.ReadAsStringAsync();

                laMesa = JsonConvert.DeserializeObject<Restaurante.Model.Mesas>(apiResponse);

            }
            catch (Exception ex)
            {
                throw ex;

            }

            return View(laMesa);
        }

        // POST: MesasController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Model.Mesas mesa)
        {
            try
            {

                var httpClient = new HttpClient();

                string json = JsonConvert.SerializeObject(mesa);

                var buffer = System.Text.Encoding.UTF8.GetBytes(json);

                var byteContent = new ByteArrayContent(buffer);

                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                await httpClient.PutAsync("https://localhost:7295/api/Mesas/EditarlaMesa", byteContent);


                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [Route("ReservarMesa/{id}")]
        public async Task<IActionResult> Deshabilitar(int id)
        {

            try
            {


                var httpClient = new HttpClient();
                string json = JsonConvert.SerializeObject(id);

                var buffer = System.Text.Encoding.UTF8.GetBytes(json);

                var byteContent = new ByteArrayContent(buffer);

                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                HttpResponseMessage httpResponseMessage = await httpClient.PutAsync("https://localhost:7295/api/Mesas/ReservarMesa", byteContent);

            }


            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction(nameof(Index));
        }


        [Route("DisponibilidadDeMesa/{id}")]
        public async Task<IActionResult> Habilitar(int id)
        {

            try
            {


                var httpClient = new HttpClient();
                string json = JsonConvert.SerializeObject(id);

                var buffer = System.Text.Encoding.UTF8.GetBytes(json);

                var byteContent = new ByteArrayContent(buffer);

                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                HttpResponseMessage httpResponseMessage = await httpClient.PutAsync("https://localhost:7295/api/Mesas/DisponibilidadDeMesa", byteContent);

            }


            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction(nameof(Index));
        }
        // POST: MesasController/Delete/5
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
