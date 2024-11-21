
using Microsoft.AspNetCore.Mvc;

namespace Restaurante.BS
{
    public interface IRepositorioDeRestaurante
    {

        //Metodos de las Medidas
        List<Model.Medidas> ObtengaLaListaDeMedidas();
        Model.Medidas ObTengalaMedida(string Nombre);
        List<Model.Medidas> ObTengaLasMedidasPorNombre(string nombre);
        Model.Medidas ObtenerPorIdLaMedida(int Id);
        void AgregueLaMedida(Model.Medidas medida);
        void EditarLaMedida(Model.Medidas medida);


        //Metodos Meni Ingredientes
        List<Model.MenuIngredientes> ObtengaLaListaDeIngredientesDelMenu();
        List<Model.CatalogoListaDeIngredientes> ObtengaLaListaFiltradaPorId(int id);
        List<Model.CatalogoListaDeIngredientes> ObTengaElCatalogoDeLaListaDeIngredientes();
        List<Model.CatalogoDeIngredienteDelMenu> ObTengaElCatalogoDeLosIngredienteDelMenu();
        Model.MenuIngredientes ObtenerPorIdElIngredienteDelMenu(int Id);
        void AgregueAlMenuDeIngredientes(Model.MenuIngredientes menuIngrediente);
        List<Model.CatalogoDeIngredienteDelMenu> AgregueACatalogoIngredienteDelMenu();
        Model.CatalogoDeIngredienteDelMenu ObtenerPorIdElCatalogoDeIngredientesDelMenu(int Id);
        Model.CatalogoListaDeIngredientes ObtenerPorIdElCatalogoDeListaDeIngredientes(int Id);
        List<Model.CatalogoListaDeIngredientes> AgregueALaListaDeIngrediente();
        Model.CatalogoListaDeIngredientes ObTengaCatalogoPorNombre(string Nombre);
        void DesasociarMenuDeIngrediente(int id);

        //Metodos de las Ingredientes
        List<Model.Ingredientes> ObtengaLaListaDeIngredientes();
        Model.Ingredientes ObTengaElIngredientePorNombre(string Nombre);
        List<Model.Ingredientes> ObTengaLosIngredientePorNombre(string nombre);
        Model.DetalleIngrediente ObtenerPorIdElIngredienteDetalle(int Id);
        void AgregueElIngrediente(Model.Ingredientes ingrediente);
        void EditarElIngrediente(Model.Ingredientes ingrediente);

        Model.Ingredientes ObtenerPorIdElIngrediente(int Id);

        //Metodos Menu
        List<Model.Menu> ObtengaLaListaDelMenu();
        Model.Menu ObTengaElMenuPorNombre(string Nombre);
        List<Model.Menu> ObTengaLosMenuPorNombre(string nombre);
        Model.Menu ObtenerPorIdElMenu(int Id);
        void AgregueElMenu(Model.Menu menu);
        void EditarElMenu(Model.Menu menu);


        //Metodos Mesas
        List<Model.Mesas> ObtengaLaListaDeMesas();
        Model.Mesas ObtenerPorIdLaMesa(int id);
        void AgregueLaMesa(Model.Mesas mesa);
        Model.Mesas ObtengaLaMesa(string nombre);
        void EditarLaMesa(Model.Mesas mesa);


        //Metodos MesaOrden
        List<Model.MesaOrden> ObtengaLaListaDelOrdenDeLasMesas();
        Model.MesaOrden ObtenerPorIdLaOrdenDeLaMesa(int id);
        void AgregueLaOrdenALaMesa(Model.MesaOrden mesa);
        void EditarLaMesa(Model.MesaOrden mesa);

        List<Model.CatalogoDeOrdenes> ObtengaLaListaDelCatalogoOrdenDeLasMesas();
        Model.CatalogoDeOrdenes ObtenerPorIdElCatalogoOrdenDeLaMesa(int id);
        List<Model.CatalogoDeOrdenes> AgregueElCatalogoDeOrdenALaMesa();


        List<Model.OrdenarMesa> ObtengaLaListaDeOrdenesDeLaMesa();
        Model.OrdenarMesa ObtengaPorIdLaOrdenDeLaMesa(int id);
        Model.OrdenarMesa AgregueUnaOrdenAlaMesa(int id);


        List<Model.ServirOrden> ObtengaLaListaDePlatillosAServir();

        Model.ServirOrden FiltrarPorNombreLaListaAServir(int id);

        void EliminarElPlatilloServido(int idMesa,int idMenu);



    }
}