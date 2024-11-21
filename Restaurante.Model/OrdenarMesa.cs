using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurante.Model
{
    public class OrdenarMesa
    {

        public int id { get; set; }

        public List<Model.Menu>  Menu { get; set; }

        [Required(ErrorMessage = "La cantidad es requerida")]
        public int Cantidad { get; set; }

        [Display(Name = "Lista De Platillos")]
        public int idMenu { get; set; }
    }
}
