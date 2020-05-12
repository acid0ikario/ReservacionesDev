using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Entities
{
   public class Reservaciones
    {
        public int Id { get; set; }
        public System.DateTime Inicio { get; set; }
        public System.DateTime Fin { get; set; }
        public int SalaId { get; set; }
        public string UserId { get; set; }
        public DateTime FechaReservacion { get; set; }

        public  Salas Sala { get; set; }
        public  Usuarios Usuario { get; set; }

    }
}
