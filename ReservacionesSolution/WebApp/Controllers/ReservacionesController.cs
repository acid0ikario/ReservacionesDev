using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;

using Newtonsoft.Json;
using WebApp.Extensions;
using WebApp.Filters;
using WebApp.Models;
using WebApp.Utilidades;

namespace WebApp.Controllers
{
    public class ReservacionesController : BaseController
    {
        [Authenticate]
        public IActionResult Index()
        {
            List<Reservaciones> reservaciones = new List<Reservaciones>();
            HttpClient client = new HttpClient();
        
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GetTokenRequest());
            client.BaseAddress = new Uri("https://localhost:44312/api/Reservaciones/GetListaReservaciones");
            var responseTask = client.GetAsync(client.BaseAddress);
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();

                reservaciones = JsonConvert.DeserializeObject<List<Reservaciones>>(readTask.Result);

            }
            return View(reservaciones);
        }

        [Authenticate]
        // GET: Reservaciones/Create
        public IActionResult Create()
        {
            ViewData["SalaId"] = new SelectList(GetListaSalas(), "Id", "Nombre");
            return View();
        }

        // POST: Reservaciones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authenticate]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Inicio,Fin,SalaId,UserId,FechaReservacion")] Reservaciones reservaciones)
        {
            if (ModelState.IsValid)
            {
                Reservaciones reservacion = new Reservaciones();
                HttpClient client = new HttpClient();
                StringContent content = new StringContent(JsonConvert.SerializeObject(reservaciones), Encoding.UTF8, "application/json");

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GetTokenRequest());
                client.BaseAddress = new Uri("https://localhost:44312/api/Reservaciones/CrearReservacion");
                
                var responseTask = client.PostAsync(client.BaseAddress.AbsoluteUri, content);
                responseTask.Wait();
                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();

                    reservacion = JsonConvert.DeserializeObject<Reservaciones>(readTask.Result);
                    return RedirectToAction("Index", "Reservaciones");
                }
                else
                {
                    var response = result.Content.ReadAsStringAsync();
                    ModelState.AddModelError(result.StatusCode.ToString(), response.Result);
                }
            }
            ViewData["SalaId"] = new SelectList(GetListaSalas(), "Id", "Nombre", reservaciones.SalaId);
           
            return View(reservaciones);
        }

        // GET: Reservaciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservaciones = GetReservacion(id.Value);
            if (reservaciones == null)
            {
                return NotFound();
            }

            return View(reservaciones);
        }

        // POST: Reservaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            string response = "";
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GetTokenRequest());
            client.BaseAddress = new Uri("https://localhost:44312/api/Reservaciones/CancelarReservacion?id=" + id);
            var responseTask = client.PostAsync(client.BaseAddress.AbsoluteUri,null);
            responseTask.Wait();
            var result = responseTask.Result;

            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Reservaciones");
            }
            else
            {
               return NotFound();
            }
        }
           

           
        

        private List<Salas> GetListaSalas() {
            List<Salas> salas = new List<Salas>();
            HttpClient client = new HttpClient();
        
            //dejo hardcode el token del admin para realizar pruebas mas rapido.
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GetTokenRequest());
            client.BaseAddress = new Uri("https://localhost:44312/api/Salas/GetListaSalas");
            var responseTask = client.GetAsync(client.BaseAddress);
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();

                salas = JsonConvert.DeserializeObject<List<Salas>>(readTask.Result);

            }
            return salas;
        }

        private Reservaciones GetReservacion(int id)
        {
            Reservaciones reservacion = new Reservaciones();
            HttpClient client = new HttpClient();
           
            //dejo hardcode el token del admin para realizar pruebas mas rapido.
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GetTokenRequest());
            client.BaseAddress = new Uri("https://localhost:44312/api/Reservaciones/GetReservacion?id=" + id);
            var responseTask = client.GetAsync(client.BaseAddress);
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();

                reservacion = JsonConvert.DeserializeObject<Reservaciones>(readTask.Result);

            }
            else {
                return null;
            }
            return reservacion;
        }
    }
}