using CuestionarioWeb.BL;
using CuestionarioWeb.EN;
using CuestionarioWeb.EN.LoginView;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CuestionarioWeb.UI.Controllers
{
    public class RespuestasController : Controller
    {
        private RespuestaBL _respuestaBL;
        private UsuarioBL _usuarioBL;

        private const string key = "%$DUJH238";

        public RespuestasController(Contexto context)
        {
            _respuestaBL = new RespuestaBL(context);
            _usuarioBL = new UsuarioBL(context);
        }

        // GET: RespuestasController
        //public ActionResult Index()
        //{
        //    string cookie = Request.Cookies[key];
        //    ResponseModel rm = _usuarioBL.Verify(cookie);
        //    if (rm.IsAuthenticated)
        //    {
               
        //        return View();
        //    }
        //    return RedirectToAction("Index", "Login");
        //}

        // GET: RespuestasController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RespuestasController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RespuestasController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PublicarRespuesta(IFormCollection resp)
        {
            try
            {
                Respuesta respuesta = new Respuesta
                {
                    RespuestaEmitida = resp["RespuestaEmitida"],
                    IdUsuario = int.Parse(resp["IdUsuario"]),
                    IdPregunta = int.Parse(resp["IdPregunta"])
                };
                string cookie = Request.Cookies[key];
                ResponseModel rm = _usuarioBL.Verify(cookie);
                if (rm.IsAuthenticated)
                {
                    int res = _respuestaBL.NuevaRespuesta(respuesta);
                    if (res > 0)
                    {
                        if (res == 1)
                        {
                            ViewData["Mensaje"] = "Respuesta publicada";
                            return RedirectToAction("Details", "Preguntas", new { id = respuesta.IdPregunta });

                        }
                        //ViewData["Mensaje"] = "Pregunta cerrada por el administrador";
                    }
                    return RedirectToAction("Details", "Preguntas", new { id = respuesta.IdPregunta });
                }
                return RedirectToAction("Index","Login");
            }
            catch
            {
                //ViewData["Mensaje"] = "Error interno";
                return RedirectToAction("Details", "Preguntas", new { id = resp["IdUsuario"] });
            }
        }


        // POST: RespuestasController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ComentarRespuesta(IFormCollection resp)
        {
            try
            {
                Respuesta respuesta = new Respuesta
                {
                    RespuestaEmitida = resp["RespuestaEmitida"],
                    IdPregunta = int.Parse(resp["IdPregunta"]),
                    AutoReferencia = int.Parse(resp["IdRespuesta"])
                };
                string cookie = Request.Cookies[key];
                ResponseModel rm = _usuarioBL.Verify(cookie);
                if (rm.IsAuthenticated)
                {
                    respuesta.IdUsuario = rm.user.IdUsuario;
                    int res = _respuestaBL.NuevaRespuesta(respuesta);
                    if (res > 0)
                    {
                        if (res == 1)
                        {
                            //ViewData["Mensaje"] = "Respuesta publicada";
                            return RedirectToAction("Details", "Preguntas", new { id = respuesta.IdPregunta });

                        }
                        //ViewData["Mensaje"] = "Pregunta cerrada por el administrador";
                    }
                    return RedirectToAction("Details", "Preguntas", new { id = respuesta.IdPregunta });
                }
                return RedirectToAction("Index", "Login");
            }
            catch
            {
                //ViewData["Mensaje"] = "Error interno";
                return RedirectToAction("Details", "Preguntas", new { id = resp["IdUsuario"] });
            }
        }

        // GET: RespuestasController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RespuestasController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        
    }
}
