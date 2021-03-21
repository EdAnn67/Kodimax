using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kodimax.Models;

namespace Kodimax.Controllers
{
    public class PeliculaController : Controller
    {
        // GET: Pelicula
        public ActionResult Peliculas()
        {
            KodimaxContext db = new KodimaxContext();
            
            return View(db.Pelicula.ToList());
        }


        public ActionResult Agregar()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Agregar(Pelicula P)
        {
            try
            {
                using (var db = new KodimaxContext())
                {
                    db.Pelicula.Add(P);
                    db.SaveChanges();
                    return RedirectToAction("Peliculas");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("Error al agregar película", e);
                return View();
            }
        }
        public ActionResult Editar(int id)
        {
            try
            {
                using (var db = new KodimaxContext())
                {
                    Pelicula P = db.Pelicula.Find(id);
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
        public ActionResult Editar(Pelicula p)
        {
            try
            {
                using (var db = new KodimaxContext())
                {
                    Pelicula P = db.Pelicula.Find(p.Id);
                    P.Nombre = p.Nombre;
                    P.Duracion = p.Duracion;
                    P.Genero = p.Genero;
                    P.Imagen = p.Imagen;
                    db.SaveChanges();
                    return RedirectToAction("Peliculas");
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public ActionResult EliminarPelicula(int id)
        {

            using (var db = new KodimaxContext())
            {
                Pelicula D = db.Pelicula.Find(id); 
                db.Pelicula.Remove(D);
                db.SaveChanges();
                return RedirectToAction("Peliculas");
            }


        }
        public ActionResult Genero()
        {
            try
            {
                using (var db = new KodimaxContext())
                {
                    return PartialView(db.Genero.ToList());
                }
            }
            catch
            {
                throw;
            }
        }


    }
}