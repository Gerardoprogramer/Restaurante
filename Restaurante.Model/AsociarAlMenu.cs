using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurante.Model
{
    public class AsociarAlMenu
    {

        public List<Medidas> Medida { get; set; }

        public List<Ingredientes> Ingrediente { get; set; }

        [Required(ErrorMessage = "La Cantidad es requerido")]
        public int Cantidad { get; set; }

        [Required(ErrorMessage = "El valor aproximado es requerido")]
        [Display(Name = "Valor Aproximado")]
        public int Valor { get; set; }

        [Required(ErrorMessage = "El ingrediente es requerido")]
        [Display(Name = "Ingredientes")]
        public int idIngrediente { get; set; }

        [Required(ErrorMessage = "La medida es requerida")]
        [Display(Name = "Medidas")]
        public int idMedidas { get; set; }

        [Required(ErrorMessage = "El Menu es requerido")]
        public int idMenu { get; set; }

    }
}
