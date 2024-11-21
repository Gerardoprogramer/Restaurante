using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurante.Model
{
    public enum EstadoMesas
    {
        [Display(Name = "Disponible")]
        Disponible =1,

        [Display(Name = "No Disponible")]
        Reservado = 2,


        [Display(Name = "Ocupada")]
        Ocupada = 3,
        [Display(Name = " ")]
        sinEstado = 4
    }
}
