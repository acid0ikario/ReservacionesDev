using DataAccess.Context;
using DataAccess.Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class SalasRepository : ISalasRepository
    {
        private readonly ReservacionesContext _dbcontext;
        public SalasRepository(ReservacionesContext context)
        {
            _dbcontext = context;
        }
        public Salas AgregarSala(Salas sala)
        {
            _dbcontext.Salas.Add(sala);
            _dbcontext.SaveChanges();
            return sala;
        }

        public void EliminarrSala(int id)
        {
            var sala = _dbcontext.Salas.Find(id);
            _dbcontext.Remove(sala);
            _dbcontext.SaveChanges();
        }

        public List<Salas> GetListaSalas()
        {
            return _dbcontext.Salas.ToList();
        }

        public Salas ModificarSala(Salas sala)
        {
            _dbcontext.Salas.Update(sala);
            _dbcontext.SaveChanges();
            return sala;
        }
    }
}
