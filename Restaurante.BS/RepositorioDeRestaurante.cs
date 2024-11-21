using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Restaurante.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurante.BS
{
    public class RepositorioDeRestaurante : IRepositorioDeRestaurante
    {
        private Restaurante.DA.DbContexto ElContextoBD;
        public IMemoryCache ElCache;


        public RepositorioDeRestaurante(Restaurante.DA.DbContexto contexto, IMemoryCache Cache)
        {
            ElContextoBD = contexto;
            ElCache = Cache;
        }


        public List<Model.CatalogoListaDeIngredientes> AgregueALaListaDeIngrediente()
        {
            List<Model.CatalogoListaDeIngredientes> elCatalogoDeIngredientes = null;
            List<Model.MenuIngredientes> elMenuDeIngredientes;
       
            elMenuDeIngredientes = ObtengaLaListaDeIngredientesDelMenu();

            foreach (var item in elMenuDeIngredientes)
            {

                Model.CatalogoListaDeIngredientes elCatalogo;
                elCatalogo = ObtenerPorIdElCatalogoDeListaDeIngredientes(item.Id_Menu);


                Model.Medidas laMedida;
                Model.Ingredientes elIngrediente;
                elCatalogoDeIngredientes = ObTengaElCatalogoDeLaListaDeIngredientes();
                elIngrediente = ObtenerPorIdElIngrediente(item.Id_Ingredientes);
                laMedida = ObtenerPorIdLaMedida(item.Id_Medidas);
                


                Model.CatalogoListaDeIngredientes IngredienteDelCatalogo = new CatalogoListaDeIngredientes();
                IngredienteDelCatalogo.id = item.Id;
                IngredienteDelCatalogo.Id_MenuIngrediente = item.Id_Menu;
                IngredienteDelCatalogo.Id_Ingrediente = item.Id_Ingredientes;
                IngredienteDelCatalogo.NombreDelIngrediente = elIngrediente.Nombre;
                IngredienteDelCatalogo.Id_Medida = item.Id_Medidas;
                IngredienteDelCatalogo.NombreDeLaMedida = laMedida.Nombre;
                IngredienteDelCatalogo.ValorAproximado = item.ValorAproximado;

                 elCatalogoDeIngredientes.Add(IngredienteDelCatalogo);

            }
            
            return elCatalogoDeIngredientes;
        }

        public void AgregueAlMenuDeIngredientes(MenuIngredientes menuIngrediente)
        {

                ElContextoBD.MenuIngredientes.Add(menuIngrediente);
                ElContextoBD.SaveChanges();
            
        }

        public void AgregueLaMedida(Medidas medida)
        {
            Model.Medidas laMedida;
            laMedida = ObTengalaMedida(medida.Nombre);

            if (laMedida == null)
            {
                ElContextoBD.Medidas.Add(medida);
                ElContextoBD.SaveChanges();
            }
        }



        public void EditarLaMedida(Medidas medida)
        {
            Model.Medidas laMedidaAModificar;

            laMedidaAModificar = ObtenerPorIdLaMedida(medida.Id);

            laMedidaAModificar.Nombre = medida.Nombre;

            ElContextoBD.Medidas.Update(laMedidaAModificar);
            ElContextoBD.SaveChanges();
        }

        public MenuIngredientes ObtenerPorIdElIngredienteDelMenu(int Id)
        {
            Model.MenuIngredientes resultado;

            resultado = ElContextoBD.MenuIngredientes.Find(Id);

            return resultado;
        }

        public Medidas ObtenerPorIdLaMedida(int Id)
        {
            Model.Medidas resultado;

            resultado = ElContextoBD.Medidas.Find(Id);

            return resultado;
        }

        public List<CatalogoListaDeIngredientes> ObTengaElCatalogoDeLaListaDeIngredientes()
        {
            List<Model.CatalogoListaDeIngredientes> resultado;

            if (ElCache.Get("CatalogoListaDeIngredientes") is null)
            {
                resultado = new List<Model.CatalogoListaDeIngredientes>();
                ElCache.Set("CatalogoListaDeIngredientes", resultado);
            }
            else
            {
                resultado = (List<Model.CatalogoListaDeIngredientes>)ElCache.Get("CatalogoListaDeIngredientes");
            }

            return resultado;
        }

        public List<CatalogoDeIngredienteDelMenu> ObTengaElCatalogoDeLosIngredienteDelMenu()
        {
            List<Model.CatalogoDeIngredienteDelMenu> resultado;

            if (ElCache.Get("CatalogoDeIngredienteDelMenu") is null)
            {
                resultado = new List<Model.CatalogoDeIngredienteDelMenu>();
                ElCache.Set("CatalogoDeIngredienteDelMenu", resultado);
            }
            else
            {
                resultado = (List<Model.CatalogoDeIngredienteDelMenu>)ElCache.Get("CatalogoDeIngredienteDelMenu");
            }

            return resultado;
        }

        public List<MenuIngredientes> ObtengaLaListaDeIngredientesDelMenu()
        {
            var resultado = from c in ElContextoBD.MenuIngredientes
                            select c;
            return resultado.ToList();
        }

        public List<Medidas> ObtengaLaListaDeMedidas()
        {
            var resultado = from c in ElContextoBD.Medidas
                            select c;
            return resultado.ToList();
        }

        public Medidas ObTengalaMedida(string Nombre)
        {
            Model.Medidas resultado = null;
            List<Model.Medidas> laLista;

            laLista = ObtengaLaListaDeMedidas();

            foreach (Model.Medidas item in laLista)
            {
                if (item.Nombre == Nombre)
                    resultado = item;
            }

            return resultado;
        }

        public List<Medidas> ObTengaLasMedidasPorNombre(string nombre)
        {
            List<Model.Medidas> laLista;
            List<Model.Medidas> laListaFiltrada;

            laLista = ObtengaLaListaDeMedidas();

            laListaFiltrada = laLista.Where(x => x.Nombre.Contains(nombre)).ToList();
            return laListaFiltrada;
        }

        public List<Ingredientes> ObtengaLaListaDeIngredientes()
        {
            var resultado = from c in ElContextoBD.Ingredientes
                            select c;
            return resultado.ToList();
        }

        public Ingredientes ObTengaElIngredientePorNombre(string Nombre)
        {
            Model.Ingredientes resultado = null;
            List<Model.Ingredientes> laLista;

            laLista = ObtengaLaListaDeIngredientes();

            foreach (Model.Ingredientes item in laLista)
            {
                if (item.Nombre == Nombre)
                    resultado = item;
            }

            return resultado;
        }



        public DetalleIngrediente ObtenerPorIdElIngredienteDetalle(int Id)
        {
            Model.Ingredientes resultado;
            Model.DetalleIngrediente Detalle= new DetalleIngrediente();

            resultado = ElContextoBD.Ingredientes.Find(Id);
            

            List<Model.MenuIngredientes> elMenu;
            List<Model.MenuIngredientes> losIngredientesDelMenu = new List<MenuIngredientes>();
            List<Model.PlatillosDelIngrediente> laLista = new List<PlatillosDelIngrediente>();
            elMenu = ObtengaLaListaDeIngredientesDelMenu();
            
            Model.Menu menu;
            Model.Medidas medida;

            foreach (Model.MenuIngredientes item in elMenu) 
            {
                if(item.Id_Ingredientes == Id)
                {
                    losIngredientesDelMenu.Add(item);
                }
            }

            foreach (var item in losIngredientesDelMenu)
            {
                menu = ObtenerPorIdElMenu(item.Id_Menu);
                medida = ObtenerPorIdLaMedida(item.Id_Medidas);
                Model.PlatillosDelIngrediente elPlatillo = new PlatillosDelIngrediente();
                elPlatillo.NombrePlatillo = menu.Nombre;
                elPlatillo.NombreMedida = medida.Nombre;
                elPlatillo.Cantidad = item.Cantidad;
                laLista.Add(elPlatillo);
            }
            Detalle.Nombre = resultado.Nombre;
            Detalle.Id = resultado.Id;
            Detalle.losPlatillos = laLista;

            return Detalle;
        }

        public void AgregueElIngrediente(Ingredientes ingredientes)
        {

            ElContextoBD.Ingredientes.Add(ingredientes);
            ElContextoBD.SaveChanges();
        }

        public void EditarElIngrediente(Ingredientes ingrediente)
        {
            Model.Ingredientes elIngredientesAModificar;

            elIngredientesAModificar = ObtenerPorIdElIngrediente(ingrediente.Id);

            elIngredientesAModificar.Nombre = ingrediente.Nombre;

            ElContextoBD.Ingredientes.Update(elIngredientesAModificar);
            ElContextoBD.SaveChanges();
        }

        public List<Ingredientes> ObTengaLosIngredientePorNombre(string nombre)
        {
            List<Model.Ingredientes> laLista;
            List<Model.Ingredientes> laListaFiltrada;

            laLista = ObtengaLaListaDeIngredientes();

            laListaFiltrada = laLista.Where(x => x.Nombre.Contains(nombre)).ToList();
            return laListaFiltrada;
        }

        public List<Menu> ObtengaLaListaDelMenu()
        {
            var resultado = from c in ElContextoBD.Menu
                            select c;
            return resultado.ToList();
        }

        public Menu ObTengaElMenuPorNombre(string Nombre)
        {
            Model.Menu resultado = null;
            List<Model.Menu> laLista;

            laLista = ObtengaLaListaDelMenu();

            foreach (Model.Menu item in laLista)
            {
                if (item.Nombre == Nombre)
                    resultado = item;
            }

            return resultado;
        }

        public List<Menu> ObTengaLosMenuPorNombre(string nombre)
        {
            List<Model.Menu> laLista;
            List<Model.Menu> laListaFiltrada;

            laLista = ObtengaLaListaDelMenu();

            laListaFiltrada = laLista.Where(x => x.Nombre.Contains(nombre)).ToList();
            return laListaFiltrada;
        }

        public Menu ObtenerPorIdElMenu(int Id)
        {
            Model.Menu resultado;

            resultado = ElContextoBD.Menu.Find(Id);

            return resultado;
        }

        public void AgregueElMenu(Menu menu)
        {
            Model.Menu elMenu;
            elMenu = ObTengaElMenuPorNombre(menu.Nombre);

            if (elMenu == null)
            {
                ElContextoBD.Menu.Add(menu);
                ElContextoBD.SaveChanges();
            }
        }

        public void EditarElMenu(Menu menu)
        {
            Model.Menu elMenuAModificar;

            elMenuAModificar = ObtenerPorIdElMenu(menu.Id);

            elMenuAModificar.Nombre = menu.Nombre;
            elMenuAModificar.Categoria = (Model.MenuDeCategorias)menu.Categoria;
            elMenuAModificar.Precio = menu.Precio;
            elMenuAModificar.Imagen = menu.Imagen;

            ElContextoBD.Menu.Update(elMenuAModificar);
            ElContextoBD.SaveChanges();
        }




        public List<Model.CatalogoDeIngredienteDelMenu> AgregueACatalogoIngredienteDelMenu()
        {
            List<Model.Menu> elMenu;
            List<Model.CatalogoDeIngredienteDelMenu> laLista = null;
            List<Model.MenuIngredientes> elMenuDeIngredientes;

            elMenu = ObtengaLaListaDelMenu();
            elMenuDeIngredientes = ObtengaLaListaDeIngredientesDelMenu();

            foreach (var item in elMenu)
            {

                    laLista = ObTengaElCatalogoDeLosIngredienteDelMenu();
                    CatalogoDeIngredienteDelMenu elPlatillo = new CatalogoDeIngredienteDelMenu();
                    elPlatillo.Nombre = item.Nombre;
                    elPlatillo.Id_MenuIngrediente = item.Id;
                    elPlatillo.Precio = item.Precio;
                    int ValorAProximado = 0;
                    foreach (var item2 in elMenuDeIngredientes)
                    {
                        if (item2.Id_Menu == item.Id)
                            ValorAProximado = ValorAProximado + item2.ValorAproximado;
                    }

                    elPlatillo.GananciasAproximadas = ValorAProximado;
                    laLista.Add(elPlatillo);
                


            }
            return laLista;
        }

        public CatalogoDeIngredienteDelMenu ObtenerPorIdElCatalogoDeIngredientesDelMenu(int Id)
        {
            Model.CatalogoDeIngredienteDelMenu resultado = null;
            List<Model.CatalogoDeIngredienteDelMenu> laLista;

            laLista = ObTengaElCatalogoDeLosIngredienteDelMenu();

            foreach (var item in laLista)
            {
                if (item.Id_MenuIngrediente == Id)
                    resultado = item;
            }

            return resultado;
        }

        public CatalogoListaDeIngredientes ObtenerPorIdElCatalogoDeListaDeIngredientes(int Id)
        {
            Model.CatalogoListaDeIngredientes resultado = null;
            List<Model.CatalogoListaDeIngredientes> laLista;

            laLista = ObTengaElCatalogoDeLaListaDeIngredientes();

            foreach (var item in laLista)
            {
                if (item.Id_MenuIngrediente == Id)
                    resultado = item;
            }

            return resultado;
        }

        public List<CatalogoListaDeIngredientes> ObtengaLaListaFiltradaPorId(int id)
        {
            List<Model.CatalogoListaDeIngredientes> laLista;
            List<Model.CatalogoListaDeIngredientes> laListaFiltrada;

            laLista = ObTengaElCatalogoDeLaListaDeIngredientes();
            laListaFiltrada = laLista.Where(x => x.Id_MenuIngrediente.ToString().Contains(id.ToString())).ToList();
            return laListaFiltrada;
        }

        public List<Mesas> ObtengaLaListaDeMesas()
        {
            var resultado = from c in ElContextoBD.Mesas
                            select c;
            return resultado.ToList();
        }

        public Mesas ObtenerPorIdLaMesa(int id)
        {
            Model.Mesas resultado;

            resultado = ElContextoBD.Mesas.Find(id);

            return resultado;
        }

        public void AgregueLaMesa(Mesas mesa)
        {
            Model.Mesas lasMesas;
            lasMesas = ObtenerPorIdLaMesa(mesa.Id);

            if (lasMesas == null)
            {
                mesa.Estado = Model.EstadoMesas.Disponible;
                ElContextoBD.Mesas.Add(mesa);
                ElContextoBD.SaveChanges();
            }
        }

        public Mesas ObtengaLaMesa(string nombre)
        {
            Model.Mesas resultado = null;
            List<Model.Mesas> laLista;

            laLista = ObtengaLaListaDeMesas();

            foreach (var item in laLista)
            {
                if (item.Nombre == nombre)
                    resultado = item;
            }

            return resultado;
        }

        public void EditarLaMesa(Mesas mesa)
        {
            Model.Mesas laMesaAModificar;

            laMesaAModificar = ObtenerPorIdLaMesa(mesa.Id);

            laMesaAModificar.Nombre = mesa.Nombre;

            ElContextoBD.Mesas.Update(laMesaAModificar);
            ElContextoBD.SaveChanges();
        }

        public CatalogoListaDeIngredientes ObTengaCatalogoPorNombre(string Nombre)
        {
            Model.CatalogoListaDeIngredientes resultado = null;
            List<Model.CatalogoListaDeIngredientes> laLista;

            laLista = ObTengaElCatalogoDeLaListaDeIngredientes();

            foreach (Model.CatalogoListaDeIngredientes item in laLista)
            {
                if (item.NombreDelIngrediente == Nombre)
                    resultado = item;
            }

            return resultado;
        }

        public List<MesaOrden> ObtengaLaListaDelOrdenDeLasMesas()
        {
            var resultado = from c in ElContextoBD.MesaOrden
                            select c;
            return resultado.ToList();
        }

        public MesaOrden ObtenerPorIdLaOrdenDeLaMesa(int id)
        {
            Model.MesaOrden resultado;

            resultado = ElContextoBD.MesaOrden.Find(id);

            return resultado;
        }

        public void AgregueLaOrdenALaMesa(MesaOrden mesa)
        {

            Model.MesaOrden lasMesas;
            lasMesas = ObtenerPorIdLaOrdenDeLaMesa(mesa.Id);

            if (lasMesas == null)
            {
                ElContextoBD.MesaOrden.Add(mesa);
                ElContextoBD.SaveChanges();
            }
            
        }


        public void EditarLaMesa(MesaOrden mesa)
        {
            Model.MesaOrden laMesaAModificar;

            laMesaAModificar = ObtenerPorIdLaOrdenDeLaMesa(mesa.Id);

            laMesaAModificar.Estado = mesa.Estado;

            ElContextoBD.MesaOrden.Update(laMesaAModificar);
            ElContextoBD.SaveChanges();
        }

        public void DesasociarMenuDeIngrediente(int id)
        {
            Model.MenuIngredientes elMenuDeIngredientes;
            elMenuDeIngredientes = ObtenerPorIdElIngredienteDelMenu(id);
            ElContextoBD.MenuIngredientes.Remove(elMenuDeIngredientes);
            ElContextoBD.SaveChanges();
        }

        public List<CatalogoDeOrdenes> ObtengaLaListaDelCatalogoOrdenDeLasMesas()
        {
            List<Model.CatalogoDeOrdenes> resultado;

            if (ElCache.Get("CatalogoDeOrdenes") is null)
            {
                resultado = new List<Model.CatalogoDeOrdenes>();
                ElCache.Set("CatalogoDeOrdenes", resultado);
            }
            else
            {
                resultado = (List<Model.CatalogoDeOrdenes>)ElCache.Get("CatalogoDeOrdenes");
            }

            return resultado;
        }

        public CatalogoDeOrdenes ObtenerPorIdElCatalogoOrdenDeLaMesa(int id)
        {
            Model.CatalogoDeOrdenes resultado = null;
            List<Model.CatalogoDeOrdenes> laLista;

            laLista = ObtengaLaListaDelCatalogoOrdenDeLasMesas();

            foreach (var item in laLista)
            {
                if (item.id == id)
                    resultado = item;
            }

            return resultado;
        }


        public List<CatalogoDeOrdenes> AgregueElCatalogoDeOrdenALaMesa()
        {
            List<Model.Mesas> laListaDeMesas;
            List<Model.CatalogoDeOrdenes> laListaDeCatalogoDeOrdenes;

            laListaDeMesas = ObtengaLaListaDeMesas();
            laListaDeCatalogoDeOrdenes = ObtengaLaListaDelCatalogoOrdenDeLasMesas();

            foreach (var item in laListaDeMesas)
            {
                Model.CatalogoDeOrdenes elCatalogo = new CatalogoDeOrdenes();
                elCatalogo.Nombre = item.Nombre;
                elCatalogo.id = item.Id;


                if (item.Estado == Model.EstadoMesas.Reservado)
                {
                    elCatalogo.Estado = Model.EstadoMesas.Reservado;
                }
                if (item.Estado == Model.EstadoMesas.Disponible)
                {
                    elCatalogo.Estado = Model.EstadoMesas.sinEstado;
                }

                foreach (var item2 in ObtengaLaListaDelOrdenDeLasMesas())
                {
                    if (item2.Id_Mesa == item.Id)
                    {
                        elCatalogo.Estado = Model.EstadoMesas.Ocupada;
                        break;
                    }
                }

                    laListaDeCatalogoDeOrdenes.Add(elCatalogo);
            }

            return laListaDeCatalogoDeOrdenes;

        }

        public List<OrdenarMesa> ObtengaLaListaDeOrdenesDeLaMesa()
        {
            List<Model.OrdenarMesa> resultado;

            if (ElCache.Get("OrdenarMesa") is null)
            {
                resultado = new List<Model.OrdenarMesa>();
                ElCache.Set("OrdenarMesa", resultado);
            }
            else
            {
                resultado = (List<Model.OrdenarMesa>)ElCache.Get("OrdenarMesa");
            }

            return resultado;
        }

        public OrdenarMesa ObtengaPorIdLaOrdenDeLaMesa(int id)
        {
            Model.OrdenarMesa resultado = null;
            List<Model.OrdenarMesa> laLista;

            laLista = ObtengaLaListaDeOrdenesDeLaMesa();

            foreach (var item in laLista)
            {
                if (item.id == id)
                    resultado = item;
            }

            return resultado;
        }

        public OrdenarMesa AgregueUnaOrdenAlaMesa(int id)
        {
            List<Model.Menu> laListaDeMenu;
            Model.OrdenarMesa OrdenesDeMesa = new OrdenarMesa();

            OrdenesDeMesa.Menu = ObtengaLaListaDelMenu();
            OrdenesDeMesa.id = id;

            return OrdenesDeMesa;
        }

        public List<ServirOrden> ObtengaLaListaDePlatillosAServir()
        {
            List<Model.ServirOrden> resultado;

            if (ElCache.Get("ServirOrden") is null)
            {
                resultado = new List<Model.ServirOrden>();
                ElCache.Set("ServirOrden", resultado);
            }
            else
            {
                resultado = (List<Model.ServirOrden>)ElCache.Get("ServirOrden");
            }

            return resultado;
        }



        public ServirOrden FiltrarPorNombreLaListaAServir(int id)
        {

            Model.ServirOrden servirOrden = new Model.ServirOrden();
            List<MesaOrden> laMesa;
            List<MesaOrden> laLista = new List<MesaOrden>();
            List<Menu> elListaMenu = new List<Menu>();
            laMesa = ObtengaLaListaDelOrdenDeLasMesas();

            foreach (var item in laMesa)
            {
                if(item.Id_Mesa == id)
                {
                    laLista.Add(item);
                }
                servirOrden.id = item.Id;
            }
            servirOrden.MesaOrden = laLista;
            foreach (var item in servirOrden.MesaOrden)
            {
                Model.Menu elMenu;
                elMenu = ObtenerPorIdElMenu(item.Id_Menu);
                if (elMenu != null)
                {
                    elListaMenu.Add(elMenu);
                }
            }
            servirOrden.MenuOrdenado = elListaMenu;

            return servirOrden;

        }

        public void EliminarElPlatilloServido(int idMesa, int idMenu)
        {
            List<Model.MesaOrden> mesaOrden;
            mesaOrden = ObtengaLaListaDelOrdenDeLasMesas();
            Model.MesaOrden laMesa = null;

            foreach (var item in mesaOrden)
            {
                if(item.Id_Mesa == idMesa && item.Id_Menu == idMenu)
                {
                    laMesa = item;
                }
            }

            if (laMesa != null)
            {
                ElContextoBD.MesaOrden.Remove(laMesa);
                ElContextoBD.SaveChanges();
            }
        }

        public Ingredientes ObtenerPorIdElIngrediente(int Id)
        {
            Model.Ingredientes resultado;

            resultado = ElContextoBD.Ingredientes.Find(Id);

            return resultado;
        }
    }
}
