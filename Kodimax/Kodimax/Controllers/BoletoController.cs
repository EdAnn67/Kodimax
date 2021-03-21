using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kodimax.Models;

namespace Kodimax.Controllers
{
    public class BoletoController : Controller
    {
        // GET: Boleto
        public ActionResult Boletos()
        {
            KodimaxContext db = new KodimaxContext();

            return View(db.Boleto.ToList());
        }

        public ActionResult ComprarBoleto()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ComprarBoleto(Boleto P)
        {
            try
            {
                using (var db = new KodimaxContext())
                {
                    db.Boleto.Add(P);
                    db.SaveChanges();
                    return RedirectToAction("Boletos");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("Error al generar Boleto", e);
                return View();
            }
        }
        public ActionResult Pelicula()
        {
            try
            {
                using (var db = new KodimaxContext())
                {
                    return PartialView(db.Pelicula.ToList());
                }
            }
            catch
            {
                throw;
            }
        }
        public ActionResult Empleado()
        {
            try
            {
                using (var db = new KodimaxContext())
                {
                    return PartialView(db.Usuario.Where(U => U.Cargo == 2).ToList());
                }
            }
            catch
            {
                throw;
            }
        }
        public ActionResult Cine()
        {
            try
            {
                using (var db = new KodimaxContext())
                {
                    return PartialView(db.Cine.ToList());
                }
            }
            catch
            {
                throw;
            }
        }
        public ActionResult Sala()
        {
            try
            {
                using (var db = new KodimaxContext())
                {
                    return PartialView(db.Sala.ToList());
                }
            }
            catch
            {
                throw;
            }
        }
        /*public ActionResult Subtotal(int sala, int NBol)
        {
            double SubT;
            if(sala == 1)
            {
                SubT = NBol * 6.50;
            }
            else if(sala == 2)
            {
                SubT = NBol * 4.75;
            }
            else
            {
                SubT = NBol * 3.55;
            }
            return SubT;
        }*/
    }
}