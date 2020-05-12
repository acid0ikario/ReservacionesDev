using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Interfaces
{
    public interface ISalasRepository
    {

        Salas AgregarSala(Salas sala);
        Salas ModificarSala(Salas sala);
        void EliminarrSala(int id);
        List<Salas> GetListaSalas();

    }
}
