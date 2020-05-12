using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Comunes
{
    public static class Utilidades
    {
        public static List<DateTime> Horarios(int intervaloEnMinutos)
        {
            DateTime inicio = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 08, 00, 00);
            DateTime fin = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 21, 00, 00);
            List<DateTime> horarios = new List<DateTime>();
            while (inicio <= fin)
            {
                horarios.Add(inicio);
                inicio.AddMinutes(intervaloEnMinutos);
            }
            return horarios;
        }
    }
}
