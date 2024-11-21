using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Restaurante.UI.Controllers
{
    public class MenuController : Controller
    {
        // GET: MenuController
        public async Task<IActionResult> Index()
        {
            List<Model.Menu> laListaMenu;
            try
            {
                var httpClient = new HttpClient();

                var response = await httpClient.GetAsync("https://localhost:7295/api/Menu/ObtengaLaListaDelMenu");
                string apiResponse = await response.Content.ReadAsStringAsync();
                laListaMenu = JsonConvert.DeserializeObject<List<Restaurante.Model.Menu>>(apiResponse);



            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View(laListaMenu);
        }

        // GET: MenuController
        public async Task<IActionResult> MenuCompleto()
        {
            List<Model.Menu> laListaMenu;
            try
            {
                var httpClient = new HttpClient();

                var response = await httpClient.GetAsync("https://localhost:7295/api/Menu/ObtengaLaListaDelMenu");
                string apiResponse = await response.Content.ReadAsStringAsync();
                laListaMenu = JsonConvert.DeserializeObject<List<Restaurante.Model.Menu>>(apiResponse);



            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View(laListaMenu);
        }

        // GET: MenuController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            Model.Menu elMenu;

            try

            {
                var httpClient = new HttpClient();
                var query = new Dictionary<string, string>()
                {

                    ["id"] = id.ToString()
                };

                var uri = QueryHelpers.AddQueryString("https://localhost:7295/api/Menu/ObtenerPorIdElMenu", query);
                var response = await httpClient.GetAsync(uri);
                string apiResponse = await response.Content.ReadAsStringAsync();
                elMenu = JsonConvert.DeserializeObject<Restaurante.Model.Menu>(apiResponse);
            }

            catch (Exception ex)
            {
                throw ex;

            }

            return View(elMenu);
        }
        // GET: MenuController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MenuController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Model.Platillo menu)
        {
            try
            {

                Restaurante.Model.Menu elMenu = new Model.Menu();
                using (var flujo = new System.IO.MemoryStream())
                {
                    await menu.Imagen.CopyToAsync(flujo);
                    elMenu.Imagen = flujo.ToArray();

                }
                elMenu.Nombre = menu.Nombre;
                elMenu.Categoria = (Model.MenuDeCategorias)menu.Categoria;
                elMenu.Precio = menu.Precio;



                var httpClient = new HttpClient();

                string json = JsonConvert.SerializeObject(elMenu);

                var buffer = System.Text.Encoding.UTF8.GetBytes(json);

                var byteContent = new ByteArrayContent(buffer);

                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                await httpClient.PostAsync("https://localhost:7295/api/Menu/AgregueElMenu", byteContent);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        // GET: MenuController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            Model.Menu elMenu;
            Model.Platillo platillo = new Model.Platillo();

            try

            {
                var httpClient = new HttpClient();

                var query = new Dictionary<string, string>()
                {

                    ["id"] = id.ToString()
                };

                var uri = QueryHelpers.AddQueryString("https://localhost:7295/api/Menu/ObtenerPorIdElMenu", query);
                var response = await httpClient.GetAsync(uri);
                string apiResponse = await response.Content.ReadAsStringAsync();

                elMenu = JsonConvert.DeserializeObject<Restaurante.Model.Menu>(apiResponse);

                platillo.Categoria = elMenu.Categoria;
                platillo.Nombre = elMenu.Nombre;
                platillo.Precio = elMenu.Precio;
                platillo.Id = elMenu.Id;
            }
            catch (Exception ex)
            {
                throw ex;

            }

            return View(platillo);
        }

        // POST: MenuController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Model.Platillo platillo)
        {
            
            try
            {
                Model.Menu elMenu = new Model.Menu();
                using (var flujo = new System.IO.MemoryStream())
                {
                    await platillo.Imagen.CopyToAsync(flujo);
                    elMenu.Imagen = flujo.ToArray();

                }
                elMenu.Nombre = platillo.Nombre;
                elMenu.Precio =platillo.Precio;
                elMenu.Categoria = platillo.Categoria;
                elMenu.Id = platillo.Id;
                


                var httpClient = new HttpClient();

                string json = JsonConvert.SerializeObject(elMenu);

                var buffer = System.Text.Encoding.UTF8.GetBytes(json);

                var byteContent = new ByteArrayContent(buffer);

                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                await httpClient.PutAsync("https://localhost:7295/api/Menu/EditarElMenu", byteContent);


                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MenuController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MenuController/Delete/5
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
