using DataAccess.Context;
using DataAccess.Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class ReservacionesRepository : IReservacionesRespository
    {
        private readonly ReservacionesContext _dbContext;
        public ReservacionesRepository(ReservacionesContext context)
        {
            _dbContext = context;
        }
        public void CancelarReservacion(int idReservacion)
        {
            var reservacion = _dbContext.Reservaciones.Find(idReservacion);
            if (reservacion == null)
                throw new Exception("no se encontro la reservacion id: " + idReservacion);


            _dbContext.Reservaciones.Remove(reservacion);
            _dbContext.SaveChanges();
            
        }

        public bool EsDiponible(Reservaciones nuevaReservacion)
        {
            List<Reservaciones> ListaPorDia = _dbContext.Reservaciones.Where(x => x.Inicio.Day == nuevaReservacion.Inicio.Day && x.SalaId == nuevaReservacion.SalaId).OrderBy(x => x.Inicio).ToList();

            bool inicioDisponible = false;
            bool finDisponible = false;

            //cuando no existen reuniones agendad para el dia.
            if (ListaPorDia.Count == 0)
                return true;

            //si la fecha de inicio es mayor a la mayor fecha o la fecha fin sea menor a minima de inicio de finalizacion entonces true
            if (nuevaReservacion.Inicio >= ListaPorDia.Max(x => x.Fin) || nuevaReservacion.Fin <= ListaPorDia.Min(x => x.Inicio))
            {
                return true;
            }

        
            foreach (var reservacion in ListaPorDia)
            {
                //verifico que la fecha inicio
                if (nuevaReservacion.Inicio >= reservacion.Fin)
                {
                    inicioDisponible = true;
                }

                if (nuevaReservacion.Fin <= reservacion.Inicio)
                {
                    finDisponible = true;
                }

                if (nuevaReservacion.Inicio == reservacion.Inicio && nuevaReservacion.Fin == reservacion.Fin)
                {
                    inicioDisponible = false;
                    finDisponible = false;
                }
            }
                
            

            if (inicioDisponible && finDisponible)
                return true;

            return false;
        }

        public List<Reservaciones> GetListaReservaciones()
        {
            return _dbContext.Reservaciones.Include(x=> x.Sala).Include(x=> x.Usuario).OrderBy(x=> x.Inicio).ToList();
        }

        public Reservaciones CrearReservacion(Reservaciones reservacion)
        {
            if (reservacion.Fin <= reservacion.Inicio)
                throw new Exception("La fecha de finalizacion no puede ser menor o igual a la de inicio");
         
            _dbContext.Reservaciones.Add(reservacion);
            _dbContext.SaveChanges();
            return reservacion;
        }

        public Reservaciones GetReservacion(int id)
        {
            Reservaciones rese =  _dbContext.Reservaciones.Include(x => x.Sala).Include(x=> x.Usuario).First(x => x.Id == id);
            rese.Sala.Reservaciones = null;
            rese.Usuario.Reservaciones = null;
            return rese;
        }
    }
}
