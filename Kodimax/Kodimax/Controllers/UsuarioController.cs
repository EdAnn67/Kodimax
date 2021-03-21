using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Kodimax.Models;
using Newtonsoft.Json;

namespace Kodimax.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        public ActionResult Clientes()
        {
            KodimaxContext db = new KodimaxContext();

            return View(db.Usuario.Where(U => U.Cargo == 1).ToList());
        }

        public ActionResult Empleados()
        {
            KodimaxContext db = new KodimaxContext();

            return View(db.Usuario.Where(U => U.Cargo == 2).ToList());
        }

        public ActionResult Registrarse()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registrarse(Usuario P)
        {
            try
            {
                using (var db = new KodimaxContext())
                {
                    db.Usuario.Add(P);
                    db.SaveChanges();
                    return RedirectToAction("Empleados");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("Error al agregar Usuario", e);
                return View();
            }
        }

        public ActionResult Editar(int id)
        {
            try
            {
                using (var db = new KodimaxContext())
                {
                    Usuario P = db.Usuario.Find(id);
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
        public ActionResult Editar(Usuario p)
        {
            try
            {
                using (var db = new KodimaxContext())
                {
                    Usuario P = db.Usuario.Find(p.Id);
                    P.Nombres = p.Nombres;
                    P.Apellidos = p.Apellidos;
                    P.Correo = p.Correo;
                    P.Telefono = p.Telefono;
                    P.Sexo = p.Sexo;
                    P.FechaNacimiento = p.FechaNacimiento;
                    P.Usuario1 = p.Usuario1;
                    P.Contraseña = p.Contraseña;
                    db.SaveChanges();
                    return RedirectToAction("Empleados");
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public ActionResult EliminarEmpleado(int id)
        {

            using (var db = new KodimaxContext())
            {
                Usuario D = db.Usuario.Find(id);
                db.Usuario.Remove(D);
                db.SaveChanges();
                return RedirectToAction("Empleados");
            }


        }

        public string Genero(int Genero)
        {
            try
            {
                using(var db = new KodimaxContext())
                {
                    return db.Genero.Find(Genero).Genero1;
                }
            }
            catch
            {
                throw;
            }
        }
        /*
        public ActionResult TESTSAVE()
        {
            KodimaxContext db = new KodimaxContext();
            var data = db.Usuario.Where(U => U.Cargo == 1).ToList();
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(data);
            var output = new FileContentResult(bytes, "application/octet-stream");
            output.FileDownloadName = "download.txt";

            return output;
        }*/

        private byte[] Serialize(object value, JsonSerializerSettings jsonSerializerSettings)
        {
            var result = JsonConvert.SerializeObject(value, jsonSerializerSettings);

            return Encoding.UTF8.GetBytes(result);
        }

        public ActionResult Download()
        {
            KodimaxContext db = new KodimaxContext();
            var download = Serialize(db.Usuario.Where(U => U.Cargo == 1).ToList(), new JsonSerializerSettings());

            return File(download, "application/json", "file.json");
        }
    }
}