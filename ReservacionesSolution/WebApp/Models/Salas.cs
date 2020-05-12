using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class Salas
    {

        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre es requerido")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El Ubicacion es requerido")]
        public string Ubicacion { get; set; }


        public ICollection<Reservaciones> Reservaciones { get; set; }
    }
}
