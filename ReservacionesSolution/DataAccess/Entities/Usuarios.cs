using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccess.Entities
{
    public class Usuarios
    {
        [Key]
        public string UserId { get; set; }
        public string Password { get; set; }
        public string RolId { get; set; }
        public string Email { get; set; }
        public string Nombre { get; set; }
        public ICollection<Reservaciones> Reservaciones { get; set; }
        public Roles Rol { get; set; }

    }
}
