using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurante.Model
{
    public class PlatillosDelIngrediente
    {
        [Required(ErrorMessage = "El nombre del platillo es requerido")]
        public string NombrePlatillo { get; set; }

        [Required(ErrorMessage = "El nombre de la medida es requerido")]
        public string  NombreMedida { get; set; }

        [Required(ErrorMessage = "La cantidad es requerida")]
        public double Cantidad { get; set; }
    }
}
