using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;

namespace WEB_2.Controllers
{
    public class HomeController : Controller
    {
        private ALUMNO alumno = new ALUMNO();
        private CURSO curso = new CURSO();
        public ActionResult Index()
        {
            return View(alumno.Listar());
        }
        public ActionResult Ver(int id)
        {
            return View(alumno.Obtener(id));
        }

        public ActionResult CRUD(int id = 0)
        {
            ViewBag.Cursos = curso.Todos();
            return View(
                        id > 0
                        ? alumno.Obtener(id)
                        : alumno
                        );
        }

        public ActionResult Eliminar(int id)
        {
            alumno.Eliminar(id);
            return Redirect("~/Home");
        }
        //public ActionResult Guardar(ALUMNO model, int[] cursos = null)
        //{
        //    if (cursos != null)
        //    {
        //        foreach (var c in cursos)
        //        {
        //            model.CURSOS.Add(new CURSO { ID = c });
        //        }
        //    }
        //    else
        //    {
        //        ModelState.AddModelError("cursos-elegidos", "Debe seleccionar al menos un curso.");
        //    }
        //    if (ModelState.IsValid)
        //    {
        //        model.Guardar();
        //        return Redirect("~/Home/CRUD/" + model.ID);
        //    }
        //    else
        //    {
        //        ViewBag.Cursos = curso.Todos();
        //        return View("~/views/Home/CRUD.cshtml", model);
        //    }
        //}

        [HttpPost]
        public JsonResult Guardar(ALUMNO model, int[] cursos_seleccionados = null)
        {
            var respuesta = new ResponseModel
            {
                respuesta = true,
                //redirect = "/Home/CRUD/" + model.ID,
                redirect = "/Home",
                error = ""
            };

            if (cursos_seleccionados != null)
            {
                foreach (var c in cursos_seleccionados)
                {
                    model.CURSOS.Add(new CURSO { ID = c });
                }
            }
            else
            {
                ModelState.AddModelError("cursos-elegidos", "Debe seleccionar al menos un curso");
                respuesta.respuesta = false;
                respuesta.error = "Debe seleccionar al menos un curso";
            }

            if (ModelState.IsValid)
            {
                model.Guardar();                
            }
            return Json(respuesta);
        }
    }
}