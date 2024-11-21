using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurante.Model
{
    public class CatalogoListaDeIngredientes
    {
        public int id { get; set; }
        public int Id_MenuIngrediente { get; set; }

        public int Id_Ingrediente { get; set; }

        [Required(ErrorMessage = "El nombre del ingrediente es requerido")]
        [Display(Name = "Ingrediente")]
        public string NombreDelIngrediente { get; set; }

        public int Id_Medida { get; set; }

        [Required(ErrorMessage = "El nombre de la medida es requerido")]
        [Display(Name = "Medida")]
        public string NombreDeLaMedida { get; set; }

        [Required(ErrorMessage = "El valor aproximado es requerido")]
        [Display(Name = "Valor Aproximado")]
        public double ValorAproximado { get; set; }
    }
}
