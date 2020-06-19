using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Entities;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Interfaces;
using ServiceStack;
using WebApi.Filters;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    
    public class SalasController : Controller
    {
        private ISalasRepository _salas;
        public SalasController(ISalasRepository salas)
        {
            _salas = salas;
        }

        
        
        [HttpGet("GetListaSalas")]
        public IEnumerable<Salas> GetListaSalas()
        {
            return _salas.GetListaSalas();
        }

        
        [HttpPost("AgregarSala")]
        [RequiredAdminPermission]
        public ActionResult<Salas> AgregarSala([FromBody]Salas sala)
        {
            if (ModelState.IsValid)
            {
                var aux = _salas.AgregarSala(sala);
                return Ok(aux);
            }
            return BadRequest(ModelState);
           
        }

        [HttpPost("ModificarSala")]
        [RequiredAdminPermission]
        public ActionResult<Salas> ModificarSala([FromBody]Salas sala)
        {
            if (ModelState.IsValid)
            {
                var aux = _salas.ModificarSala(sala);
                return Ok(aux);
            }
            return BadRequest(ModelState);

        }

       

        // DELETE api/<controller>/5
        [HttpPost("EliminarSala")]
        [RequiredAdminPermission]
        public ActionResult<string> EliminarSala(int id)
        {
            try
            {
                _salas.EliminarrSala(id);
                return Ok("registro borrado");
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.InnerException.Message);
            }
           
            
        }
    }
}
