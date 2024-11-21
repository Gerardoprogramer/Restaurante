using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurante.Model
{
    public class MenuIngredientes
    {
        public int Id { get; set; }

        public int Id_Menu { get; set; }

        public int Id_Ingredientes { get; set; }

        [Required(ErrorMessage = "La cantidad es requerida")]
        public double Cantidad { get; set; }

        public int Id_Medidas { get; set; }

        [Required(ErrorMessage = "El valor es requerido")]
        [Display(Name = "Valor Aproxima")]
        public int ValorAproximado { get; set; }
    }
}
