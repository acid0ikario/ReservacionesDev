using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class Reservaciones
    {
        public int Id { get; set; }
        public System.DateTime Inicio { get; set; }
        public System.DateTime Fin { get; set; }
        [Required(ErrorMessage ="Sala requerida")]
        public int SalaId { get; set; }
        public string UserId { get; set; }

        public DateTime FechaReservacion { get; set; }

        public string strFechaReservacion
        {
            get { return FechaReservacion.ToShortDateString() ; }
        }
        public Salas Sala { get; set; }
        public Usuarios Usuario { get; set; }
    }
}
