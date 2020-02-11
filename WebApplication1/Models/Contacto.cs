using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Contacto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Nombres { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Apellidos { get; set; }
        [Display(Name = "Fecha de Nacimiento")]
        public DateTime FechaNacimiento { get; set; }
        [StringLength(300)]
        public string Direccion { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }

        public int DepartamentoId { get; set; }
        public virtual Departamento Departamento { get; set; }
    }
}
