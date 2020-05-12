using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Entities
{
   public class Roles
    {
        public string Id { get; set; }
        public string Descripcion { get; set; }

      
        public  ICollection<Usuarios> Usuarios { get; set; }

    }
}
