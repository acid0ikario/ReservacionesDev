using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Interfaces
{
   public interface IReservacionesRespository
    {
        Reservaciones CrearReservacion(Reservaciones reservacion);
        Reservaciones GetReservacion(int id);
        void CancelarReservacion(int idReservacion);
        bool EsDiponible(Reservaciones reservacion);
        List<Reservaciones> GetListaReservaciones();

    }
}
