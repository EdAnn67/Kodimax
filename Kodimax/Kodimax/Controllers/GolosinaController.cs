using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kodimax.Models;

namespace Kodimax.Controllers
{
    public class GolosinaController : Controller
    {
        // GET: Golosina
        public ActionResult Golosinas()
        {
            KodimaxContext db = new KodimaxContext();

            return View(db.Golosina.ToList());
        }


        public ActionResult AgregarGolosina()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AgregarGolosina(Golosina P)
        {
            try
            {
                using (var db = new KodimaxContext())
                {
                    db.Golosina.Add(P);
                    db.SaveChanges();
                    return RedirectToAction("Golosinas");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("Error al agregar Golosina", e);
                return View();
            }
        }
        public ActionResult EditarGolosina(int id)
        {
            try
            {
                using (var db = new KodimaxContext())
                {
                    Golosina P = db.Golosina.Find(id);
                    return View(P);
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarGolosina(Golosina p)
        {
            try
            {
                using (var db = new KodimaxContext())
                {
                    Golosina P = db.Golosina.Find(p.Id);
                    P.Nombre = p.Nombre;
                    P.Tipo = p.Tipo;
                    P.Precio = p.Precio;
                    P.Imagen = p.Imagen;
                    db.SaveChanges();
                    return RedirectToAction("Golosinas");
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public ActionResult EliminarGolosina(int id)
        {

            using (var db = new KodimaxContext())
            {
                Golosina D = db.Golosina.Find(id);
                db.Golosina.Remove(D);
                db.SaveChanges();
                return RedirectToAction("Golosinas");
            }


        }
        public ActionResult Tipo()
        {
            try
            {
                using (var db = new KodimaxContext())
                {
                    return PartialView(db.Tipo.ToList());
                }
            }
            catch
            {
                throw;
            }
        }
    }
}