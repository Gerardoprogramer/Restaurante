using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurante.Model
{
    public enum MenuDeCategorias
    {
        Entrada = 1,
        [Display(Name = "Pequeñas botanas")]
        PequenasBotanas = 2,
        Aperitivos = 3,
        [Display(Name = "Sopas y ensaladas")]
        SopasYEnsaladas = 4,
        [Display(Name = "Platos principales")]
        PlatosPrincipales = 5,
        Postres = 6,
        Bebidas = 7

    }
}
