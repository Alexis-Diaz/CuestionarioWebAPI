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
    public class PreguntasController : Controller
    {
        private PreguntaBL _preguntaBL;
        private UsuarioBL _usuarioBL;
        private RespuestaBL _respuestaBL;
        private const string key = "%$DUJH238";

        public PreguntasController(Contexto context)
        {
            _preguntaBL = new PreguntaBL(context);
            _usuarioBL = new UsuarioBL(context);
            _respuestaBL = new RespuestaBL(context);
        }

        // GET: PreguntasController
        public ActionResult Index( )
        {
            List<Pregunta> ListPreguntas = new List<Pregunta>();
            string cookie = Request.Cookies[key];
            ResponseModel rm = _usuarioBL.Verify(cookie);
            if (rm.IsAuthenticated)
            {
                ListPreguntas = _preguntaBL.ListarPreguntasPorMasRecientes();
                var CantidadRespuestas = _respuestaBL.ListasDeRespuestasOrdenadasPorPreguntas();
                if(CantidadRespuestas.Count > 0)
                {
                    ViewData["CantidadRespuestas"] = CantidadRespuestas;

                }
                ViewData["Roles"] = rm.rol.TipoRolUsuario;
                ViewData["IdUsuarioActual"] = rm.user.IdUsuario;
                ViewData["ListaUsuarios"] = _usuarioBL.ListarUsuarios();
                ViewData["User"] = rm;
                return View(ListPreguntas);
            }
            return RedirectToAction("Index", "Login");
        }

        // GET: PreguntasController/Details/5
        public ActionResult Details(int id, bool ordernarPorReaccion = false)
        {
            string cookie = Request.Cookies[key];
            ResponseModel rm = _usuarioBL.Verify(cookie);
            if (rm.IsAuthenticated)
            {
                if (ordernarPorReaccion)
                {
                    var pregunta = _preguntaBL.BuscarPreguntaPorId(id);
                    var respuestas = _respuestaBL.ListarCatidadReaccionesPorRespuesta(id);
                    List<List<ReaccionUsuarioRespuesta>> listaReaccionesPorPregunta = new List<List<ReaccionUsuarioRespuesta>>();
                    foreach (var respuesta in respuestas)
                    {
                        var list = _respuestaBL.ListarTodasLasReaccionesPorRespuesta(respuesta.IdRespuesta);
                        if (list.Count == 0)
                        {
                            continue;
                        }
                        listaReaccionesPorPregunta.Add(list);
                    }
                    if (respuestas.Count > 0)
                    {
                        ViewData["listaReaccionesPorPregunta"] = listaReaccionesPorPregunta;
                        ViewData["ListaReacciones"] = _respuestaBL.ListarTodasLasReacciones();
                        ViewData["ListadoRespuestas"] = respuestas;
                        ViewData["ListaUsuarios"] = _usuarioBL.ListarUsuarios();
                    }
                    ViewData["IdUsuario"] = rm.user.IdUsuario;
                    ViewData["User"] = rm;
                    return View(pregunta);
                }
                else
                {
                    var pregunta = _preguntaBL.BuscarPreguntaPorId(id);
                    var respuestas = _respuestaBL.ListarRespuestasPorPregunta(id);
                    List<List<ReaccionUsuarioRespuesta>> listaReaccionesPorPregunta = new List<List<ReaccionUsuarioRespuesta>>();
                    foreach (var respuesta in respuestas)
                    {
                        var list = _respuestaBL.ListarTodasLasReaccionesPorRespuesta(respuesta.IdRespuesta);
                        if (list.Count == 0)
                        {
                            continue;
                        }
                        listaReaccionesPorPregunta.Add(list);
                    }
                    if (respuestas.Count > 0)
                    {
                        ViewData["listaReaccionesPorPregunta"] = listaReaccionesPorPregunta;
                        ViewData["ListaReacciones"] = _respuestaBL.ListarTodasLasReacciones();
                        ViewData["ListadoRespuestas"] = respuestas;
                        ViewData["ListaUsuarios"] = _usuarioBL.ListarUsuarios();
                    }
                    ViewData["IdUsuario"] = rm.user.IdUsuario;
                    ViewData["User"] = rm;
                    return View(pregunta);
                }
               
            }
            return RedirectToAction("Index", "Login");
        }

        //GET:ComentariosDeRespuestas
        public JsonResult ComentariosDeRespuestas(int? id)
        {
            if (id != null || id < 0)
            {
                var listRespuestas = _respuestaBL.ListarLosComentariosPorRespuesta(id);
                foreach(var respuesta in listRespuestas)
                {
                    respuesta.usuario = _usuarioBL.BuscarUsuarioPorId(respuesta.IdUsuario);
                }
                return Json(listRespuestas);
            }
            return Json("{'mensaje': 'error'}");
        }


        // GET: PreguntasController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PreguntasController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection form)
        {
            try
            {
                string cookie = Request.Cookies[key];
                ResponseModel rm = _usuarioBL.Verify(cookie);
                if (rm.IsAuthenticated)
                {
                    if (!string.IsNullOrEmpty(form["Pregunta"]))
                    {
                        Pregunta pregunta = new Pregunta()
                        {
                            PreguntaFormulada = form["Pregunta"],
                            IdUsuario = int.Parse(form["IdUsuario"])
                        };
                        int res = _preguntaBL.GuardarPregunta(pregunta);
                        if (res == 1)
                        {
                            //ViewData["Mensaje"] = "Pregunta publicada";
                            return RedirectToAction("Index");
                        }
                    }
                   
                    //ViewData["Mensaje"] = "La pregunta se pudo publicar, intentalo de nuevo.";
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index", "Login");
            }
            catch
            {
                //ViewData["Mensaje"] = "Oye creo que algo salió, trataremos de resolverlo en breve.";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Reaccionar(int? idRespuesta, int? idEmoji, int? idUsuario, int? idPregunta)
        {
            string cookie = Request.Cookies[key];
            ResponseModel rm = _usuarioBL.Verify(cookie);
            if (rm.IsAuthenticated)
            {
                int res = _respuestaBL.ReaccionarSobreRespuesta(idRespuesta, idEmoji, idUsuario);
                if (res == 1)
                {
                    return RedirectToAction("Details", "Preguntas", new { id = idPregunta });
                }
                return NotFound();
            }
            return RedirectToAction("Index", "Login");
          
        }

        [HttpGet]
        public ActionResult CerrarPregunta(int id)
        {
            string cookie = Request.Cookies[key];
            ResponseModel rm = _usuarioBL.Verify(cookie);
            if (rm.IsAuthenticated)
            {
                string rol = rm.rol.TipoRolUsuario.ToLower();
                if(rol == "administrador")
                {
                    if (id > 0)
                    {
                        _preguntaBL.CerrarPregunta(id);
                        return RedirectToAction("Index","Preguntas");
                    }
                }
                return RedirectToAction("Index", "Preguntas");
            }
            return RedirectToAction("Index", "Login");
        }
        // GET: PreguntasController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PreguntasController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details(IFormCollection form)
        {
            try
            {
                int idPregunta = int.Parse(form["idPregunta"]);
                var res = 0;
                bool ordernarPorReaccion = int.TryParse(form["ordernarPorReaccion"], out res);
                if (ordernarPorReaccion)
                {
                    return RedirectToAction("Details", "Preguntas", new { id = idPregunta, ordernarPorReaccion = ordernarPorReaccion });

                }
                return RedirectToAction("Details", "Preguntas");
            }
            catch
            {
                return RedirectToAction("Details", "Preguntas");
            }
        }

        // GET: PreguntasController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PreguntasController/Delete/5
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
