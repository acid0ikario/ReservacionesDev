using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccess.Entities
{
    public class Salas
    {

        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre es requerido")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El ubicacion es requerido")]
        public string Ubicacion { get; set; }

      
        public ICollection<Reservaciones> Reservaciones { get; set; }

    }
}
