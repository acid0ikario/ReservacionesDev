using DataAccess.Context;
using DataAccess.Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository.Services
{
   public class AccountRepository : IAccountRepository
    {
        public readonly ReservacionesContext _dbContext;
        public AccountRepository(ReservacionesContext contx)
        {
            _dbContext = contx;
        }
        public Usuarios AuthenticateUser(string user, string pass)
        {
            return _dbContext.Usuarios.FirstOrDefault(x => x.UserId == user && x.Password == pass);
        }

        public List<Usuarios> GetListaUsers()
        {
            return _dbContext.Usuarios.ToList();
        }
    }
}
