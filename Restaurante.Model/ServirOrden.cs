using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurante.Model
{
    public class ServirOrden
    {
        [Display(Name = "Platillos")]
        public int id { get; set; }

        public List<Model.Menu> MenuOrdenado { get; set; }

        public List<Model.MesaOrden> MesaOrden { get; set; }
    }
}
