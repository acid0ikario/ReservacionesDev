using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using DataAccess.Entities;

namespace DataAccess.Context
{
   public  class ReservacionesContext : DbContext
    {
        public ReservacionesContext(DbContextOptions<ReservacionesContext> options) :base(options)
        {

        }

        public  DbSet<Reservaciones> Reservaciones { get; set; }
        public  DbSet<Roles> Roles { get; set; }
        public  DbSet<Salas> Salas { get; set; }
        public  DbSet<Usuarios> Usuarios { get; set; }
    }
}
