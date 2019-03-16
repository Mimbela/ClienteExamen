using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClienteExamen.Filters;
using ClienteExamen.Models;
using ClienteExamen.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClienteExamen.Controllers
{
    public class EmpleadosController : Controller
    {
        Repository repo;

        public EmpleadosController()
        {
            this.repo = new Repository();
        }


        [EmpleadosAuthorize]
        public async Task<IActionResult> Index()
        {
            String token = HttpContext.Session.GetString("TOKEN");
            Doctor empleado = await
                this.repo.PerfilEmpleado(token);
            return View(empleado);
        }


        public async Task<IActionResult> MostrarHospital()
        {
            String token = HttpContext.Session.GetString("TOKEN");
            List<Hospital> hosp = await this.repo.ListaHospitalesAsync(token);
         
            ViewBag.Hospitales = hosp;
            return View(new Hospital());
        }

        [HttpPost]
        public async Task<IActionResult> MostrarHospital(int codigoh)
        {
           String token = HttpContext.Session.GetString("TOKEN");
         
            List<Hospital> hosp = await this.repo.ListaHospitalesAsync(token);
            ViewBag.Hospitales = hosp;

            return View(await this.repo.BuscarHospitalAsync(codigoh));
        }
        //----------------------
        public async Task<IActionResult> VerHospitalTabla()
        {
          String token = HttpContext.Session.GetString("TOKEN");
            List<Hospital> hosp = await this.repo.ListaHospitalesAsync(token);
            return View(hosp);
        }

        //----------------------------CRUD-----------------------------------
        //MODIFICAR, ELIMINAR,DETALLES,CREAR

        public async Task<IActionResult> InsertarHospital()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> InsertarHospital(Hospital h)
        {
            // Hospital hosp = this.repo.InsertarHospitalAsync(h);
            await this.repo.InsertarHospitalAsync(h);
            return RedirectToAction("VerHospitalTabla");
        }
        //--------------------
        public async Task<IActionResult> ModificarHospital(int id)
        {
            Hospital hosp = await this.repo.BuscarHospitalAsync(id);
            return View(hosp);
        }

        [HttpPost]
        public async Task<IActionResult> ModificarHospital(Hospital hospital, int id)
        {
            await this.repo.ModificarHospitalAsync(hospital, id);

            return RedirectToAction("VerHospitalTabla");
        }
        //------------------------
        public async Task<IActionResult> EliminarHospital(int id)
        {
            await this.repo.EliminarHospitalAsync(id);
            return RedirectToAction("VerHospitalTabla");
        }


    }
}