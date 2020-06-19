using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using DataAccess.Entities;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using Repository.Interfaces;
using WebApi.Helpers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    
    public class ReservacionesController : Controller
    {
        private readonly IReservacionesRespository _reservaciones;
        public ReservacionesController(IReservacionesRespository reser)
        {
            _reservaciones = reser;
        }
        // GET: api/<controller>
        [HttpGet("GetListaReservaciones")]
        public ActionResult<string> GetListaReservaciones()
        {
            var listaReservaciones = _reservaciones.GetListaReservaciones();
            listaReservaciones.ForEach(x => { x.Sala.Reservaciones = null;  x.Usuario.Reservaciones = null; }) ;
            return  Ok(listaReservaciones);
        }

        [HttpGet("GetReservacion")]
        public ActionResult<string> GetReservacion(int id)
        {
            var reservacion = _reservaciones.GetReservacion(id);
            return Ok(reservacion);
        }

     
        [HttpPost("CrearReservacion")]
        public ActionResult<Reservaciones> CrearReservacion([FromBody]Reservaciones reservacion)
        {
            TokenManipulations _token = new TokenManipulations(Request);
            reservacion.UserId = _token.GetLoggedUser();
            reservacion.FechaReservacion = DateTime.Now;
            bool disponible = _reservaciones.EsDiponible(reservacion);
            if (!disponible)
                ModelState.AddModelError("NoDisponible", "Sala no disponible en este horario");

            if (ModelState.IsValid)
            {
                try
                {
                    return Ok(_reservaciones.CrearReservacion(reservacion));
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                }
            }
            else
            {
                return BadRequest(ModelState);
            }

        }

        // POST api/<controller>
        [HttpPost("CancelarReservacion")]
        public ActionResult<Reservaciones> CancelarReservacion(int id)
        {
            try
            {
                _reservaciones.CancelarReservacion(id);
                return Ok("Reservacion Cancelada");
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
          
        }

    }
}
