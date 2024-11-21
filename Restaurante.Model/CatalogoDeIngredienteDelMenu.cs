using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurante.Model
{
    public class CatalogoDeIngredienteDelMenu
    {

        public int Id_MenuIngrediente { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El precio es requerido")]
        public double Precio { get; set; }

        [Required(ErrorMessage = "las ganacias aproximadas son requerida")]
        [Display(Name = "Valor Aproximado")]
        public double GananciasAproximadas { get; set; }
    }
}
