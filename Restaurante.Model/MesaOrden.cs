using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurante.Model
{
    public class MesaOrden
    {

        public int Id { get; set; }

        public int Id_Mesa { get; set; }

        public int Id_Menu { get; set; }

        [Required(ErrorMessage = "La cantidad esd requerida")]
        public int Cantidad { get; set; }

        public EstadoMesas Estado { get; set; }


    }
}
