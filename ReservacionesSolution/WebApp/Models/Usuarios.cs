using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class Usuarios
    {
        [Key]
        [Required(ErrorMessage ="Usuario Requerido")]
        public string UserId { get; set; }
        [Required(ErrorMessage = "Constraseña Requerido")]
        public string Password { get; set; }
        public string RolId { get; set; }
        public string Email { get; set; }
        public string Nombre { get; set; }
        public ICollection<Reservaciones> Reservaciones { get; set; }
    }
}
