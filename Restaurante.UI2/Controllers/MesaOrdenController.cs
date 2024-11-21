using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace Restaurante.UI.Controllers
{

    [Authorize]
    public class MesaOrdenController : Controller
    {
        // GET: MesaOrdenController
        public async Task<IActionResult> Index()
        {
            List<Model.CatalogoDeOrdenes> laListaDeOrdenesDeMesa;
            try
            {
                var httpClient = new HttpClient();

                var response = await httpClient.GetAsync("https://restaurantesi.azurewebsites.net/api/MesaOrden/ObtengaLaListaDelOrdenDeLasMesas");
                string apiResponse = await response.Content.ReadAsStringAsync();
                laListaDeOrdenesDeMesa = JsonConvert.DeserializeObject<List<Restaurante.Model.CatalogoDeOrdenes>>(apiResponse);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View(laListaDeOrdenesDeMesa);
        }

        // GET: MesaOrdenController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MesaOrdenController/Create
        public async Task<IActionResult> OrdenarMesa(int id)
        {
            Model.OrdenarMesa OrdenarMesa;
            try

            {

                var httpClient = new HttpClient();
                var query = new Dictionary<string, string>()
                {

                    ["id"] = id.ToString()
                };

                var uri = QueryHelpers.AddQueryString("https://restaurantesi.azurewebsites.net/api/MesaOrden/ObtengaElMenuParaOrdenar", query);
                var response = await httpClient.GetAsync(uri);
                string apiResponse = await response.Content.ReadAsStringAsync();
                OrdenarMesa = JsonConvert.DeserializeObject<Restaurante.Model.OrdenarMesa>(apiResponse);
                ViewBag.ListaDeMenus = OrdenarMesa.Menu;
            }
            catch (Exception ex)
            {
                throw ex;

            }

            return View(OrdenarMesa);
        }

        // POST: MesaOrdenController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OrdenarMesa(string idMenu, Model.OrdenarMesa ordenarMesa)
        {
            try
            {

                Model.MesaOrden mesaOrden = new Model.MesaOrden();
                mesaOrden.Id_Menu = int.Parse(idMenu);
                mesaOrden.Id_Mesa = ordenarMesa.id;
                mesaOrden.Cantidad = ordenarMesa.Cantidad;
                mesaOrden.Estado = Model.EstadoMesas.Disponible;




                var httpClient = new HttpClient();

                string json = JsonConvert.SerializeObject(mesaOrden);

                var buffer = System.Text.Encoding.UTF8.GetBytes(json);

                var byteContent = new ByteArrayContent(buffer);

                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                await httpClient.PostAsync("https://restaurantesi.azurewebsites.net/api/MesaOrden/AgregueLaOrdenALaMesa", byteContent);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }



        // GET: MesaOrdenController/Create
        public async Task<IActionResult> ServirMesa(int id)
        {
            Model.ServirOrden ServirOrden;
            try

            {

                var httpClient = new HttpClient();
                var query = new Dictionary<string, string>()
                {

                    ["id"] = id.ToString()
                };

                var uri = QueryHelpers.AddQueryString("https://restaurantesi.azurewebsites.net/api/MesaOrden/FiltrarPorNombreLaListaAServir", query);
                var response = await httpClient.GetAsync(uri);
                string apiResponse = await response.Content.ReadAsStringAsync();
                ServirOrden = JsonConvert.DeserializeObject<Restaurante.Model.ServirOrden>(apiResponse);
                ViewBag.ListaDeMenus = ServirOrden.MenuOrdenado;
            }
            catch (Exception ex)
            {
                throw ex;

            }

            return View(ServirOrden);
        }

        // POST: MesaOrdenController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ServirMesa(string idMenu, Model.OrdenarMesa ordenarMesa)
        {
            try
            {

                Model.MesaOrden mesaOrden = new Model.MesaOrden();
                mesaOrden.Id_Menu = int.Parse(idMenu);
                mesaOrden.Id_Mesa = ordenarMesa.id;


                var httpClient = new HttpClient();

                string json = JsonConvert.SerializeObject(mesaOrden);

                var buffer = System.Text.Encoding.UTF8.GetBytes(json);

                var byteContent = new ByteArrayContent(buffer);

                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                await httpClient.PostAsync("https://restaurantesi.azurewebsites.net/api/MesaOrden/EliminarElPlatilloServido", byteContent);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MesaOrdenController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MesaOrdenController/Delete/5
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
