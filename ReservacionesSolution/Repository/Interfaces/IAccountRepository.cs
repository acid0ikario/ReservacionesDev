using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Interfaces
{
   public interface IAccountRepository
    {
        Usuarios AuthenticateUser(string user, string pass);
        List<Usuarios> GetListaUsers();
    }
}
