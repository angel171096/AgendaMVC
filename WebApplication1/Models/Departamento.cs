using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Departamento
    {
        public int Id { get; set; }
        [Required]
        [StringLength(200)]
        [Display(Name = "Departamento")]
        public string NombreDep { get; set; }
        [StringLength(200)]
        public string Leyenda { get; set; }

        public virtual List<Contacto> Contactos { get; set; }
    }
}
